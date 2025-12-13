using OutSystems.ExternalLibraries.SDK;

namespace AGV.ZXing.Structures
{

    [OSStructure(Description = "Barcode metadata")]
    public struct Metadata
    {
        [OSStructureField(IsMandatory = true, Description = "Barcode metadata key", Length = 50)]
        public string Key { get; set; } = "";

        [OSStructureField(Description = "Barcode metadata value", Length = 2000)]
        public string Value { get; set; } = "";

        public Metadata(Metadata m) : this()
        {
            Value = m.Value;
            Key = m.Key;
        }

        public Metadata(string k, string v) : this()
        {
            Key = k;
            Value = v;
        }
    }
}