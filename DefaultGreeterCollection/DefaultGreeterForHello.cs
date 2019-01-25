using IoC_Lib;

namespace DefaultGreeterCollection
{
    public class DefaultGreeterForHello : IGreeter
    {
        public string SayHello()
        {
            return "Hi there!";
        }
    }
}
