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
            // 從檔案
            .ConfigureAppConfiguration ((hostContext, config) => {
                var env = hostContext.HostingEnvironment;
                config.SetBasePath (Path.Combine (env.ContentRootPath, "Configuration"))
                    .AddJsonFile (path: "settings.json", optional : false, reloadOnChange : true);
            })
            // 從指令參數
            .ConfigureAppConfiguration ((hostContext, config) => config.AddCommandLine (args))
            // 從環境變數
            .ConfigureAppConfiguration ((hostContext, config) => config.AddEnvironmentVariables ())
            // 從記憶體物件(Hardcode)
            .ConfigureAppConfiguration ((hostContext, config) => {
                var dictionary = new Dictionary<string, string> { { "Site:Name", "John" },
                { "Site:Domain", "blog" }
                    };
                config.AddInMemoryCollection (dictionary);
            })
            // 從自訂組態
            .ConfigureAppConfiguration ((hostContext, config) => config.Add (new CustomConfigurationSource ()))
            .UseStartup<Startup> ()
            .Build ();
    }
}