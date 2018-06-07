using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMVC.Models;

// 只有特定的 Action 需要改路由，也可以只加 Action
// 如果 [Route] 是設定在 Action，路徑是由網站根路徑開始算。

// http://localhost:5000/user1/profile 會對應到 UserController 的 Profile()。
// http://localhost:5000/change-password 會對應到 UserController 的 ChangePassword()。
// http://localhost:5000/user1/other 會對應到 UserController 的 Other()。
namespace MyMVC.Controllers {
    public class User1Controller : Controller {

        public string Profile () {
            return "Hello User1Controller Profile";
        }

        [Route ("change-password")]
        public string ChangePassword () {
            return "Hello User1Controller ChangePassword";
        }

        public string Other () {
            return "Hello User1Controller Other";
        }
    }
}