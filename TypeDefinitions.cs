using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnrealUiControllerGenerator {
    class TypeDefinitions {
        public static TypeDefinition[] Types { get; } = {
            new TypeDefinition("UVerticalBox"),
            new TypeDefinition("UVerticalBoxSlot"),
            new TypeDefinition("UHorizontalBox"),
            new TypeDefinition("UHorizontalBoxSlot"),
            new TypeDefinition("UTextBlock"),
            new TypeDefinition("UBorder"),
            new TypeDefinition("UBorderSlot"),
            new TypeDefinition("UButton"),
            new TypeDefinition("UCheckBox"),
            new TypeDefinition("UImage"),
            new TypeDefinition("UNamedSlot"),
            new TypeDefinition("UProgressBar"),
            new TypeDefinition("URadialSlider"),
            new TypeDefinition("URichTextBlock"),
            new TypeDefinition("USlider"),
            new TypeDefinition("UComboBox"),
            new TypeDefinition("UEditableText"),
            new TypeDefinition("UMultiLineEditableText"),
            new TypeDefinition("USpinBox"),
            new TypeDefinition("UEditableTextBox"),
            new TypeDefinition("UMultiLineEditableTextBox"),
            new TypeDefinition("UListView"),
            new TypeDefinition("UTileView"),
            new TypeDefinition("UTreeView"),
            new TypeDefinition("UExpandableArea"),
            new TypeDefinition("UCanvasPanel"),
            new TypeDefinition("UCanvasPanelSlot"),
            new TypeDefinition("UGridPanel"),
            new TypeDefinition("UGridPanelSlot"),
            new TypeDefinition("UOverlay"),
            new TypeDefinition("USafeZone"),
            new TypeDefinition("UScaleBox"),
            new TypeDefinition("UScrollBox"),
            new TypeDefinition("USizeBox"),
            new TypeDefinition("UStackBox"),
            new TypeDefinition("UUniformGridPanel"),
            new TypeDefinition("UWidgetSwitcher"),
            new TypeDefinition("UWrapBox"),
            new TypeDefinition("UCircularThrobber"),
            new TypeDefinition("UMenuAnchor"),
            new TypeDefinition("UNativeWidgetHost"),
            new TypeDefinition("USpacer"),
            new TypeDefinition("UThrobber"),
            new TypeDefinition("UBackgroundBlur")
        };

        static Dictionary<string, TypeDefinition> TypeToDefinitionDictionary { get; }

        static TypeDefinitions() {
            TypeDefinition[] types = Types;
            var definitionDictionary = new Dictionary<string, TypeDefinition>();
            foreach (TypeDefinition type in types) {
                definitionDictionary[type.TypeName] = type;
            }

            TypeToDefinitionDictionary = definitionDictionary;
        }

        public static String GetIncludeFor(string typeName) {
            if (TypeToDefinitionDictionary.ContainsKey(typeName))
                return TypeToDefinitionDictionary[typeName].IncludePath;
            return String.Empty;
        }
    }
}
