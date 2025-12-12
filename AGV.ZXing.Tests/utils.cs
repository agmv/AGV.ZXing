using System;
using System.Reflection;

namespace AGV.ZXing.Tests
{
    public class Utils
    {
        public static byte[] LoadResource(string resourceName)
        {
            Stream? s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName) ?? throw new Exception();
            using MemoryStream ms = new();
            s.CopyTo(ms);
            return ms.ToArray();
        }
    }
}