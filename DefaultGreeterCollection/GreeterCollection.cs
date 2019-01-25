using System;
using ControllersLib;
using IoC_Lib;

namespace DefaultGreeterCollection
{
    public class GreeterCollection : IGreeterLib
    {
        public Type GetGreeterType(Type forType)
        {
            if (forType == typeof(HelloController))
            {
                return typeof(DefaultGreeterForHello);
            }
            else if (forType == typeof(HiController))
            {
                return typeof(DefaultGreeterForHi);
            }
            else return null;
        }
    }
}
