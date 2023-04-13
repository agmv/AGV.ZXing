using OutSystems.ExternalLibraries.SDK;
using System.Collections.Generic;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Defines a name by its components")]
    public struct ComposedName {
        [OSStructureField(Description = "First name", Length = 50)]
        public string firstName;
        [OSStructureField(Description = "Last name", Length = 50)]
        public string lastName;
        [OSStructureField(Description = "Middle names", Length = 100)]
        public string middleNames;
        [OSStructureField(Description = "Name prefix", Length = 20)]
        public string prefix;
        [OSStructureField(Description = "Name suffix", Length = 20)]
        public string suffix;

        public ComposedName(string firstName, string lastName, string middleNames = "", string prefix = "", string suffix = ""):this() {
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleNames = middleNames;
            this.prefix = prefix;
            this.suffix = suffix;
        }

        public ComposedName(ComposedName n):this() {
            this.firstName = n.firstName;
            this.lastName = n.lastName;
            this.middleNames = n.middleNames;
            this.prefix = n.prefix;
            this.suffix = n.suffix;
        }
    }
}