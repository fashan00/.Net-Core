using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMVC.Models;
using MyWebsite.Filters;
using MyWebsite.Helpers;

namespace MyMVC.Controllers {
    [Route ("api/[controller]s")]
    public class FileController : Controller {
        private readonly static Dictionary<string, string> _contentTypes = new Dictionary<string, string> { { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif", "image/gif" }
        };
        private readonly string _folder;

        public FileController (IHostingEnvironment env) {
            // 把上傳目錄設為：wwwroot\UploadFolder
            _folder = $@"{env.WebRootPath}\UploadFolder";
        }

        [HttpPost]
        public async Task<IActionResult> Upload (List<IFormFile> files) {
            var size = files.Sum (f => f.Length);

            var downloadPath = new List<string> ();
            foreach (var file in files) {
                if (file.Length > 0) {
                    var path = $@"{_folder}\{file.FileName}";
                    downloadPath.Add ("http://localhost:5000/api/files/" + file.FileName);
                    using (var stream = new FileStream (path, FileMode.Create)) {
                        await file.CopyToAsync (stream);
                    }
                }
            }

            return Ok (new { count = files.Count, size, downloadPath });
        }

        [HttpGet ("{fileName}")]
        public async Task<IActionResult> Download (string fileName) {
            if (string.IsNullOrEmpty (fileName)) {
                return NotFound ();
            }

            var path = $@"{_folder}\{fileName}";
            var memoryStream = new MemoryStream ();
            using (var stream = new FileStream (path, FileMode.Open)) {
                await stream.CopyToAsync (memoryStream);
            }
            memoryStream.Seek (0, SeekOrigin.Begin);

            // 回傳檔案到 Client 需要附上 Content Type，否則瀏覽器會解析失敗。
            return new FileStreamResult (memoryStream, _contentTypes[Path.GetExtension (path).ToLowerInvariant ()]);
        }

        [Route ("album")]
        [HttpPost]
        [DisableFormValueModelBindingFilter]
        public async Task<IActionResult> Album () {
            var photoCount = 0;
            var formValueProvider = await Request.StreamFile ((file) => {
                photoCount++;
                return System.IO.File.Create ($"{_folder}\\{file.FileName}");
            });

            var model = new AlbumModel {
                Title = formValueProvider.GetValue ("title").ToString (),
                Date = Convert.ToDateTime (formValueProvider.GetValue ("date").ToString ())
            };

            // ...

            return Ok (new {
                title = model.Title,
                    date = model.Date.ToString ("yyyy/MM/dd"),
                    photoCount = photoCount
            });
        }

    }
}