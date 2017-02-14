using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace PascalSpace
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType("text")]
    [TagType(typeof(SymbolNamePartTag))]
    internal class SymbolNamePartTaggerProvider : IViewTaggerProvider
    {
        [Import]
        internal IViewClassifierAggregatorService ViewClassifierAggregatorService { get; set; }

        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            //provide highlighting only on the top buffer
            if (textView.TextBuffer != buffer) return null;

            var viewClassifierAggregator = ViewClassifierAggregatorService.GetClassifier(textView);
            return new SymbolNamePartTagger(buffer, viewClassifierAggregator) as ITagger<T>;
        }
    }
}
