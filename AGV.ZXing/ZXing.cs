using System;
using System.IO;
using System.Collections.Generic;
using ZXing;
using ZXing.Rendering;
using ZXing.Common;
using ZXing.ImageSharp.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using OutSystems.Model.ExternalLibraries.SDK;
using AGV.ZXing.Structures;

namespace AGV.ZXing {

    public class ZXingLib : IZXingLib
    {
        public Structures.Barcode Decode(byte[] image, string? formatHint) {
            
            var reader = new BarcodeReaderGeneric();            
            reader.AutoRotate = true;            
            reader.Options.CharacterSet = "UTF-8";            
            reader.Options.AssumeMSICheckDigit = true;
            reader.Options.ReturnCodabarStartEnd = true;
            reader.Options.TryHarder = true;
            reader.Options.TryInverted = true;
            if (formatHint != null && formatHint != "")
            {    
                reader.Options.PossibleFormats = new List<BarcodeFormat>();
                reader.Options.PossibleFormats.Add(Enum.Parse<BarcodeFormat>(formatHint));
            }  
            
            var result = reader.Decode(Image.Load<Rgba32>(new MemoryStream(image)));

            if (result == null)
                throw new Exception("No barcode decoded.");

            return new Structures.Barcode(result.Text, result.RawBytes, result.BarcodeFormat.ToString(), convertMetadata(result.ResultMetadata));
        }


        IEnumerable<Structures.Barcode> DecodeMulti(byte[] image, string? formatHint) {
            
            var reader = new BarcodeReaderGeneric();            
            reader.AutoRotate = true;            
            reader.Options.CharacterSet = "UTF-8";            
            reader.Options.AssumeMSICheckDigit = true;
            reader.Options.ReturnCodabarStartEnd = true;
            reader.Options.TryHarder = true;
            reader.Options.TryInverted = true;
            if (formatHint != null && formatHint != "")
            {    
                reader.Options.PossibleFormats = new List<BarcodeFormat>();
                reader.Options.PossibleFormats.Add(Enum.Parse<BarcodeFormat>(formatHint));
            }  
            
            var result = reader.DecodeMultiple(Image.Load<Rgba32>(new MemoryStream(image)));
            if (result == null)
            {
                throw new Exception("No barcode decoded.");
            }                        

            return Array.ConvertAll(result, r => new Structures.Barcode(r.Text, r.RawBytes, r.BarcodeFormat.ToString(), convertMetadata(r.ResultMetadata)));
        }

        public byte[] Encode(string contents, string format, int width, int height, int margin, bool pureBarcode, bool gS1Format, bool noPadding, string? encoding, string? ecl, int? qRCodeVersion, byte[]? overlayImage) {
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
                
                //Check if overlay coverage on top of QR code does not invalid it
                var ratio = (overlay.Width * overlay.Height) / (width * height * 1.0);
                /*7% (L), 15 % (M), 25% (Q), 30% (H) of error correction were a error correction of level H should result in a QRCode that are still valid even when it�s 30% obscured */
                if (ecl == "H" && ratio > 0.3)
                    throw new Exception("With ErrorCorrectionLevel.H the maximum overlap of the QR code is 30%. Choose a smaller overlay image.");
                if (ecl == "Q" && ratio > 0.25)
                    throw new Exception("With ErrorCorrectionLevel.Q the maximum overlap of the QR code is 25%. Choose a smaller overlay image or a higher error correction level.");
                if (ecl == "M" && ratio > 0.15)
                    throw new Exception("With ErrorCorrectionLevel.M the maximum overlap of the QR code is 15%. Choose a smaller overlay image or a higher error correction level.");
                if (ecl == "L" && ratio > 0.07)
                    throw new Exception("With ErrorCorrectionLevel.L the maximum overlap of the QR code is 7%. Choose a smaller overlay image or a higher error correction level.");
                
                var deltaWidth = width - overlay.Width;
                var deltaHeight = height - overlay.Height;                
                image.Mutate(x => x.DrawImage(overlay, new Point(deltaWidth/2,deltaHeight/2), 0.8f));
            }

            var stream = new MemoryStream();
            image.SaveAsPng(stream);
            return stream.ToArray();
        }

        public byte[] EncodeCalendarEvent(CalendarEvent calendarEvent, int size, byte[]? overlayImage = null)
        {
            return Encode(calendarEvent.ToString(), "QR_CODE", size, size, 0, false, false, true, "UTF-8", null, null, overlayImage);
        }

        public byte[] EncodeContact(Contact contact, bool isMeCard, int size, byte[]? overlayImage = null)
        {
            return Encode(isMeCard ? contact.ToMeCardString() : contact.ToVCardString(), "QR_CODE", size, size, 0, false, false, true, "UTF-8", null, null, overlayImage);
        }

        public byte[] EncodeEmail(string email,int size,byte[]? overlayImage = null)
        {
            return Encode(string.Format("mailto:{0}",email), "QR_CODE", size, size, 0, false, false, true, "UTF-8", null, null, overlayImage);
        }

        public byte[] EncodeLocation(string latitude, string longitude, int size, byte[]? overlayImage = null)
        {
            return Encode(string.Format("geo:{0},{1}", latitude, longitude), "QR_CODE", size, size, 0, false, false, true, "UTF-8", null, null, overlayImage);
        }

        public byte[] EncodePhoneNumber(string phoneNumber, bool isFacetime, int size, byte[]? overlayImage = null)
        {
            return Encode(string.Format("{0}{1}", isFacetime?"facetime:":"tel:", phoneNumber), "QR_CODE", size, size, 0, false, false, true, "UTF-8", null, null, overlayImage);
        }

        public byte[] EncodeSMS(string phoneNumber, string message, int size, byte[]? overlayImage = null)
        {
            return Encode(string.Format("smsto:{0}:{1}", phoneNumber, message), "QR_CODE", size, size, 0, false, false, true, "UTF-8", null, null, overlayImage);
        }

        public byte[] EncodeWifi(Wifi wifi, int size, byte[]? overlayImage = null)
        {
            return Encode(wifi.ToString(), "QR_CODE", size, size, 0, false, false, true, "UTF-8", null, null, overlayImage);
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
        
    }

    public static class StringExtension{
        public static string encodeQRCode(this string s) {
            return s.Replace(",",@"\,").Replace(";",@"\;").ReplaceLineEndings(@"\n").Replace(@"\",@"\\").Replace(@"""",@"\""").Replace(":",@"\:");
        }
    }
}