using Microsoft.AspNetCore.Mvc;

namespace MyWebAPI.Controllers {
    [Route ("api/[controller]")]
    public class HomeController : Controller {
        private readonly ISample _transient;
        private readonly ISample _scoped;
        private readonly ISample _singleton;

        public HomeController (
            ISampleTransient transient,
            ISampleScoped scoped,
            ISampleSingleton singleton) {
            _transient = transient;
            _scoped = scoped;
            _singleton = singleton;
        }

        [HttpGet]
        [Route ("index")]
        public IActionResult Index () {
            ViewBag.TransientId = _transient.Id;
            ViewBag.TransientHashCode = _transient.GetHashCode ();

            ViewBag.ScopedId = _scoped.Id;
            ViewBag.ScopedHashCode = _scoped.GetHashCode ();

            ViewBag.SingletonId = _singleton.Id;
            ViewBag.SingletonHashCode = _singleton.GetHashCode ();
            return View ();
        }
    }
}