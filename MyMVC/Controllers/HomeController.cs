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

    [ActionFilter (Name = "Controller", Order = 2)]
    [AuthorizationFilter]
    [Route ("api/[controller]")]
    public class HomeController : Controller {

        // private readonly ISessionWapper _sessionWapper;

        // public HomeController (ISessionWapper sessionWapper) {
        //     _sessionWapper = sessionWapper;
        //     _sessionWapper.User = new UserModel () {
        //         Id = 1,
        //         Name = "John",
        //         Email = "john@mail.com"
        //     };
        // }

        // [HttpGet]
        // [Route ("Index")]
        // public IActionResult Index () {
        //     var user = _sessionWapper.User;
        //     _sessionWapper.User = user;
        //     return Ok (user);
        // }

        [HttpGet]
        [Route ("api1/{p1}")]
        [Route ("api1")]
        public string api1 (string p1) {
            return p1;
        }

        [HttpGet ("Hello")]
        // 使用ActionFilter
        [TypeFilter (typeof (ActionFilter))]
        public void Hello () {
            Response.WriteAsync ("Hello World! \r\n");
        }

        [HttpGet ("Error")]
        // ActionFilter有繼承Attribute
        [ActionFilter]
        public void Error () {
            throw new System.Exception ("Error");
        }

        [HttpGet ("Order")]
        [ActionFilter (Name = "Action", Order = 1)]
        public void Order () {
            Response.WriteAsync ("Hello World! \r\n");
        }
    }

}