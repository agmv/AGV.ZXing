using System.Linq;
using AGV.ZXing.Structures;

namespace AGV.ZXing.Tests
{
    [TestFixture]
    public class DecodeTests
    {
        private ZXingLib z = Tests.zxing;

        /*
            BarcodeFormat.UPC_A, BarcodeFormat.UPC_E,
            BarcodeFormat.EAN_8, BarcodeFormat.EAN_13, BarcodeFormat.CODE_39, BarcodeFormat.CODE_93, BarcodeFormat.CODE_128,
            BarcodeFormat.ITF, BarcodeFormat.CODABAR, BarcodeFormat.MSI, BarcodeFormat.RSS_14, BarcodeFormat.RSS_EXPANDED,
            BarcodeFormat.QR_CODE, BarcodeFormat.DATA_MATRIX, BarcodeFormat.AZTEC, BarcodeFormat.PDF_417, 
            BarcodeFormat.MAXICODE, BarcodeFormat.IMB
        */
        [Test(Description = "Decode test cases")]
        [TestCase("UPC_A", "ZXingLibTest.resources.upc-a.png", "000012345670")]
        [TestCase("UPC_E", "ZXingLibTest.resources.upc-e.png", "12345670")]
        [TestCase("EAN_8", "ZXingLibTest.resources.ean-8.png", "12345670")]
        [TestCase("EAN_13", "ZXingLibTest.resources.EAN_13.png", "8413000065504")]
        [TestCase("CODE_39", "ZXingLibTest.resources.code-39.png", "0123456789A")]
        [TestCase("CODE_93", "ZXingLibTest.resources.code-93.png", "1234567890ABCDEFG")]
        [TestCase("CODE_128", "ZXingLibTest.resources.code-128.png", "01234567890abcdefg")]
        [TestCase("ITF", "ZXingLibTest.resources.itf.png", "123457")]
        [TestCase("CODABAR", "ZXingLibTest.resources.codabar.png", "A1234567890C")]
        [TestCase("MSI", "ZXingLibTest.resources.msi.png", "1234567890123")]
        [TestCase("RSS_14", "ZXingLibTest.resources.rss_14.png", "00821935106427")]
        [TestCase("RSS_EXPANDED", "ZXingLibTest.resources.rss_expanded.png", "(11)100224(17)110224(3102)000100")]
        [TestCase("QR_CODE", "ZXingLibTest.resources.qrcode.png", "ZXing rocks!")]
        [TestCase("DATA_MATRIX", "ZXingLibTest.resources.datamatrix.png", "\u001d0106972413915988112020091720250821BJ0006GA3A")]
        [TestCase("AZTEC", "ZXingLibTest.resources.aztec.png", "Demonstration Aztec Code symbol generated by libzint")]
        [TestCase("PDF_417", "ZXingLibTest.resources.pdf417.png", "DBARCODE 2D")]
        [TestCase("MAXICODE", "ZXingLibTest.resources.maxicode.png", "[)>\u001e01\u001d96123450000\u001d222\u001d111\u001dMODE3")]
        [TestCase("IMB", "ZXingLibTest.resources.imb.png", "0004000074510848240094306000000")]
        [TestCase("PHARMA_CODE", "ZXingLibTest.resources.pharmacode.png", "12323")]
        [Category("Decode")]
        public void TestDecode(string format, string resourceImage, string contents)
        {
            var barcode = z.Decode(Utils.LoadResource(resourceImage), format, true);
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(b.value, Is.EqualTo(contents));
            Assert.That(b.format, Is.EqualTo(format));
#if DEBUG
            if (b.detectedBarcode != null && b.detectedBarcode.Length > 0)
                File.WriteAllBytes($"output/decoded/{format}.png", b.detectedBarcode);
#endif
        }

        [Test]
        [TestCase(0, "9310779300005", "EAN_13", "POSSIBLE_COUNTRY")]
        [TestCase(1, "CODE39", "CODE_39")]
        [TestCase(2, "Code 128", "CODE_128")]
        [TestCase(3, "012345678905", "UPC_A", "POSSIBLE_COUNTRY")]
        [TestCase(4, "0200000123000017", "CODE_128")]
        [Category("DecodeMulti")]
        public void TestDecodeMulti(int position, string value, string format, string? metadatakey = null)
        {
            var barcodes = z.DecodeMulti(Utils.LoadResource("ZXingLibTest.resources.multi-1d.jpg"), null, true);
            Assert.That(barcodes, Is.Not.Null);
            Assert.That(barcodes, Is.Not.Empty);
            var barcode = barcodes != null ? barcodes.ElementAt(position) : new Barcode();
            Assert.That(barcode.format, Is.EqualTo(format));
            Assert.That(barcode.value, Is.EqualTo(value));
            if (metadatakey != null)
                Assert.That(barcode.metadata.Any(x => x.key == metadatakey), Is.True);
#if DEBUG
            if (barcode.detectedBarcode != null && barcode.detectedBarcode.Length > 0)
                File.WriteAllBytes($"output/decoded/multi1d_{position}_{format}.png", barcode.detectedBarcode);
#endif

        }
    }
}