using OutSystems.Model.ExternalLibraries.SDK;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Barcode metadata", OriginalName = "Metadata")]
    public struct Metadata {
        [OSStructureField(IsMandatory = true, Description = "Barcode metadata key", OriginalName = "Key")]
        public string key;

        [OSStructureField(Description = "Barcode metadata value", OriginalName = "Value")]
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