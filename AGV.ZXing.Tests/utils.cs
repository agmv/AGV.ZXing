using System;
using System.Reflection;

namespace AGV.ZXing.Tests
{
    [TestFixture]
    public class Utils
    {
        public static byte[] LoadResource(string resourceName)
        {
            Stream? s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName) ?? throw new Exception();
            var ms = new MemoryStream();
            s.CopyTo(ms);
            return ms.ToArray();
        }
    }
}