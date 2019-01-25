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
                return typeof(DefaultGreeterForHello);

            if (forType == typeof(HiController))
                return typeof(DefaultGreeterForHi);

            return null;
        }
    }
}
