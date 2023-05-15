using System.Collections.Generic;
using OutSystems.ExternalLibraries.SDK;

namespace AGV.ZXing {

    [OSInterface(Description = @"Provides actions to encode and decode barcodes. 
The following barcodes are supported by the decoder: UPC-A, UPC-E, EAN-8, EAN-13, Code 39, Code 93, Code 128, ITF, Codabar, MSI, RSS-14 (all variants), QR Code, Data Matrix, Aztec, and PDF-417. The encoder supports the following formats: UPC-A, EAN-8, EAN-13, Code 39, Code 128, ITF, Codabar, Plessey, MSI, QR Code, PDF-417, Aztec, Data Matrix", 
    IconResourceName = "AGV.ZXing.resources.zxing.png", Name = "ZXingLib")]
    public interface IZXingLib {        
        [OSAction(Description = @"Scans barcode from an image", IconResourceName = "AGV.ZXing.resources.qr_code_scanner.png", ReturnName = "Barcode")]
        public Structures.Barcode? Decode(
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Image containing the barcode")]            
            byte[] image,
            [OSParameter(DataType = OSDataType.Text, Description = "A hint for the barcode format present in the image")] 
            string? formatHint = null,
            [OSParameter(DataType = OSDataType.Boolean, Description = "If true, the action will return the original image with barcode detection marks.")] 
            bool detectionImage = false);

        [OSAction(Description = @"Scans barcode from an image", IconResourceName = "AGV.ZXing.resources.qr_code_scanner.png", ReturnName = "Barcodes")]
        public IEnumerable<Structures.Barcode>? DecodeMulti(
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Image containing the barcode")]
            byte[] image,
            [OSParameter(DataType = OSDataType.Text, Description = "A hint for the barcode format present in the image")] 
            string? formatHint = null,
            [OSParameter(DataType = OSDataType.Boolean, Description = "If true, the action will return the original image with barcode detection marks.")] 
            bool detectionImage = false);

        [OSAction(Description = "Supported barcode encoding formats", IconResourceName = "AGV.ZXing.resources.encoders.png", ReturnName = "Formats")]
        public string[] Encoders();


        [OSAction(Description = "Generates a barcode with the received input data", IconResourceName = "AGV.ZXing.resources.qr_code.png", 
                    ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData)]
        public byte[] Encode(
            [OSParameter(DataType = OSDataType.Text, Description = "Contents to encode as a barcode")]
            string contents,
            [OSParameter(DataType = OSDataType.Text, Description = "Barcode format")]
            string format,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image")]
            int width,
            [OSParameter(DataType = OSDataType.Integer, Description = "Height of the barcode image")]
            int height,
            [OSParameter(DataType = OSDataType.Integer, Description = "Specifies margin, in pixels, to use when generating the barcode. The meaning can vary by format; for example it controls margin before and after the barcode horizontally for most 1D formats.")]
            int margin,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Don't put the content string into the output image.")]
            bool pureBarcode,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Specifies whether the data should be encoded to the GS1 standard; FNC1 character is added in front of the data")]
            bool gS1Format,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Don't add a white area around the generated barcode if the requested size is larger than then barcode.")]
            bool noPadding,
            [OSParameter(DataType = OSDataType.Text, Description = "Specifies what character encoding to use where applicable, e.g. UTF-8, ISO-8859-1")]
            string? encoding = null,
            [OSParameter(DataType = OSDataType.Text, Description = @"Specifies what degree of error correction to use, for example in QR Codes. Type depends on the encoder. 
            For example for QR codes it can be L (~7% correction), M (~15% correction), Q (~20% correction), and H (~30% correction).
            For Aztec it is integer representing the minimal percentage of error correction words. Note: an Aztec symbol should have a minimum of 25% EC words. 
            For PDF417 it is integer between 0 and 8")]
            string? ecl = null,
            [OSParameter(DataType = OSDataType.Integer, Description = "Specifies the exact version of QR code to be encoded.")]
            int? qRCodeVersion = null,
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.")]
            byte[]? overlayImage = null,
            [OSParameter(Description = "Output image format, e.g. PNG. Default: PNG. Supported formats: PNG,GIF,WEBP,JPG,BMP.")]
            string outputFormat = "PNG");

        [OSAction(Description = "Generates a barcode with the received calendar event data", IconResourceName = "AGV.ZXing.resources.event.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData)]
        public byte[] EncodeCalendarEvent(
            [OSParameter(Description = "Calendar event to encode as a barcode")]
            Structures.CalendarEvent calendarEvent,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.")]
            byte[]? overlayImage = null,
            [OSParameter(Description = "Output image format, e.g. PNG. Default: PNG. Supported formats: PNG,GIF,WEBP,JPG,BMP.")]
            string outputFormat = "PNG");

        [OSAction(Description = "Generates a barcode with the received contact data", IconResourceName = "AGV.ZXing.resources.contact.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData)]
        public byte[] EncodeContact(
            [OSParameter(Description = "Contact to encode as a barcode")]
            Structures.Contact contact,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Indicates if it uses MECARD format instead of VCARD format")]
            bool isMeCard,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.")]
            byte[]? overlayImage = null,
            [OSParameter(Description = "Output image format, e.g. PNG. Default: PNG. Supported formats: PNG,GIF,WEBP,JPG,BMP.")]
            string outputFormat = "PNG");
        
        [OSAction(Description = "Generates a barcode with the received email address", IconResourceName = "AGV.ZXing.resources.email.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData)]
        public byte[] EncodeEmail(
            [OSParameter(DataType = OSDataType.Text, Description = "Email to encode")]
            string email,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.")]
            byte[]? overlayImage = null,
            [OSParameter(Description = "Output image format, e.g. PNG. Default: PNG. Supported formats: PNG,GIF,WEBP,JPG,BMP.")]
            string outputFormat = "PNG");
            
        [OSAction(Description = "Generates a barcode with the received geo location", IconResourceName = "AGV.ZXing.resources.gps.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData)]
        public byte[] EncodeLocation(
            [OSParameter(DataType = OSDataType.Text, Description = "Latitude")]
            string latitude,
            [OSParameter(DataType = OSDataType.Text, Description = "Longitude")]
            string longitude,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.")]
            byte[]? overlayImage = null,
            [OSParameter(Description = "Output image format, e.g. PNG. Default: PNG. Supported formats: PNG,GIF,WEBP,JPG,BMP.")]
            string outputFormat = "PNG");
            
        [OSAction(Description = "Generates a barcode with the received phone number", IconResourceName = "AGV.ZXing.resources.phone.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData)]
        public byte[] EncodePhoneNumber(
            [OSParameter(DataType = OSDataType.PhoneNumber, Description = "Phone number to encode")]
            string phoneNumber,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Indicates if it generates a facetime call")]
            bool isFacetime,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.")]
            byte[]? overlayImage = null,
            [OSParameter(Description = "Output image format, e.g. PNG. Default: PNG. Supported formats: PNG,GIF,WEBP,JPG,BMP.")]
            string outputFormat = "PNG");
            
        [OSAction(Description = "Generates a barcode with the received SMS content", IconResourceName = "AGV.ZXing.resources.sms.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData)]
        public byte[] EncodeSMS(
            [OSParameter(DataType = OSDataType.PhoneNumber, Description = "Phone number")]
            string phoneNumber,
            [OSParameter(DataType = OSDataType.Text, Description = "Message")]
            string message,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.")]
            byte[]? overlayImage = null,
            [OSParameter(Description = "Output image format, e.g. PNG. Default: PNG. Supported formats: PNG,GIF,WEBP,JPG,BMP.")]
            string outputFormat = "PNG");
            
        [OSAction(Description = "Generates a barcode with the Wifi connection information", IconResourceName = "AGV.ZXing.resources.wifi.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData)]
        public byte[] EncodeWifi(
            [OSParameter(Description = "Wifi data")]
            Structures.Wifi wifi,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.")]
            byte[]? overlayImage = null,
            [OSParameter(Description = "Output image format, e.g. PNG. Default: PNG. Supported formats: PNG,GIF,WEBP,JPG,BMP.")]
            string outputFormat = "PNG");
            
    }
}
