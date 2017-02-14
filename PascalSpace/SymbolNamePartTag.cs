using System;
using Microsoft.VisualStudio.Text.Tagging;

namespace PascalSpace
{
    internal class SymbolNamePartTag : TextMarkerTag
    {
        public SymbolNamePartTag(Type formatDefinitionType) : base($"MarkerFormatDefinition/{formatDefinitionType.Name}")
        {
        }
    }
}
