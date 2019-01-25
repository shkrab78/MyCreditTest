using System;
using System.Web.Http;
using IoC_Lib;

namespace ControllersLib
{
    public abstract class MyTestController : ApiController
    {
        private readonly IGreeter _greeter;

        protected MyTestController()
        {
            try
            {
                _greeter = GreeterFactory.Create(GetType());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        public string Get()
        {
            return _greeter?.SayHello(); 
        }
    }
}
