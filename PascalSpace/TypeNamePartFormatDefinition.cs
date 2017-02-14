using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace PascalSpace
{
    [Export(typeof(EditorFormatDefinition))]
    [Name("MarkerFormatDefinition/" + nameof(TypeNamePartFormatDefinition))]
    [UserVisible(true)]
    internal class TypeNamePartFormatDefinition : MarkerFormatDefinition
    {
        public TypeNamePartFormatDefinition()
        {
            // Default colour of type names: #FF2B91AF
            BackgroundColor = ColourHelpers.BlendWithEditorBackground(Color.FromRgb(0x2b, 0x91, 0xaf));
            BackgroundCustomizable = true;
            DisplayName = "Type Name Part";
            ZOrder = 5;
        }
    }
}
