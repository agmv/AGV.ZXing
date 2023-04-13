using OutSystems.ExternalLibraries.SDK;
using System.Collections.Generic;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Structure that holds decoded barcodes")]
    public struct Barcode {
        [OSStructureField(DataType = OSDataType.Text, Description = "Barcode decoded value")]
        public string value;

        [OSStructureField(DataType = OSDataType.BinaryData, Description = "Barcode raw bytes")]
        public byte[] rawBytes;
        
        [OSStructureField(DataType = OSDataType.Text, Description = "Barcode format")]
        public string format;

        //[OSStructureField(Description = "Metadata associated with the decoded barcode")]
        //public IEnumerable<Metadata> metadata;

        public Barcode(Structures.Barcode b):this() {
        value = b.value;
        rawBytes = b.rawBytes;
        format = b.format;
        // metadata = b.metadata;
        } 

        public Barcode(string v, byte[] b, string f, Metadata[] m):this() {
        value = v;
        rawBytes = b;
        format = f;
        // metadata = m;
        }
    }    
}