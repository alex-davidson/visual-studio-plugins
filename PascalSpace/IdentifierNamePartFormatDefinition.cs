using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace PascalSpace
{
    [Export(typeof(EditorFormatDefinition))]
    [Name("MarkerFormatDefinition/" + nameof(IdentifierNamePartFormatDefinition))]
    [UserVisible(true)]
    internal class IdentifierNamePartFormatDefinition : MarkerFormatDefinition
    {
        public IdentifierNamePartFormatDefinition()
        {
            // Default colour of identifier names: #FF000000
            BackgroundColor = ColourHelpers.BlendWithEditorBackground(Color.FromRgb(0x80, 0x80, 0x80));
            BackgroundCustomizable = true;
            DisplayName = "Identifier Name Part";
            ZOrder = 5;
        }
    }
}
