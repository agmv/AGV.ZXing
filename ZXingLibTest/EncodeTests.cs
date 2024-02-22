namespace AGV.ZXing.Tests;

[TestFixture]
public class EncodeTests
{
    private ZXingLib z = Tests.zxing;

    [Test(Description = "Executes tests for encoding 1D barcodes")]
    [TestCase("692771017440", "UPC_A")]
    [TestCase("00123457", "UPC_E")]
    [TestCase("9780804816632", "EAN_13")]
    [TestCase("48512343", "EAN_8")]
    [TestCase("A40156B", "CODABAR")]
    [TestCase("TEST", "CODE_39")]
    [TestCase("00123457", "CODE_93")]
    [TestCase("0123456789045678", "CODE_128")]
    [TestCase("(11)100224(17)110224(3102)000100", "CODE_128")]
    [TestCase("00012345678905", "ITF")]
    [TestCase("1234567890ABCEDF", "PLESSEY", false)]
    [TestCase("1234567", "MSI", false)]
    [Category("Encode")]
    public void TestEncode_1D(string contents, string format, bool canDecode = true)
    {
        var bytes = z.Encode(contents, format, 300, 100, 10, true, false, false, "UTF-8", null, null, null, null);
#if DEBUG
        File.WriteAllBytes($"output/1d/{format}.png", bytes);
#endif
        var barcode = z.Decode(bytes, format);
        if (canDecode) {
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(contents, Is.EqualTo(b.value));
        }
    }

    [Test(Description = "Executes tests for encoding GS1 barcodes")]
    [TestCase("CODE_128")]
    [TestCase("DATA_MATRIX", "square")]
    [TestCase("DATA_MATRIX", "rectangle")]
    [Category("Encode")]
    public void TestEncode_GS1(string format, string? forceShape = null)
    {
        var contents = format == "CODE_123" ? $"{(char)0x00F1}01234567890{(char)0x00F1}45678" : $"{(char)29}01234567890{(char)29}45678";
        var bytes = z.Encode(contents, format, 300, 100, 10, true, false, true, "UTF-8", null, null, null, forceShape);
#if DEBUG
        File.WriteAllBytes($"output/gs1/{format}_{forceShape}_GS1.png", bytes);
#endif
        var barcode = z.Decode(bytes, format);
        Assert.That(barcode, Is.Not.Null);
        var b = barcode.GetValueOrDefault();
        Assert.That(b.value, Is.EqualTo(contents));
    }

    [Test(Description = "Executes tests for 2D barcodes")]
    [TestCase("test", "AZTEC")]
    [TestCase("␝01234567890␝45678", "DATA_MATRIX", null, "square")]
    [TestCase("test", "PDF_417")]
    [TestCase("This is a test", "QR_CODE", "L")]
    [TestCase("This is a test", "QR_CODE", "M")]
    [TestCase("This is a test", "QR_CODE", "Q")]
    [TestCase("This is a test", "QR_CODE", "H")]
    [Category("Encode")]
    public void TestEncode_2D(string contents, string format, string? ecl = null, string? forceShape = null)
    {
        var bytes = z.Encode(contents, format, 200, 200, 10, true, false, false, "UTF-8", ecl, null, null, forceShape);
#if DEBUG
        File.WriteAllBytes($"output/2d/{format}.png", bytes);
#endif
        var barcode = z.Decode(bytes, format);
        Assert.That(barcode, Is.Not.Null);
        var b = barcode.GetValueOrDefault();
        Assert.That(b.value, Is.EqualTo(contents));
    }

    [Test(Description = "Executes tests for 1D barcodes with label")]
    [TestCase("A40156B", "CODABAR")]
    [TestCase("692771017440", "UPC_A")]
    [TestCase("00123457", "UPC_E")]
    [TestCase("9780804816632", "EAN_13")]
    [TestCase("48512343", "EAN_8")]
    [TestCase("TEST", "CODE_39")]
    [TestCase("00123457", "CODE_93")]
    [TestCase("0123456789045678", "CODE_128")]
    [TestCase("(11)100224(17)110224(3102)000100", "CODE_128")]
    [TestCase("00012345678905", "ITF")]
    [TestCase("1234567890ABCEDF", "PLESSEY", false)]
    [TestCase("1234567", "MSI", false)]
    [Category("BarcodeLabel")]
    public void TestEncode_NotPureBarcode(string contents, string format, bool canDecode = true)
    {
        var bytes = z.Encode(contents, format, 300, 100, 10, false, false, false, "UTF-8", null, null, null, null);
        var img = SixLabors.ImageSharp.Image.Load(new MemoryStream(bytes));
#if DEBUG
        File.WriteAllBytes($"output/withlabel/{format}_label.png", bytes);
#endif
        if (canDecode) {
            var barcode = z.Decode(bytes, format);
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(contents, Is.EqualTo(b.value));
        }
    }

}