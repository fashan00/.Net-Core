using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMVC.Models;
using MyWebsite.Filters;

namespace MyMVC.Controllers {

    public class HomeController : Controller {

        private static IDistributedCache _distributedCache;

        public HomeController (IDistributedCache distributedCache) {
            _distributedCache = distributedCache;
        }

        [HttpGet ("Index")]
        public IActionResult Index () {
            _distributedCache.Set ("Sample", ObjectToByteArray (new UserModel () {
                Id = 12,
                    Name = "John12"
            }));
            var model = ByteArrayToObject<UserModel> (_distributedCache.Get ("Sample"));
            return Ok (model);
        }

        private byte[] ObjectToByteArray (object obj) {
            var binaryFormatter = new BinaryFormatter ();
            using (var memoryStream = new MemoryStream ()) {
                binaryFormatter.Serialize (memoryStream, obj);
                return memoryStream.ToArray ();
            }
        }

        private T ByteArrayToObject<T> (byte[] bytes) {
            using (var memoryStream = new MemoryStream ()) {
                var binaryFormatter = new BinaryFormatter ();
                memoryStream.Write (bytes, 0, bytes.Length);
                memoryStream.Seek (0, SeekOrigin.Begin);
                var obj = binaryFormatter.Deserialize (memoryStream);
                return (T) obj;
            }
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