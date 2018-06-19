using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMVC.Models;
using MyWebsite.Filters;

namespace MyMVC.Controllers {

    public class HomeController : Controller {

        private static IMemoryCache _memoryCache;

        public HomeController (IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        [HttpGet ("Index")]
        public IActionResult Index () {
            _memoryCache.Set ("Sample", new UserModel () {
                Id = 11,
                    Name = "John11"
            });
            var model = _memoryCache.Get<UserModel> ("Sample");
            return Ok (model);
        }

        [HttpGet]
        [Route ("api1/{p1}")]
        [Route ("api1")]
        public string api1 (string p1) {
            return p1;
        }

        public IActionResult Error () {
            return View ();
        }
    }

}