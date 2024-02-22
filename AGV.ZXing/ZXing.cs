using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using ZXing;
using ZXing.Common;
using ZXing.ImageSharp.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;
using AGV.ZXing.Structures;
using ZXing.Datamatrix;
using ZXing.Datamatrix.Encoder;

namespace AGV.ZXing {

    public class ZXingLib : IZXingLib
    {
        // The following barcodes are supported by the decoder: UPC-A, UPC-E, EAN-8, EAN-13, Code 39, Code 93, Code 128, ITF, Codabar, MSI, RSS-14 (all variants), 
        // QR Code, Data Matrix, Aztec and PDF-417.
        // AGV: Maxicode, IMB, and PHARMA_CODE decoders also implemented according to Github... 
        static BarcodeFormat[] decoders = new BarcodeFormat[] { BarcodeFormat.UPC_A, BarcodeFormat.UPC_E,
                        BarcodeFormat.EAN_8, BarcodeFormat.EAN_13, BarcodeFormat.CODE_39, BarcodeFormat.CODE_93, BarcodeFormat.CODE_128,
                        BarcodeFormat.ITF, BarcodeFormat.CODABAR, BarcodeFormat.MSI, BarcodeFormat.RSS_14, BarcodeFormat.RSS_EXPANDED,
                        BarcodeFormat.QR_CODE, BarcodeFormat.DATA_MATRIX, BarcodeFormat.AZTEC, BarcodeFormat.PDF_417, 
                        BarcodeFormat.MAXICODE, BarcodeFormat.IMB };
        
        // The encoder supports the following formats: UPC-A, EAN-8, EAN-13, Code 39, Code 128, ITF, Codabar, Plessey, MSI, QR Code, PDF-417, Aztec, Data Matrix
        static BarcodeFormat[] encoders = new BarcodeFormat[] { BarcodeFormat.UPC_A,
                    BarcodeFormat.EAN_8, BarcodeFormat.EAN_13, BarcodeFormat.CODE_39, BarcodeFormat.CODE_128,
                    BarcodeFormat.ITF, BarcodeFormat.CODABAR, BarcodeFormat.PLESSEY, BarcodeFormat.MSI,
                    BarcodeFormat.QR_CODE, BarcodeFormat.DATA_MATRIX, BarcodeFormat.AZTEC, BarcodeFormat.PDF_417 };
                            
        public Structures.Barcode? Decode(byte[] image, string? formatHint, bool detectionImage = false) {
            
            var reader = new BarcodeReaderGeneric();
            reader.AutoRotate = true;            
            reader.Options.CharacterSet = "UTF-8";            
            reader.Options.ReturnCodabarStartEnd = true;
            reader.Options.TryHarder = true;
            reader.Options.TryInverted = true;
            reader.Options.UseCode39ExtendedMode = true;
            reader.Options.UseCode39RelaxedExtendedMode = true;
            if (formatHint != null && formatHint != "")
            {    
                reader.Options.PossibleFormats = new List<BarcodeFormat>();
                reader.Options.PossibleFormats.Add(Enum.Parse<BarcodeFormat>(formatHint));
            } else {
                reader.Options.PossibleFormats = decoders;
            }
            var img = Image.Load<Rgba32>(new MemoryStream(image));            
            var result = reader.Decode(img);

            return (result == null) ? null : new Structures.Barcode(result.Text, 
                                        result.RawBytes, 
                                        result.BarcodeFormat.ToString(), 
                                        convertMetadata(result.ResultMetadata), 
                                        detectionImage ? generateMarksImage(result, img.Clone()) : null);
        }


        public IEnumerable<Structures.Barcode>? DecodeMulti(byte[] image, string? formatHint, bool detectionImage = false) {
                        
            var reader = new BarcodeReaderGeneric();            
            reader.AutoRotate = true;
            reader.Options.CharacterSet = "UTF-8";            
            reader.Options.ReturnCodabarStartEnd = true;
            reader.Options.TryHarder = true;
            reader.Options.TryInverted = true;
            reader.Options.UseCode39ExtendedMode = true;
            reader.Options.UseCode39RelaxedExtendedMode = true;
            if (formatHint != null && formatHint != "")
            {    
                reader.Options.PossibleFormats = new List<BarcodeFormat>();
                reader.Options.PossibleFormats.Add(Enum.Parse<BarcodeFormat>(formatHint));
            }
            else {
                reader.Options.PossibleFormats = decoders;
            }
            var img = Image.Load<Rgba32>(new MemoryStream(image));
            var result = reader.DecodeMultiple(img);
            
            return (result == null) ? null : Array.ConvertAll(result, r => new Structures.Barcode(r.Text, 
                                                                    r.RawBytes, 
                                                                    r.BarcodeFormat.ToString(), 
                                                                    convertMetadata(r.ResultMetadata),
                                                                    detectionImage?generateMarksImage(r,img.Clone()):null));
        }

        public byte[] Encode(string contents, string format, int width, int height, int margin, bool pureBarcode, bool gS1Format, 
        bool noPadding, string? encoding, string? ecl, int? qRCodeVersion, byte[]? overlayImage, string? forceShape, string outputFormat = "PNG") {
            var f = Enum.Parse<BarcodeFormat>(format);
            if ((f == BarcodeFormat.EAN_13 || f == BarcodeFormat.UPC_A) && (margin < 6)) {
                throw new Exception("EAN-13 and UPC-A codes should have a margin greater than 6 to ensure barcode can be scanned properly.");
            }

            var options = new EncodingOptions{
                Width = width,
                Height = height,
                Margin = margin,
                PureBarcode = pureBarcode,
                GS1Format = gS1Format,
                NoPadding = noPadding
            };

            if (encoding != null && encoding != "")
                options.Hints.Add(EncodeHintType.CHARACTER_SET, encoding);
                
            if (ecl != null && ecl != "")
                options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ecl);

            if (f == BarcodeFormat.QR_CODE && qRCodeVersion != null && qRCodeVersion != 0)
                options.Hints.Add(EncodeHintType.QR_VERSION, qRCodeVersion);

            if (f == BarcodeFormat.DATA_MATRIX && forceShape != null){
                options.Hints.Add(EncodeHintType.DATA_MATRIX_SHAPE, forceShape == "square" ? SymbolShapeHint.FORCE_SQUARE : forceShape == "rectangle" ? SymbolShapeHint.FORCE_RECTANGLE : SymbolShapeHint.FORCE_NONE);
            }

            // if (SVG) {
            //     var w = new BarcodeWriterSvg(){ Format = f, Options = options};
            //     var b = w.Encode(contents);
            //     var r = new SvgRenderer();
            //     var i = r.Render(b, f, contents);
            //     return System.Text.Encoding.UTF8.GetBytes(i.Content);
            // }

            var writer = new BarcodeWriter<SixLabors.ImageSharp.Formats.Png.PngFormat>{ Format = f, Options = options };
            var barcode = writer.Encode(contents);            
            var render = new ImageSharpRenderer<Rgba32>();
            var image = render.Render(barcode, f, contents);
            
            if (f == BarcodeFormat.QR_CODE && overlayImage != null && overlayImage.Length != 0) {
                var overlay = Image.Load(new MemoryStream(overlayImage));
                
                //Check overlay coverage on top of QR code. Too much may lead to barcode not being readable.
                var ratio = (overlay.Width * overlay.Height) / (image.Width * image.Height * 1.0);
                /*7% (L), 15 % (M), 25% (Q), 30% (H) of error correction where a error correction of level H should result in a QRCode that is still valid even when it's 30% obscured */
                if (ecl == "H" && ratio > 0.3)
                    throw new Exception("With ErrorCorrectionLevel.H the maximum overlap of the QR code is 30%. Choose a smaller overlay image.");
                if (ecl == "Q" && ratio > 0.25)
                    throw new Exception("With ErrorCorrectionLevel.Q the maximum overlap of the QR code is 25%. Choose a smaller overlay image or a higher error correction level.");
                if (ecl == "M" && ratio > 0.15)
                    throw new Exception("With ErrorCorrectionLevel.M the maximum overlap of the QR code is 15%. Choose a smaller overlay image or a higher error correction level.");
                if (ecl == "L" && ratio > 0.07)
                    throw new Exception("With ErrorCorrectionLevel.L the maximum overlap of the QR code is 7%. Choose a smaller overlay image or a higher error correction level.");
                
                var center = new Point((image.Width - overlay.Width)/2,(image.Height - overlay.Height)/2);
                image.Mutate(x => x.DrawImage(overlay, center, 0.8f));
            }

            var l = new List<BarcodeFormat> { BarcodeFormat.CODABAR, BarcodeFormat.CODE_128, BarcodeFormat.CODE_39, BarcodeFormat.CODE_93, BarcodeFormat.CODE_128,
            BarcodeFormat.EAN_8, BarcodeFormat.EAN_13, BarcodeFormat.ITF, BarcodeFormat.RSS_14, BarcodeFormat.RSS_EXPANDED, BarcodeFormat.UPC_A, BarcodeFormat.UPC_E, 
            BarcodeFormat.PLESSEY, BarcodeFormat.MSI };
                
            if (!pureBarcode && l.Contains(f)) {
                Stream? s = Assembly.GetExecutingAssembly().GetManifestResourceStream("AGV.ZXing.resources.OCRB Regular.ttf");

                if (s != null) {
                    var collection = new FontCollection();
                    var family = collection.Add(s);
                    var font = family.CreateFont(12, FontStyle.Regular);                    
                    var o = new ResizeOptions {
                        Mode = ResizeMode.BoxPad,
                        PadColor = Color.White,
                        Position = AnchorPositionMode.Top,
                        Size = new Size(image.Width, (int)(image.Height + font.Size + 4))
                    };
                    var h = image.Height + 4;
                    var w = image.Width;
                    image.Mutate(x => x.Resize(o).DrawText(contents,font,Color.Black,new PointF(w/2 - TextMeasurer.MeasureSize(contents, new(font){}).Width/2,h)));
                }
            }

            var stream = new MemoryStream();            
            switch (outputFormat.ToLower())
            {
                case "gif":
                    image.SaveAsGif(stream);
                    break;
                case "jpg":
                    image.SaveAsJpeg(stream);
                    break;
                case "webp":
                    image.SaveAsWebp(stream);
                    break;
                case "bmp":
                    image.SaveAsBmp(stream);
                    break;
                case "png":
                    image.SaveAsPng(stream);
                    break;
                default:
                    throw new Exception("Unsupported format");
            }
            return stream.ToArray();
        }

        public byte[] EncodeCalendarEvent(CalendarEvent calendarEvent, int size, byte[]? overlayImage = null, string outputFormat = "PNG")
        {
            return Encode(calendarEvent.ToString(), "QR_CODE", size, size, 0, true, false, true, "UTF-8", null, null, overlayImage, outputFormat);
        }

        public byte[] EncodeContact(Contact contact, bool isMeCard, int size, byte[]? overlayImage = null, string outputFormat = "PNG")
        {
            return Encode(isMeCard ? contact.ToMeCardString() : contact.ToVCardString(), "QR_CODE", size, size, 0, true, false, true, "UTF-8", null, null, overlayImage, outputFormat);
        }

        public byte[] EncodeEmail(string email,int size,byte[]? overlayImage = null, string outputFormat = "PNG")
        {
            return Encode($"mailto:{email}", "QR_CODE", size, size, 0, true, false, true, "UTF-8", null, null, overlayImage, outputFormat);
        }

        public byte[] EncodeLocation(string latitude, string longitude, int size, byte[]? overlayImage = null, string outputFormat = "PNG")
        {
            return Encode($"geo:{latitude},{longitude}", "QR_CODE", size, size, 0, true, false, true, "UTF-8", null, null, overlayImage, outputFormat);
        }

        public byte[] EncodePhoneNumber(string phoneNumber, bool isFacetime, int size, byte[]? overlayImage = null, string outputFormat = "PNG")
        {
            return Encode(isFacetime?$"facetime:{phoneNumber}":$"tel:{phoneNumber}", "QR_CODE", size, size, 0, true, false, true, "UTF-8", null, null, overlayImage, null, outputFormat);
        }

        public byte[] EncodeSMS(string phoneNumber, string message, int size, byte[]? overlayImage = null, string outputFormat = "PNG")
        {
            return Encode($"smsto:{phoneNumber}:{message}", "QR_CODE", size, size, 0, true, false, true, "UTF-8", null, null, overlayImage, outputFormat);
        }

        public byte[] EncodeWifi(Wifi wifi, int size, byte[]? overlayImage = null, string outputFormat = "PNG")
        {
            return Encode(wifi.ToString(), "QR_CODE", size, size, 0, true, false, true, "UTF-8", null, null, overlayImage, outputFormat);
        }

        protected Structures.Metadata[] convertMetadata(System.Collections.Generic.IDictionary<ResultMetadataType, object> metadata) {
            var l = new List<Structures.Metadata>(metadata.Count);
            foreach( var m in metadata) {
                var r = new Structures.Metadata();
                r.key = Enum.GetName(typeof(ResultMetadataType), m.Key) ?? "";
                r.value = System.Text.Json.JsonSerializer.Serialize(m.Value);
                l.Add(r);
            }
            return l.ToArray();
        }

        protected byte[]? generateMarksImage(Result result, Image<Rgba32> img) {
            if (result.ResultPoints.Count() > 1) {
                var stream = new MemoryStream();
                        
                var rotated = false;
                // Rotate if needed
                if (Int32.TryParse(result.ResultMetadata[ResultMetadataType.ORIENTATION].ToString(), out int angle)) {
                    img.Mutate(x => x.Rotate(-angle));
                    rotated = true;
                }
                
                var points = result.ResultPoints.ToList().ConvertAll(new Converter<ResultPoint, PointF>(x => new PointF(x.X, x.Y)));
                var pen = Pens.Solid(Color.LimeGreen, 2);

                switch(points.Count) {
                    case 2:
                        img.Mutate(x => x.DrawLine(pen, points.ToArray()));
                    break;
                    case 4:
                        img.Mutate(x => x.DrawPolygon(pen, points.ToArray()));
                    break;
                    default:
                        if (points.Count % 2 == 0) {
                            for (int i = 0; i < points.Count; i+=2) {
                                var p = new PointF[] {
                                    points[i], points[i+1]//, points[i+2], points[i+3]
                                };
                                img.Mutate(x => x.DrawPolygon(pen, p));
                            }
                        }
                    break;
                }
                
                if (rotated)
                    img.Mutate(x => x.Rotate(angle));

                img.SaveAsPng(stream);
                return stream.ToArray();
            }
            return null;
        }

        public string[] Encoders() {
            return Array.ConvertAll(encoders, x => x.ToString());
        }
                
    }

    public static class StringExtension{
        public static string encodeQRCode(this string s) {
            return s.Replace(",",@"\,").Replace(";",@"\;").ReplaceLineEndings(@"\n").Replace(@"\",@"\\").Replace(@"""",@"\""").Replace(":",@"\:");
        }
    }

}