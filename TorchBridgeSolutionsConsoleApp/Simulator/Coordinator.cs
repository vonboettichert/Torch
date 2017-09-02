using System.Collections.Generic;
using System.Linq;

namespace TorchBridgeSolutionsConsoleApp.Simulator
{
    public class Coordinator
    {
        private readonly List<Bridge> _lastBridgeScenarios;

        public Coordinator(Bridge bridge)
        {
            _lastBridgeScenarios = new List<Bridge> {bridge};
        }

        private bool AllPersonsAreOnRightSide()
        {
            return _lastBridgeScenarios.Any(cbs => cbs.GetPersonsOnLeftSide().Count == 0);
        }

        internal void RunSimulations()
        {
            while (!AllPersonsAreOnRightSide())
            {
                MoveAcrossLeftToRight();
                if (!AllPersonsAreOnRightSide())
                    MoveAcrossRightToLeft();
            }
        }

        internal string[] GetShortestCrossingScenario()
        {
            var shortestCrossingScenario = _lastBridgeScenarios.Select(l => l.GetTotalCrossingTime()).Min();
            var shortestCrossingOrder =
                _lastBridgeScenarios.Where(l => l.GetTotalCrossingTime() == shortestCrossingScenario).ToArray();

            return shortestCrossingOrder.Select(s => s.GetCrossingInformation()).ToArray();
        }

        private void MoveAcrossLeftToRight()
        {
            var currentBridgeScenarios = new List<Bridge>();
            foreach (var bridgeScenario in _lastBridgeScenarios)
            {
                var personsOnLeftSide = bridgeScenario.GetPersonsOnLeftSide();
                var personCombinations = Utils.Permute(personsOnLeftSide, 2);
                foreach (var personCombination in personCombinations)
                {
                    var newBridge = bridgeScenario.Clone();
                    newBridge.TravelFromLeftToRightSide(personCombination[0], personCombination[1]);
                    currentBridgeScenarios.Add(newBridge);
                }
            }
            _lastBridgeScenarios.Clear();
            _lastBridgeScenarios.AddRange(currentBridgeScenarios);
        }

        private void MoveAcrossRightToLeft()
        {
            var currentBridgeScenarios = new List<Bridge>();
            foreach (var bridgeScenario in _lastBridgeScenarios)
            {
                var personsOnRightSide = bridgeScenario.GetPersonsOnRightSide();
                foreach (var person in personsOnRightSide)
                {
                    var newBridge = bridgeScenario.Clone();
                    newBridge.TravelFromRightToLeftSide(person);
                    currentBridgeScenarios.Add(newBridge);
                }
            }
            _lastBridgeScenarios.Clear();
            _lastBridgeScenarios.AddRange(currentBridgeScenarios);
        }
    }
}