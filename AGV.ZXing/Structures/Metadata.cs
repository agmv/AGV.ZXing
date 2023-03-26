using OutSystems.Model.ExternalLibraries.SDK;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Barcode metadata")]
    public struct Metadata {
        [OSStructureField(DataType = OSDataType.Text, Description = "Barcode metadata key")]
        public string key;

        [OSStructureField(DataType = OSDataType.Text, Description = "Barcode metadata value")]
        public string value;

       public Metadata(Structures.Metadata m):this() {
        value = m.value;
        key = m.key;
       }

       public Metadata (string k, string v):this() {
        key = k;
        value = v;
       } 
    }    
}