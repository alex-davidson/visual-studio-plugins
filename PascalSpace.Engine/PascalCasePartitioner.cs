using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.Text;

namespace PascalSpace.Engine
{
    public class PascalCasePartitioner
    {
        private readonly List<int> offsets = new List<int>(32);
        
        public int Count => offsets.Count - 1;

        public SnapshotSpan GetSubSpan(SnapshotSpan span, int index)
        {
            Debug.Assert(index < Count);
            var start = offsets[index];
            var end = offsets[index + 1];
            return new SnapshotSpan(span.Snapshot, span.Start.Position + start, end - start);
        }

        public int GetOffset(int index)
        {
            Debug.Assert(index < Count);
            return offsets[index];
        }

        public int GetLength(int index)
        {
            Debug.Assert(index < Count);
            return offsets[index + 1] - offsets[index];
        }

        public void Analyse(string symbolName)
        {
            offsets.Clear();
            offsets.Add(0);
            var lastOffset = 0;
            var prev = symbolName[0];
            for (var i = 1; i < symbolName.Length; i++)
            {
                var current = symbolName[i];
                if (IsBoundary(prev, current))
                {
                    offsets.Add(i);
                    lastOffset = i;
                }
                else if (lastOffset != i - 1)
                {
                    if (IsPastBoundary(prev, current))
                    {
                        offsets.Add(i - 1);
                        lastOffset = i - 1;
                    }
                }
                prev = current;
            }

            offsets.Add(symbolName.Length);
        }

        private static bool IsBoundary(char prev, char current)
        {
            if (char.IsLetterOrDigit(prev) ^ char.IsLetterOrDigit(current)) return true;
            if (char.IsDigit(prev))
            {
                return char.IsUpper(current);
            }
            if (!char.IsUpper(prev))
            {
                return !char.IsLower(current);
            }
            return false;
        }

        private static bool IsPastBoundary(char prev, char current)
        {
            if (char.IsUpper(prev))
            {
                if (char.IsLower(current)) return true;
            }
            return false;
        }
    }
}
