using System;
using System.Collections.Generic;
using TorchBridgeSolutionsConsoleApp.Simulator;

namespace TorchBridgeSolutionsConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            var person1 = new Person("Adam", 5);
            var person2 = new Person("Bob", 10);
            var person3 = new Person("Clair", 20);
            var person4 = new Person("Dave", 25);

            var persons = new List<Person>
            {
                person1,
                person2,
                person3,
                person4
            };

            var bridge = new Bridge(persons);
            var coordinator = new Coordinator(bridge);

            Console.WriteLine("Torch Bridge Problem Simulator: \r\n");
            Console.Write("The following persons want to cross the bridge: \r\n");
            foreach (var person in persons)
                Console.WriteLine(person);
            Console.Write("\r\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine("\r\nRunning bridge crossing simulations...\r\n");
            coordinator.RunSimulations();
            Console.WriteLine("The shortest crossing time(s):\r\n");
            var results = coordinator.GetShortestCrossingScenario();
            for (var i = 0; i < results.Length; i++)
            {
                Console.WriteLine("Result " + i + ":\r\n");
                Console.WriteLine(results[i] + "\r\n");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}