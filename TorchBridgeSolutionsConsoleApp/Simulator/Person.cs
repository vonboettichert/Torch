namespace TorchBridgeSolutionsConsoleApp.Simulator
{
    public class Person
    {
        public Person(string name, int timeToCrossBridge)
        {
            Name = name;
            TimeToCrossBridge = timeToCrossBridge;
        }

        public string Name { get; }
        public int TimeToCrossBridge { get; }

        public override string ToString()
        {
            return string.Format("{0}: Crossing time {1}.", Name, TimeToCrossBridge);
        }
    }
}