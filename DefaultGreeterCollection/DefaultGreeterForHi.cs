using IoC_Lib;

namespace DefaultGreeterCollection
{
    public class DefaultGreeterForHi : IGreeter
    {
        public string SayHello()
        {
            return "Hi everyone!";
        }
    }
}
