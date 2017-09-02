using System;
using System.Collections.Generic;
using NUnit.Framework;
using TorchBridgeSolutionsConsoleApp.Simulator;

namespace TorchBridgeSolutionsConsoleApp.Tests
{
    [TestFixture]
    public class BridgeTests
    {
        [SetUp]
        public void Setup()
        {
            var person1 = new Person("person1", 1);
            var person2 = new Person("person2", 2);
            var person3 = new Person("person3", 3);
            var person4 = new Person("person4", 4);
            var person5 = new Person("person5", 5);
            _persons = new List<Person>
            {
                person1,
                person2,
                person3,
                person4,
                person5
            };
        }

        private List<Person> _persons;

        private Bridge GetSut()
        {
            return new Bridge(_persons, null, 0);
        }

        [Test]
        public void Can_clone_bridge_correctly()
        {
            var sut = GetSut();
            sut.TravelFromLeftToRightSide(_persons[0], _persons[1]);
            sut.TravelFromRightToLeftSide(_persons[0]);

            var newSut = sut.Clone();

            Assert.That(sut.GetCrossingInformation() == newSut.GetCrossingInformation());
            Assert.That(sut.GetTotalCrossingTime() == newSut.GetTotalCrossingTime());
            Assert.That(sut.GetPersonsOnLeftSide().Count == newSut.GetPersonsOnLeftSide().Count);
            Assert.That(sut.GetPersonsOnRightSide().Count == newSut.GetPersonsOnRightSide().Count);
        }

        [Test]
        public void Can_create_sut()
        {
            Assert.DoesNotThrow(() => GetSut());
        }

        [Test]
        public void Can_get_crossingOrder_correctly()
        {
            var sut = GetSut();

            sut.TravelFromLeftToRightSide(_persons[0], _persons[1]);
            sut.TravelFromRightToLeftSide(_persons[0]);

            var result = sut.GetCrossingInformation();

            Assert.That(
                result ==
                "TravelRight: person1 & person2 (Total time: 2) \r\nTravelLeft: person1 (Total time: 3) \r\n");
        }

        [Test]
        public void Can_get_personsOnLeftSide_correctly()
        {
            var sut = GetSut();

            var result = sut.GetPersonsOnLeftSide();

            Assert.That(result.Count == 5);
        }

        [Test]
        public void Can_get_personsOnRightSide_correctly()
        {
            var sut = GetSut();

            var result = sut.GetPersonsOnRightSide();

            Assert.That(result.Count == 0);
        }

        [Test]
        public void Can_move_person_from_left_to_right()
        {
            var sut = GetSut();

            sut.TravelFromLeftToRightSide(_persons[0], _persons[1]);

            var personsOnLeftSide = sut.GetPersonsOnLeftSide();
            var personsOnRightSide = sut.GetPersonsOnRightSide();

            Assert.That(personsOnLeftSide.Count == 3);
            Assert.That(personsOnRightSide.Count == 2);
            Assert.That(personsOnRightSide[0].Name == "person1");
            Assert.That(personsOnRightSide[1].Name == "person2");
        }

        [Test]
        public void Can_move_person_from_right_to_left()
        {
            var sut = GetSut();

            sut.TravelFromLeftToRightSide(_persons[0], _persons[1]);
            sut.TravelFromRightToLeftSide(_persons[0]);

            var personsOnLeftSide = sut.GetPersonsOnLeftSide();
            var personsOnRightSide = sut.GetPersonsOnRightSide();

            Assert.That(personsOnLeftSide.Count == 4);
            Assert.That(personsOnRightSide.Count == 1);
            Assert.That(personsOnRightSide[0].Name == "person2");
        }

        [Test]
        public void Can_record_totalCrossingTime_correctly()
        {
            var sut = GetSut();

            sut.TravelFromLeftToRightSide(_persons[0], _persons[1]);
            sut.TravelFromRightToLeftSide(_persons[0]);
            var result = sut.GetTotalCrossingTime();
            Assert.That(result == 3);
        }

        [Test]
        public void Throws_if_trying_to_move_invalid_person_fromLeftToRight()
        {
            var sut = GetSut();

            sut.TravelFromLeftToRightSide(_persons[0], _persons[1]);

            Assert.Throws<InvalidOperationException>(() => sut.TravelFromLeftToRightSide(_persons[0], _persons[1]));
            Assert.Throws<InvalidOperationException>(() => sut.TravelFromLeftToRightSide(_persons[2], _persons[1]));
        }

        [Test]
        public void Throws_if_trying_to_move_invalid_person_fromRightToLeft()
        {
            var sut = GetSut();

            sut.TravelFromLeftToRightSide(_persons[0], _persons[1]);
            sut.TravelFromRightToLeftSide(_persons[0]);

            Assert.Throws<InvalidOperationException>(() => sut.TravelFromRightToLeftSide(_persons[0]));
        }
    }
}