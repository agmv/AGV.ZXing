using OutSystems.ExternalLibraries.SDK;

namespace AGV.ZXing.Structures
{

    [OSStructure(Description = "Defines a name by its components")]
    public struct ComposedName
    {
        [OSStructureField(Description = "First name", Length = 50)]
        public string FirstName { get; set; } = "";
        [OSStructureField(Description = "Last name", Length = 50)]
        public string LastName { get; set; } = "";
        [OSStructureField(Description = "Middle names", Length = 100)]
        public string MiddleNames { get; set; } = "";
        [OSStructureField(Description = "Name prefix", Length = 20)]
        public string Prefix { get; set; } = "";
        [OSStructureField(Description = "Name suffix", Length = 20)]
        public string Suffix { get; set; } = "";

        public ComposedName(string firstName, string lastName, string middleNames = "", string prefix = "", string suffix = "") : this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleNames = middleNames;
            this.Prefix = prefix;
            this.Suffix = suffix;
        }

        public ComposedName(ComposedName n) : this()
        {
            FirstName = n.FirstName;
            LastName = n.LastName;
            MiddleNames = n.MiddleNames;
            Prefix = n.Prefix;
            Suffix = n.Suffix;
        }
    }
}