using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMVC.Models;

// 預設 RouteAttribute 的優先順序高於 Startup 註冊的 MapRoute
// 若 Controller 設定了 [Route]，Action 就要跟著加 [Route]，不然會發生錯誤。

// http://localhost:5000/user 會對應到 UserController 的 Profile()。
// http://localhost:5000/user/change-password 會對應到 UserController 的 ChangePassword()。
// http://localhost:5000/user/other 會對應到 UserController 的 Other()。
namespace MyMVC.Controllers {
    [Route ("[controller]")]
    public class UserController : Controller {
        [Route ("")]
        public string Profile () {
            return "Hello UserController Profile";
        }

        [Route ("change-password")]
        public string ChangePassword () {
            return "Hello UserController ChangePassword";
        }

        [Route ("[action]")]
        public string Other () {
            return "Hello UserController Other";
        }
    }
}