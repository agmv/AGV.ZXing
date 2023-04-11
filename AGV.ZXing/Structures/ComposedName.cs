using OutSystems.Model.ExternalLibraries.SDK;
using System.Collections.Generic;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Defines a name by its components", OriginalName = "ComposedName")]
    public struct ComposedName {
        [OSStructureField(Description = "First name", OriginalName = "FirstName")]
        public string firstName;
        [OSStructureField(Description = "Last name", OriginalName = "LastName")]
        public string lastName;
        [OSStructureField(Description = "Middle names", OriginalName = "MiddleNames")]
        public string middleNames;
        [OSStructureField(Description = "Name prefix", OriginalName = "Prefix")]
        public string prefix;
        [OSStructureField(Description = "Name suffix", OriginalName = "Suffix")]
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