using System;
using System.IO;
using System.Collections.Generic;
using ZXing;
using ZXing.Common;
using ZXing.ImageSharp.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AGV.ZXing {

    public class ZXingLib : IZXingLib
    {
        public IEnumerable<Structures.Barcode> Decode(byte[] image, string? formatHint) {                        

            var i = Image.Load<Rgba32>(new MemoryStream(image));            

            var reader = new BarcodeReaderGeneric();            
            reader.AutoRotate = true;            
            reader.Options.CharacterSet = "UTF-8";            
            reader.Options.AssumeMSICheckDigit = true;
            reader.Options.ReturnCodabarStartEnd = true;
            reader.Options.TryHarder = true;
            reader.Options.TryInverted = true;
            if (formatHint != null)
            {    
                reader.Options.PossibleFormats = new List<BarcodeFormat>();
                reader.Options.PossibleFormats.Add(Enum.Parse<BarcodeFormat>(formatHint));
            }  
            
            var result = reader.DecodeMultiple(i);
            if (result == null)
            {
                throw new Exception("No barcode decoded.");
            }                        

            return Array.ConvertAll(result, r => new Structures.Barcode(r.Text, r.RawBytes, r.BarcodeFormat.ToString(), convertMetadata(r.ResultMetadata)));
        }


        public byte[] Encode(string contents, string format, int width, int height, int margin, bool pureBarcode, bool gS1Format, bool noPadding, string? encoding, string? ecl, int? qRCodeVersion) {
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

            if (encoding != null)
                options.Hints.Add(EncodeHintType.CHARACTER_SET, encoding);
                
            if (ecl != null)
                options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ecl);

            if (f == BarcodeFormat.QR_CODE && qRCodeVersion != null)
                options.Hints.Add(EncodeHintType.QR_VERSION, qRCodeVersion);

            var writer = new BarcodeWriter<SixLabors.ImageSharp.Formats.Png.PngFormat>{ Format = f, Options = options };            
            var barcode = writer.Encode(contents);
            var render = new ImageSharpRenderer<Rgba32>();
            var stream = new MemoryStream();
            render.Render(barcode, f, contents).SaveAsPng(stream);
            return stream.ToArray();
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
}