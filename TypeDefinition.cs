using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnrealUiControllerGenerator {
    public class TypeDefinition {
        public string TypeName { get; set; }
        public string IncludePath { get; set; }

        public TypeDefinition(string typeName) {
            TypeName = typeName;
            IncludePath = "Components/" + typeName.Remove(0, 1) + ".h";
        }

        public TypeDefinition(string typeName, string includePath) {
            TypeName = typeName;
            IncludePath = includePath;
        }
    }
}
