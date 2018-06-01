using Microsoft.AspNetCore.Mvc;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ISample _sample;

        public HomeController(ISample sample)
        {
            _sample = sample;
        }

        [HttpGet]
        [Route("index")]
        public string Index() {
            return $"[ISample]\r\n"
                 + $"Id: {_sample.Id}\r\n"
                + $"HashCode: {_sample.GetHashCode()}\r\n"
                + $"Tpye: {_sample.GetType()}";
        }
    }
}