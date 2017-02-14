using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using PascalSpace.Engine;

namespace PascalSpace
{
    internal class SymbolNamePartTagger : ITagger<SymbolNamePartTag>
    {
        private readonly ITextBuffer buffer;
        private readonly IClassifier viewClassifier;

        public SymbolNamePartTagger(ITextBuffer buffer, IClassifier viewClassifier)
        {
            this.buffer = buffer;
            this.viewClassifier = viewClassifier;

            viewClassifier.ClassificationChanged += ClassificationChanged;
        }

        private void ClassificationChanged(object sender, ClassificationChangedEventArgs e)
        {
            TagsChanged?.Invoke(this, new SnapshotSpanEventArgs(e.ChangeSpan));
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<SymbolNamePartTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            if (viewClassifier == null) yield break;
            if (spans == null) yield break;
            if (spans.Count == 0) yield break;

            var partitioner = new PascalCasePartitioner();

            foreach (var snapshotSpan in spans)
            {
                Debug.Assert(snapshotSpan.Snapshot.TextBuffer == buffer);
                foreach (var classificationSpan in viewClassifier.GetClassificationSpans(snapshotSpan))
                {
                    var tag = GetTagForSymbolType(classificationSpan.ClassificationType);
                    if (tag == null) continue;

                    partitioner.Analyse(classificationSpan.Span.GetText());
                    if (partitioner.Count <= 1) continue;

                    for (var i = 1; i < partitioner.Count; i += 2)
                    {
                        var subSpan = partitioner.GetSubSpan(classificationSpan.Span, i);
                        yield return new TagSpan<SymbolNamePartTag>(subSpan, tag);
                    }
                }
            }
        }

        private readonly SymbolNamePartTag typeNameTag = new SymbolNamePartTag(typeof(TypeNamePartFormatDefinition));
        private readonly SymbolNamePartTag identifierNameTag = new SymbolNamePartTag(typeof(IdentifierNamePartFormatDefinition));

        private SymbolNamePartTag GetTagForSymbolType(IClassificationType type)
        {
            if (type.IsOfType("type parameter name")) return typeNameTag;
            if (type.IsOfType("class name")) return typeNameTag;
            if (type.IsOfType("interface name")) return typeNameTag;
            if (type.IsOfType("struct name")) return typeNameTag;

            if (type.IsOfType("identifier")) return identifierNameTag;

            return null;
        }
    }
}
