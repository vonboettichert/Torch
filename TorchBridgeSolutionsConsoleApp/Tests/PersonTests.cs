using NUnit.Framework;
using TorchBridgeSolutionsConsoleApp.Simulator;

namespace TorchBridgeSolutionsConsoleApp.Tests
{
    [TestFixture]
    public class PersonTests
    {
        private Person GetSut()
        {
            return new Person("person", 5);
        }

        [Test]
        public void Can_create_sut()
        {
            Assert.DoesNotThrow(() => GetSut());
        }

        [Test]
        public void Can_override_toString_method_correctly()
        {
            var sut = GetSut();

            var result = sut.ToString();

            Assert.That(result == "person: Crossing time 5.");
        }

        [Test]
        public void Can_set_fields_correctly()
        {
            var sut = GetSut();

            Assert.That(sut.TimeToCrossBridge == 5);
            Assert.That(sut.Name == "person");
        }
    }
}