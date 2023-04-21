using OutSystems.ExternalLibraries.SDK;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Barcode metadata")]
    public struct Metadata {
        [OSStructureField(IsMandatory = true, Description = "Barcode metadata key", Length = 50)]
        public string key;

        [OSStructureField(Description = "Barcode metadata value", Length = 2000)]
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