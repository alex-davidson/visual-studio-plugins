using System.Linq;
using NUnit.Framework;
using PascalSpace.Engine;

namespace PascalSpace.UnitTests
{
    [TestFixture]
    public class PascalCasePartitionerTests
    {
        [TestCase("SymbolName", new [] { 6, 4 })]
        [TestCase("SymbolName345", new [] { 6, 4, 3 })]
        [TestCase("NameContainingUPPERCASEDPart", new [] { 4, 10, 10, 4 })]
        [TestCase("INameOfInterfaceA", new [] { 1, 4, 2, 9, 1 })]
        [TestCase("Number234abWithAlphaSuffix", new [] { 6, 5, 4, 5, 6 })]
        [TestCase("Name_with_underscores", new [] { 4, 1, 4, 1, 11 })]
        [TestCase("MixedNaming_WithPascalCase_AndUnderscores", new [] { 5, 6, 1, 4, 6, 4, 1, 3, 11 })]
        public void PartitionsCorrectly(string name, int[] lengths)
        {
            var partitioner = new PascalCasePartitioner();
            partitioner.Analyse(name);
            var partitionLengths = Enumerable.Range(0, partitioner.Count).Select(partitioner.GetLength).ToArray();
            Assert.That(partitionLengths, Is.EqualTo(lengths));
        }

    }
}
