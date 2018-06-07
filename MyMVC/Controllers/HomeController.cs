using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMVC.Models;

namespace MyMVC.Controllers {
    public class HomeController : Controller {
        // public IActionResult Index()
        // {
        //     return View();
        // }

        // public IActionResult About()
        // {
        //     ViewData["Message"] = "Your application description page.";

        //     return View();
        // }

        // public IActionResult Contact()
        // {
        //     ViewData["Message"] = "Your contact page.";

        //     return View();
        // }

        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }

        public IActionResult Index () {
            var user = new UserModel ();
            return View (model: user);
        }

        public string About () {
            return "Hello About";
        }

        public string Test () {
            return "Hello Test";
        }

        [HttpGet ("home/api1/{p1}")]
        public string api1 (string p1) {
            return p1;
        }

        public string api2 (string p1, string p2, string p3) {
            return $"p1={p1}, p2={p2}, p3={p3}";
        }

    }
}