using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        private readonly IConfiguration _config;
        private readonly Settings _settings;

        public HomeController (IConfiguration config, IOptions<Settings> settings) {
            _config = config;
            _settings = settings.Value;
        }

        [HttpGet ("MultipleEnvironments")]
        public string MultipleEnvironments () {
            // 弱型別
            var DBConnectionString = _config["DBConnectionString"];
            var subProperty1 = _config["CustomObject:Property:SubProperty1"];
            var subProperty2 = _config["CustomObject:Property:SubProperty2"];
            var subProperty3 = _config["CustomObject:Property:SubProperty3"];

            return $"DBConnectionString({DBConnectionString.GetType()}): {DBConnectionString}\r\n" +
                $"subProperty1({subProperty1.GetType()}): {subProperty1}\r\n" +
                $"subProperty2({subProperty2.GetType()}): {subProperty2}\r\n" +
                $"subProperty3({subProperty3.GetType()}): {subProperty3}\r\n";
        }

        [HttpGet ("ConfigFromFile")]
        public string ConfigFromFile () {
            // 弱型別
            var defaultCulture = _config["SupportedCultures:1"];
            var subProperty1 = _config["CustomObject:Property:SubProperty1"];
            var subProperty2 = _config["CustomObject:Property:SubProperty2"];
            var subProperty3 = _config["CustomObject:Property:SubProperty3"];

            return $"defaultCulture({defaultCulture.GetType()}): {defaultCulture}\r\n" +
                $"subProperty1({subProperty1.GetType()}): {subProperty1}\r\n" +
                $"subProperty2({subProperty2.GetType()}): {subProperty2}\r\n" +
                $"subProperty3({subProperty3.GetType()}): {subProperty3}\r\n";
        }

        [HttpGet ("ConfigFromFileModel")]
        public string ConfigFromFileModel () {
            // 強型別
            var defaultCulture = _settings.SupportedCultures[0];
            var subProperty1 = _settings.CustomObject.Property.SubProperty1;
            var subProperty2 = _settings.CustomObject.Property.SubProperty2;
            var subProperty3 = _settings.CustomObject.Property.SubProperty3;

            return $"defaultCulture({defaultCulture.GetType()}): {defaultCulture}\r\n" +
                $"subProperty1({subProperty1.GetType()}): {subProperty1}\r\n" +
                $"subProperty2({subProperty2.GetType()}): {subProperty2}\r\n" +
                $"subProperty3({subProperty3.GetType()}): {subProperty3}\r\n";
        }

        [HttpGet ("ConfigFromCommandLine")]
        public string ConfigFromCommandLine () {
            var siteName = _config["SiteName"];
            var domain = _config["Domain"];

            return $"SiteName({siteName.GetType()}): {siteName}\r\n" +
                $"Domain({domain.GetType()}): {domain}\r\n";
        }

        [HttpGet ("ConfigFromEnvironmentVariables")]
        public string ConfigFromEnvironmentVariables () {
            var sample = _config["Sample"];
            return $"sample({sample.GetType()}): {sample}\r\n";
        }

        [HttpGet ("ConfigFromHardCode")]
        public string ConfigFromHardCode () {
            var siteName = _config["Site:Name"];
            var domain = _config["Site:Domain"];

            return $"Site.Name({siteName.GetType()}): {siteName}\r\n" +
                $"Site.Domain({domain.GetType()}): {domain}\r\n";
        }

        [HttpGet ("ConfigFromCustom")]
        public string ConfigFromCustom () {
            var siteName = _config["Custom:Site:Name"];
            var domain = _config["Custom:Site:Domain"];

            return $"Custom.Site.Name({siteName.GetType()}): {siteName}\r\n" +
                $"Custom.Site.Domain({domain.GetType()}): {domain}\r\n";
        }

        public IActionResult Error () {
            return View ();
        }
    }

}