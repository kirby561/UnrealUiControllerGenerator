using System.IO;
using System.Windows;
using System.Windows.Forms;
using WinForms = System.Windows.Forms;

namespace UnrealUiControllerGenerator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private const string WidgetNamePlaceholder = "[WIDGET_NAME]";
        private const string WidgetContentPathPlaceholder = "[WIDGET_CONTENT_PATH]";
        private const string ForwardDeclarationPlaceholder = "[FORWARD_DECLARATIONS_SECTION]";
        private const string PropertyDefinitionPlaceholder = "[PROPERTY_DEFINITION_SECTION]";
        private const string IncludesSectionPlaceholder = "[INCLUDES_SECTION]";
        private const string FindWidgetsSectionPlaceholder = "[FIND_WIDGETS_SECTION]";

        public MainWindow() {
            InitializeComponent();
        }

        private void OnCreateButtonClicked(object sender, RoutedEventArgs e) {
            if (_createRadioButton.IsChecked == true) {
                MakeCreateUiControllerFiles();
            } else if (_attachRadioButton.IsChecked == true) {
                MakeAttachedUiControllerFiles();
            } else if (_windowRadioButton.IsChecked == true) {
                MakeWindowUiControllerFiles();
            } else {
                WinForms.MessageBox.Show("Select a controller type!");
            }
        }

        private void OnDirectoryButtonClicked(object sender, RoutedEventArgs e) {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == WinForms.DialogResult.OK) {
                _outputDirectory.Text = dialog.SelectedPath;
            }
        }

        private string ReadTemplateFile(string templateName) {
            string currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            string templateFilePath = currentDirectory + "\\Templates\\" + templateName;
            return File.ReadAllText(templateFilePath);
        }

        private void MakeWindowUiControllerFiles() {
            string templateHeader = ReadTemplateFile("WindowHeaderTemplate.h");
            string templateCpp = ReadTemplateFile("WindowHeaderTemplate.cpp");

            CreateControllerFiles(templateHeader, templateCpp);
        }

        private void MakeAttachedUiControllerFiles() {
            string templateHeader = ReadTemplateFile("AttachHeaderTemplate.h");
            string templateCpp = ReadTemplateFile("AttachHeaderTemplate.cpp");

            CreateControllerFiles(templateHeader, templateCpp);
        }

        private void MakeCreateUiControllerFiles() {
            string templateHeader = ReadTemplateFile("CreateHeaderTemplate.h");
            string templateCpp = ReadTemplateFile("CreateHeaderTemplate.cpp");

            CreateControllerFiles(templateHeader, templateCpp);
        }

        private void CreateControllerFiles(string templateHeader, string templateCpp) {
            // Parse the input
            UnrealWidgetParser parser = new UnrealWidgetParser();
            parser.Parse(_inputTextBox.Text);

            string outputDirectory = _outputDirectory.Text;

            string widgetContentReference = _widgetReferencePathText.Text;
            string widgetName = _widgetNameText.Text;
            string outputFileHeaderName = outputDirectory + "\\" + widgetName + ".h";
            string outputFileCppName = outputDirectory + "\\" + widgetName + ".cpp";

            string propertyDefinitions = parser.GetPropertyDefinitionsSection();
            string forwardDeclarations = parser.GetForwardDeclarations();

            string outputHeader = templateHeader
                .Replace(WidgetNamePlaceholder, widgetName)
                .Replace(ForwardDeclarationPlaceholder, forwardDeclarations)
                .Replace(PropertyDefinitionPlaceholder, propertyDefinitions);

            string findWidgetsSection = parser.GetFindWidgetsSection();
            string includesSection = parser.GetIncludes();

            string outputCpp = templateCpp
                .Replace(WidgetNamePlaceholder, widgetName)
                .Replace(WidgetContentPathPlaceholder, widgetContentReference)
                .Replace(FindWidgetsSectionPlaceholder, findWidgetsSection)
                .Replace(IncludesSectionPlaceholder, includesSection);
            File.WriteAllText(outputFileHeaderName, outputHeader);
            File.WriteAllText(outputFileCppName, outputCpp);

            WinForms.MessageBox.Show("Controller written to: \n" + outputFileHeaderName + "\n" + outputFileCppName);
        }
    }
}
