
namespace Laboratory.EventTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "Event"; }
        }

        public void Run()
        {
            var cat = new Cat();
            var mouse = new Mouse();
            cat.cry += mouse.Run;

            var people = new People();
            cat.cry += people.WakeUp;

            cat.Cryed();
        }
    }
}
