using System.Collections.Generic;
using OutSystems.Model.ExternalLibraries.SDK;

namespace AGV.ZXing {

    [OSInterface(Description = "Provides actions to encode and decode barcodes. The following barcodes are supported by the decoder: UPC-A, UPC-E, EAN-8, EAN-13, Code 39, Code 93, Code 128, ITF, Codabar, MSI, RSS-14 (all variants), QR Code, Data Matrix, Aztec, and PDF-417. The encoder supports the following formats: UPC-A, EAN-8, EAN-13, Code 39, Code 128, ITF, Codabar, Plessey, MSI, QR Code, PDF-417, Aztec, Data Matrix", IconResourceName = "AGV.ZXing.resources.zxing.png", Name = "ZXingLib")]
    public interface IZXingLib {
        [OSAction(Description = @"Scans barcode from an image", IconResourceName = "AGV.ZXing.resources.qr_code_scanner.png", ReturnName = "Barcode", OriginalName = "Decode")]
        public Structures.Barcode Decode(
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Image containing the barcode", OriginalName = "Image")]            
            byte[] image,
            [OSParameter(DataType = OSDataType.Text, Description = "A hint for the barcode format present in the image", OriginalName = "FormatHint")] 
            string? formatHint = null);

        //TODO: return a list of barcodes (IEnumerable<Structures.Barcode>) instead of just one.
        // [OSAction(Description = @"Scans barcode from an image", IconResourceName = "AGV.ZXing.resources.qr_code_scanner.png", ReturnName = "Barcodes")]
        // public IEnumerable<Structures.Barcode> DecodeMulti(
        //     [OSParameter(DataType = OSDataType.BinaryData, Description = "Image containing the barcode")]
        //     byte[] image,
        //     [OSParameter(DataType = OSDataType.Text, Description = "A hint for the barcode format present in the image")] 
        //     string? formatHint = null);

        [OSAction(Description = "Generates a barcode with the received input data", IconResourceName = "AGV.ZXing.resources.qr_code.png", 
                    ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData, OriginalName = "Encode")]
        public byte[] Encode(
            [OSParameter(DataType = OSDataType.Text, Description = "Contents to encode as a barcode", OriginalName = "Contents")]
            string contents,
            [OSParameter(DataType = OSDataType.Text, Description = "Barcode format", OriginalName = "Format")]
            string format,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image", OriginalName = "Width")]
            int width,
            [OSParameter(DataType = OSDataType.Integer, Description = "Height of the barcode image", OriginalName = "Height")]
            int height,
            [OSParameter(DataType = OSDataType.Integer, Description = "Specifies margin, in pixels, to use when generating the barcode. The meaning can vary by format; for example it controls margin before and after the barcode horizontally for most 1D formats.", OriginalName = "Margin")]
            int margin,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Don't put the content string into the output image.", OriginalName = "IsPureBarcode")]
            bool pureBarcode,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Specifies whether the data should be encoded to the GS1 standard; FNC1 character is added in front of the data", OriginalName = "IsGS1Format")]
            bool gS1Format,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Don't add a white area around the generated barcode if the requested size is larger than then barcode.", OriginalName = "NoPadding")]
            bool noPadding,
            [OSParameter(DataType = OSDataType.Text, Description = "Specifies what character encoding to use where applicable, e.g. UTF-8, ISO-8859-1", OriginalName = "Encoding")]
            string? encoding = null,
            [OSParameter(DataType = OSDataType.Text, Description = @"Specifies what degree of error correction to use, for example in QR Codes. Type depends on the encoder. 
            For example for QR codes it can be L (~7% correction), M (~15% correction), Q (~20% correction), and H (~30% correction).
            For Aztec it is integer representing the minimal percentage of error correction words. Note: an Aztec symbol should have a minimum of 25% EC words. 
            For PDF417 it is integer between 0 and 8", OriginalName = "ErrorCorrection")]
            string? ecl = null,
            [OSParameter(DataType = OSDataType.Integer, Description = "Specifies the exact version of QR code to be encoded.", OriginalName = "QRCodeVersion")]
            int? qRCodeVersion = null,
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.", OriginalName = "OverlayImage")]
            byte[]? overlayImage = null);

        [OSAction(Description = "Generates a barcode with the received calendar event data", IconResourceName = "AGV.ZXing.resources.event.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData, OriginalName = "EncodeCalendarEvent")]
        public byte[] EncodeCalendarEvent(
            [OSParameter(Description = "Calendar event to encode as a barcode", OriginalName = "CalendarEvent")]
            Structures.CalendarEvent calendarEvent,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image", OriginalName = "Size")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.", OriginalName = "OverlayImage")]
            byte[]? overlayImage = null);

        [OSAction(Description = "Generates a barcode with the received contact data", IconResourceName = "AGV.ZXing.resources.contact.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData, OriginalName = "EncodeContact")]
        public byte[] EncodeContact(
            [OSParameter(Description = "Contact to encode as a barcode", OriginalName = "Contact")]
            Structures.Contact contact,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Indicates if it uses MECARD format instead of VCARD format", OriginalName = "IsMeCard")]
            bool isMeCard,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image", OriginalName = "Size")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.", OriginalName = "OverlayImage")]
            byte[]? overlayImage = null);
        
        [OSAction(Description = "Generates a barcode with the received email address", IconResourceName = "AGV.ZXing.resources.email.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData, OriginalName = "EncodeEmail")]
        public byte[] EncodeEmail(
            [OSParameter(DataType = OSDataType.Text, Description = "Email to encode", OriginalName = "Email")]
            string email,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image", OriginalName = "Size")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.", OriginalName = "OverlayImage")]
            byte[]? overlayImage = null);
            
        [OSAction(Description = "Generates a barcode with the received geo location", IconResourceName = "AGV.ZXing.resources.gps.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData, OriginalName = "EncodeLocation")]
        public byte[] EncodeLocation(
            [OSParameter(DataType = OSDataType.Text, Description = "Latitude", OriginalName = "Latitude")]
            string latitude,
            [OSParameter(DataType = OSDataType.Text, Description = "Longitude", OriginalName = "Longitude")]
            string longitude,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image", OriginalName = "Size")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.", OriginalName = "OverlayImage")]
            byte[]? overlayImage = null);
            
        [OSAction(Description = "Generates a barcode with the received phone number", IconResourceName = "AGV.ZXing.resources.phone.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData, OriginalName = "EncodePhoneNumber")]
        public byte[] EncodePhoneNumber(
            [OSParameter(DataType = OSDataType.Text, Description = "Phone number to encode", OriginalName = "PhoneNumber")]
            string phoneNumber,
            [OSParameter(DataType = OSDataType.Boolean, Description = "Indicates if it generates a facetime call", OriginalName = "IsFacetime")]
            bool isFacetime,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image", OriginalName = "Size")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.", OriginalName = "OverlayImage")]
            byte[]? overlayImage = null);
            
        [OSAction(Description = "Generates a barcode with the received SMS content", IconResourceName = "AGV.ZXing.resources.sms.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData, OriginalName = "EncodeSMS")]
        public byte[] EncodeSMS(
            [OSParameter(DataType = OSDataType.Text, Description = "Phone number", OriginalName = "PhoneNumber")]
            string phoneNumber,
            [OSParameter(DataType = OSDataType.Text, Description = "Message", OriginalName = "Message")]
            string message,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image", OriginalName = "Size")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.", OriginalName = "OverlayImage")]
            byte[]? overlayImage = null);
            
        [OSAction(Description = "Generates a barcode with the Wifi connection information", IconResourceName = "AGV.ZXing.resources.wifi.png", 
                ReturnName = "BarcodeImage", ReturnType = OSDataType.BinaryData, OriginalName = "EncodeWifi")]
        public byte[] EncodeWifi(
            [OSParameter(Description = "Wifi data", OriginalName = "Wifi")]
            Structures.Wifi wifi,
            [OSParameter(DataType = OSDataType.Integer, Description = "Width of the barcode image", OriginalName = "Size")]
            int size,            
            [OSParameter(DataType = OSDataType.BinaryData, Description = "Overlay image for QR Code.", OriginalName = "OverlayImage")]
            byte[]? overlayImage = null);
            
    }
}
