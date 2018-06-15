using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MyMVC {
    public class Program {
        public static void Main (string[] args) {
            BuildWebHost (args).Run ();
        }

        public static IWebHost BuildWebHost (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            // 當 settings.Production.json 載入後，就會把 settings.json 的 DBConnectionString 設定蓋掉，
            // 而 settings.json 其它的設定依然能繼續使用。
            // 因為設定 optional=false
            .ConfigureAppConfiguration ((hostContext, config) => {
                var env = hostContext.HostingEnvironment;
                config.SetBasePath (Path.Combine (env.ContentRootPath, "Configuration"))
                    .AddJsonFile (path: "settings.json", optional : false, reloadOnChange : true)
                    .AddJsonFile (path: $"settings.{env.EnvironmentName}.json", optional : true, reloadOnChange : true);
            })
            .ConfigureLogging ((hostContext, logging) => {
                var env = hostContext.HostingEnvironment;
                var configuration = new ConfigurationBuilder ()
                    .SetBasePath (Path.Combine (env.ContentRootPath, "Configuration"))
                    .AddJsonFile (path: "settings.json", optional : true, reloadOnChange : true)
                    .Build ();
                logging.AddConfiguration (configuration.GetSection ("Logging"));
            })
            .UseStartup<Startup> ()
            .Build ();
    }
}