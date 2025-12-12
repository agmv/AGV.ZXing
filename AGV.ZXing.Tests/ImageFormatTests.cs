namespace AGV.ZXing.Tests
{
    [TestFixture]
    public class ImageFormatTests
    {
        private ZXingLib z = Tests.zxing;

        [Test(Description = "Execute tests for image formats")]
        [TestCase("png")]
        [TestCase("jpg")]
        [TestCase("bmp")]
        [TestCase("webp")]
        [Category("ImageFormat")]
        public void TestImageFormats(string format)
        {
            var contents = "This is a test";
            var bytes = z.Encode(contents, "CODE_128", 300, 100, 10, true, false, false, "UTF-8", null, null, null, null);
#if DEBUG
            File.WriteAllBytes($"output/imageformat/image_{format}.{format}", bytes);
#endif
            var barcode = z.Decode(bytes, "CODE_128");
            Assert.That(barcode, Is.Not.Null);
            var b = barcode.GetValueOrDefault();
            Assert.That(contents, Is.EqualTo(b.Value));
        }
    }
}