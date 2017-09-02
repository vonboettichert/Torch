using System;
using System.Collections.Generic;

namespace TorchBridgeSolutionsConsoleApp.Simulator
{
    public class Bridge
    {
        private readonly List<Person> _personsOnLeftSide;
        private readonly List<Person> _personsOnRightSide;
        private string _crossingInformation;
        private int _crossingTime;

        public Bridge(List<Person> personsOnLeftSide)
        {
            _crossingTime = 0;
            _crossingInformation = string.Empty;
            _personsOnRightSide = new List<Person>();
            _personsOnLeftSide = new List<Person>();
            if (personsOnLeftSide != null || personsOnLeftSide.Count == 0)
                _personsOnLeftSide.AddRange(personsOnLeftSide);
            else
                throw new InvalidOperationException("There are no people that need to cross the bridge.");
        }

        public Bridge(List<Person> personsOnLeftSide, List<Person> personsOnRightSide, int crossingTime)
        {
            _crossingTime = crossingTime;
            _crossingInformation = string.Empty;
            _personsOnLeftSide = new List<Person>();
            if (personsOnLeftSide != null)
                _personsOnLeftSide.AddRange(personsOnLeftSide);

            _personsOnRightSide = new List<Person>();
            if (personsOnRightSide != null)
                _personsOnRightSide.AddRange(personsOnRightSide);
        }

        internal List<Person> GetPersonsOnLeftSide()
        {
            return _personsOnLeftSide;
        }

        internal List<Person> GetPersonsOnRightSide()
        {
            return _personsOnRightSide;
        }

        internal void TravelFromLeftToRightSide(Person firstPerson, Person secondPerson)
        {
            if (!_personsOnLeftSide.Contains(firstPerson))
                throw new InvalidOperationException(
                    string.Format("{0} is already on the right side.", firstPerson.Name));

            if (!_personsOnLeftSide.Contains(secondPerson))
                throw new InvalidOperationException(string.Format("{0} is already on the right side.",
                    secondPerson.Name));

            _personsOnLeftSide.Remove(firstPerson);
            _personsOnLeftSide.Remove(secondPerson);
            _personsOnRightSide.Add(firstPerson);
            _personsOnRightSide.Add(secondPerson);

            _crossingTime += Math.Max(firstPerson.TimeToCrossBridge, secondPerson.TimeToCrossBridge);

            var additionalInfo = string.Format("TravelRight: {0} & {1} (Total time: {2}min) \r\n", firstPerson.Name,
                secondPerson.Name, _crossingTime);
            _crossingInformation += additionalInfo;
        }

        internal void TravelFromRightToLeftSide(Person person)
        {
            if (!_personsOnRightSide.Contains(person))
                throw new InvalidOperationException(string.Format("{0} is already on the left side.", person.Name));

            _personsOnLeftSide.Add(person);
            _personsOnRightSide.Remove(person);

            _crossingTime += person.TimeToCrossBridge;

            var additionalInfo = string.Format("TravelLeft: {0} (Total time: {1}min) \r\n", person.Name, _crossingTime);
            _crossingInformation += additionalInfo;
        }

        internal int GetTotalCrossingTime()
        {
            return _crossingTime;
        }

        internal string GetCrossingInformation()
        {
            return _crossingInformation;
        }

        internal Bridge Clone()
        {
            var personsOnLeftSide = new List<Person>();
            var personsOnRightSide = new List<Person>();

            personsOnLeftSide.AddRange(_personsOnLeftSide);
            personsOnRightSide.AddRange(_personsOnRightSide);

            var newBridge =
                new Bridge(personsOnLeftSide, personsOnRightSide, _crossingTime) {_crossingInformation = _crossingInformation};

            return newBridge;
        }
    }
}