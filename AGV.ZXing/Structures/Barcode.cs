using OutSystems.ExternalLibraries.SDK;
using System.Collections.Generic;

namespace AGV.ZXing.Structures
{

    [OSStructure(Description = "Structure that holds decoded barcodes")]
    public struct Barcode
    {
        [OSStructureField(DataType = OSDataType.Text, Description = "Barcode decoded value")]
        public string value;

        [OSStructureField(Description = "Barcode raw bytes")]
        public byte[]? rawBytes;

        [OSStructureField(DataType = OSDataType.Text, Description = "Barcode format")]
        public string format;

        [OSStructureField(Description = "Metadata associated with the decoded barcode")]
        public IEnumerable<Metadata> metadata;

        [OSStructureField(Description = "Original image with a bounding rectangle on the detected barcode")]
        public byte[]? detectedBarcode;

        public Barcode(Barcode b) : this()
        {
            value = b.value;
            rawBytes = b.rawBytes;
            format = b.format;
            metadata = b.metadata;
            detectedBarcode = b.detectedBarcode;
        }

        public Barcode(string v, byte[] b, string f, Metadata[] m, byte[]? i) : this()
        {
            value = v;
            rawBytes = b;
            format = f;
            metadata = m;
            detectedBarcode = i;
        }
    }
}