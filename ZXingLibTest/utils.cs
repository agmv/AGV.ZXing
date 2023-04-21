using System;
using System.IO;
using System.Reflection;

namespace AGV.ZXing.Tests;

[TestFixture]
public class Utils
{
    public static byte[] loadResource(string resourceName)
    {
        Stream? s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (s == null)
            throw new Exception();
        var ms = new MemoryStream();
        s.CopyTo(ms);
        return ms.ToArray();
    }
}