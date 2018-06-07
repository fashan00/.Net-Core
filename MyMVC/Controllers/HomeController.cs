using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyMVC.Models;

namespace MyMVC.Controllers {
    public class HomeController : Controller {

        // Route: http://localhost:5000/Home/Index/2
        // Query: http://localhost:5000/Home/Index?id=1
        // Form(Body) > Route > Query
        public IActionResult Index (string id) {
            return Content ($"id: {id}");
        }
        // PostMan
        // http://localhost:5000/Home/FirstSample/custom-route?query=custom-query
        // Headers(key:Value): {header:custom-header}
        // Body(key:Value): x-www-form-urlencoded {form: custom-form} 
        public IActionResult FirstSample (
            [FromHeader] string header, [FromForm] string form, [FromRoute] string id, [FromQuery] string query) {
            return Content ($"header: {header}, form: {form}, id: {id}, query: {query}");
        }

        //http://localhost:5000/Home/DISample
        public IActionResult DISample ([FromServices] ILogger<HomeController> logger) {
            return Content ($"logger is null? {logger == null}");
        }

        public IActionResult BodySample ([FromBody] UserModel model) {
            // 由於 Id 是 int 型別，int 預設為 0
            // 雖然有帶上 [Required]，但不是 null 所以算是有值。
            if (model != null && model.Id < 1) {
                ModelState.AddModelError ("Id", "Id not exist");
            }
            if (ModelState.IsValid) {
                return Ok (model);
            }
            return BadRequest (ModelState);
        }

    }

}