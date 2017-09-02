using System.Collections.Generic;
using NUnit.Framework;
using TorchBridgeSolutionsConsoleApp.Simulator;

namespace TorchBridgeSolutionsConsoleApp.Tests
{
    [TestFixture]
    public class CoordinatorTests
    {
        [SetUp]
        public void Setup()
        {
            var person1 = new Person("Adam", 1);
            var person2 = new Person("Bob", 2);
            var person3 = new Person("Clair", 3);
            var person4 = new Person("Dave", 4);
            _originalPersons = new List<Person>
            {
                person1,
                person2,
                person3,
                person4
            };
            _originalBridge = new Bridge(_originalPersons, null, 0);
        }

        private Bridge _originalBridge;
        private List<Person> _originalPersons;

        private Coordinator GetSut()
        {
            return new Coordinator(_originalBridge);
        }

        [Test]
        public void Can_create_sut()
        {
            Assert.DoesNotThrow(() => GetSut());
        }

        [Test]
        public void Can_get_shortestCrossingScenario()
        {
            var sut = GetSut();
            sut.RunSimulations();

            var result = sut.GetShortestCrossingScenario();

            Assert.IsNotEmpty(result);
        }

        [Test]
        public void Can_run_simulations()
        {
            var sut = GetSut();

            Assert.DoesNotThrow(() => sut.RunSimulations());
        }
    }
}