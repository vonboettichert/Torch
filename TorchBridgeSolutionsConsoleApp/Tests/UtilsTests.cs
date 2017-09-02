using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TorchBridgeSolutionsConsoleApp.Simulator;

namespace TorchBridgeSolutionsConsoleApp.Tests
{
    [TestFixture]
    public class UtilsTests
    {
        [SetUp]
        public void Setup()
        {
            _testList = new List<string> {"a", "b", "c"};
        }

        private List<string> _testList;

        [Test]
        public void Can_create_correct_permutations()
        {
            var firstResult = Utils.Permute(_testList, 1);
            var secondResult = Utils.Permute(_testList, 2);

            Assert.That(firstResult.Count() == 3);
            Assert.That(secondResult.Count() == 3);
            Assert.That(secondResult.Any(s => s.Contains("a") && s.Contains("b")));
            Assert.That(secondResult.Any(s => s.Contains("a") && s.Contains("c")));
            Assert.That(secondResult.Any(s => s.Contains("b") && s.Contains("c")));
        }
    }
}