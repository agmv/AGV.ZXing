using OutSystems.ExternalLibraries.SDK;
using System.Collections.Generic;

namespace AGV.ZXing.Structures
{

    [OSStructure(Description = "Structure that holds decoded barcodes")]
    public struct Barcode
    {
        [OSStructureField(DataType = OSDataType.Text, Description = "Barcode decoded value")]
        public string Value { get; set; } = "";

        [OSStructureField(DataType = OSDataType.BinaryData, Description = "Barcode raw bytes")]
        public byte[]? RawBytes { get; set; } = null;

        [OSStructureField(DataType = OSDataType.Text, Description = "Barcode format")]
        public string Format { get; set; } = "";

        [OSStructureField(Description = "Metadata associated with the decoded barcode")]
        public IEnumerable<Metadata> Metadata { get; set; } = [];

        [OSStructureField(DataType = OSDataType.BinaryData, Description = "Original image with a bounding rectangle on the detected barcode")]
        public byte[]? DetectedBarcode { get; set; } = null;

        public Barcode(Barcode b) : this()
        {
            Value = b.Value;
            RawBytes = b.RawBytes;
            Format = b.Format;
            Metadata = b.Metadata;
            DetectedBarcode = b.DetectedBarcode;
        }

        public Barcode(string v, byte[] b, string f, Metadata[] m, byte[]? i) : this()
        {
            Value = v;
            RawBytes = b;
            Format = f;
            Metadata = m;
            DetectedBarcode = i;
        }
    }
}