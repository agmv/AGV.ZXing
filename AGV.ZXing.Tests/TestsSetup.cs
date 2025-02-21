namespace AGV.ZXing.Tests
{
    [SetUpFixture]
    public class Tests
    {

        public static ZXingLib zxing = new ZXingLib();

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            if (Directory.Exists("output"))
                Directory.Delete("output", true);
            Directory.CreateDirectory("output");
            Directory.CreateDirectory("output/decoded");
            Directory.CreateDirectory("output/1d");
            Directory.CreateDirectory("output/gs1");
            Directory.CreateDirectory("output/2d");
            Directory.CreateDirectory("output/withlabel");
            Directory.CreateDirectory("output/imageformat");
            Directory.CreateDirectory("output/qrcode");
        }

    }
}