using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UnrealUiControllerGenerator {
    public class UnrealWidgetParser {
        private string _input;

        // Output
        private string _includesSection;
        private string _forwardDeclarationsSection;
        private string _propertyDefinitionsSection;
        private string _findWidgetsSection;

        public UnrealWidgetParser() {
        }

        public void Parse(string input) {
            _input = input;

            List<WidgetDeclaration> widgetDeclarations = new List<WidgetDeclaration>();

            // Get all the lines that have object definitions
            using (StringReader reader = new StringReader(input)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    string trimmedLine = line.Trim();
                    string linePrefix = "Begin Object Class=";
                    if (trimmedLine.StartsWith(linePrefix)) {
                        // Get the class
                        string withoutPrefix = trimmedLine.Substring(linePrefix.Length);

                        // The full class name is everything up to the first space
                        int spaceIndex = withoutPrefix.IndexOf(" ");
                        string fullClassName = withoutPrefix.Substring(0, spaceIndex);
                        string className = fullClassName;
                        string umgClassPrefix = "/Script/UMG.";
                        string umgEditorClassPrefix = "/Script/UMGEditor.";
                        bool isUmgClass = false;
                        if (className.StartsWith(umgClassPrefix)) {
                            // UMG class in the form "/Script/UMG.HorizontalBoxSlot"
                            className = className.Replace(umgClassPrefix, "U");
                            isUmgClass = true;
                        } else if (className.StartsWith(umgEditorClassPrefix)) {
                            // UMG Editor class in the form "/Script/UMGEditor.HorizontalBoxSlot"
                            className = className.Replace(umgEditorClassPrefix, "U");
                            isUmgClass = true;
                        } else {
                            // Custom blueprint class in the form "/Game/Editor/UI/WBP_MidLineGraphic.WBP_MidLineGraphic_C"
                            int lastSlashIndex = className.LastIndexOf("/");
                            className = className.Substring(lastSlashIndex + 1);

                            // Remove the .WBP... suffix
                            int lastDotIndex = className.LastIndexOf(".");
                            className = className.Substring(0, lastDotIndex);
                        }
                        
                        string remainingString = withoutPrefix.Substring(spaceIndex + 1);
                        string namePrefix = "Name=";
                        string variableNameWithQuotes = remainingString.Substring(namePrefix.Length);
                        string variableName = variableNameWithQuotes.Substring(1, variableNameWithQuotes.Length - 2);

                        // If the Name with any _# suffix removed is the same as the type, then it's auto-generated and we should ignore it
                        string autoName = className;
                        if (isUmgClass)
                            autoName = autoName.Substring(1); // Remove the U prefix because that's not in the auto name
                        string autoNameSlash = autoName + "_";

                        // If the name starts with autoName, then it's probably auto generated and we should ignore it
                        if (!variableName.StartsWith(autoNameSlash) && !variableName.Equals(autoName)) {
                            widgetDeclarations.Add(new WidgetDeclaration(className, variableName));
                        }                        
                    }
                }
            }

            // Fill out the property definitions/linking sections and make a set of the class names
            HashSet<string> classNames = new HashSet<string>();
            foreach (WidgetDeclaration widgetDeclaration in widgetDeclarations) {
                string className = widgetDeclaration.ClassName;
                classNames.Add(className);

                string variableName = widgetDeclaration.VariableName;
                string firstCharacter = variableName.Substring(0, 1);
                string memberName = "_" + firstCharacter.ToLower() + variableName.Substring(1);
                _propertyDefinitionsSection += "\n\tUPROPERTY()\n\t" + className + "* " + memberName + " = nullptr;\n";

                String findWidgetStatement = "\t" + memberName + " = Cast<" + className + ">(_rootWidget->WidgetTree->FindWidget(TEXT(\"" + variableName + "\")));\n";
                _findWidgetsSection += findWidgetStatement;
            }

            // Remove the last newline
            _findWidgetsSection = _findWidgetsSection.Substring(0, _findWidgetsSection.Length - 1);
            _propertyDefinitionsSection = _propertyDefinitionsSection.Substring(0, _propertyDefinitionsSection.Length - 1);

            // Fill out the forward declarations/includes
            _forwardDeclarationsSection = "";
            foreach (string className in classNames) {
                _forwardDeclarationsSection += "class " + className + ";\n";
                _includesSection += "#include \"" + TypeDefinitions.GetIncludeFor(className) + "\"\n";
            }
            _forwardDeclarationsSection = _forwardDeclarationsSection.Substring(0, _forwardDeclarationsSection.Length - 1);
            _includesSection = _includesSection.Substring(0, _includesSection.Length - 1);
        }

        public string GetIncludes() {
            return _includesSection;
        }

        public string GetForwardDeclarations() {
            return _forwardDeclarationsSection;
        }
        
        public string GetPropertyDefinitionsSection() {
            return _propertyDefinitionsSection;
        }

        public string GetFindWidgetsSection() {
            return _findWidgetsSection;
        }        
    }
}
