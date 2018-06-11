using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyMVC.Models;

namespace MyMVC.Controllers {
    [Route ("api/[controller]")]
    public class HomeController : Controller {

        private readonly ISessionWapper _sessionWapper;

        public HomeController (ISessionWapper sessionWapper) {
            _sessionWapper = sessionWapper;
            _sessionWapper.User = new UserModel () {
                Id = 1,
                Name = "John",
                Email = "john@mail.com"
            };
        }

        [HttpGet]
        [Route ("Index")]
        public IActionResult Index () {
            var user = _sessionWapper.User;
            _sessionWapper.User = user;
            return Ok (user);
        }

        [HttpGet]
        [Route ("api1/{p1}")]
        [Route ("api1")]
        public string api1 (string p1) {
            return p1;
        }
    }

}