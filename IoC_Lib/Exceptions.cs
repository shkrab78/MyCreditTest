using System;

namespace IoC_Lib
{
    [Serializable]
    public class IoCException : Exception
    {
        public IoCException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
