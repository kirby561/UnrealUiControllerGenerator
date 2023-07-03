using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnrealUiControllerGenerator {
    public class WidgetDeclaration {
        public string ClassName { get; }
        public string VariableName { get; }

        public WidgetDeclaration(string className, string variableName) {
            ClassName = className;
            VariableName = variableName;
        }

        public string ToString() {
            return ClassName + " " + VariableName;
        }
    }
}
