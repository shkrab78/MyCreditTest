using System.Web.Http;

namespace ControllersLib
{
    public class LibController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "asdfgasdfasdfasdf"; 
        }
    }
}
