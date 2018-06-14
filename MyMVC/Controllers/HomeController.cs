using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyMVC.Models;
using MyWebsite.Filters;

namespace MyMVC.Controllers {

    public class HomeController : Controller {

        [HttpGet]
        [Route ("api1/{p1}")]
        [Route ("api1")]
        public string api1 (string p1) {
            return p1;
        }

        public void Index () {
            throw new System.Exception ("This is exception sample from Index().");
        }

        [Route ("/api/test")]
        public string Test () {
            throw new System.Exception ("This is exception sample from Test().");
        }

        public IActionResult Error () {
            return View ();
        }
    }

}