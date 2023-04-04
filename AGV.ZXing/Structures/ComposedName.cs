using OutSystems.Model.ExternalLibraries.SDK;
using System.Collections.Generic;

namespace AGV.ZXing.Structures {

    [OSStructure(Description = "Defines a name by its components")]
    public struct ComposedName {
        [OSStructureField(DataType = OSDataType.Text, Description = "First name")]
        public string firstName;
        [OSStructureField(DataType = OSDataType.Text, Description = "Last name")]
        public string lastName;
        [OSStructureField(DataType = OSDataType.Text, Description = "Middle names")]
        public string middleNames;
        [OSStructureField(DataType = OSDataType.Text, Description = "Name prefix")]
        public string prefix;
        [OSStructureField(DataType = OSDataType.Text, Description = "Name suffix")]
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