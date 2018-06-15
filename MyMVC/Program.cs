using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MyMVC {
    public class Program {
        private readonly static ILog _log = LogManager.GetLogger (typeof (Program));

        public static void Main (string[] args) {
            LoadLog4netConfig ();
            _log.Info ("Application Start");
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
            // .ConfigureLogging ((hostContext, logging) => {
            //     var env = hostContext.HostingEnvironment;
            //     var configuration = new ConfigurationBuilder ()
            //         .SetBasePath (Path.Combine (env.ContentRootPath, "Configuration"))
            //         .AddJsonFile (path: "settings.json", optional : true, reloadOnChange : true)
            //         .Build ();
            //     logging.AddConfiguration (configuration.GetSection ("Logging"));
            // })
            .ConfigureLogging (logging => {
                logging.AddProvider (new Log4netProvider ("log4net.config"));
            })
            .UseStartup<Startup> ()
            .Build ();
        private static void LoadLog4netConfig () {
            var repository = LogManager.CreateRepository (
                Assembly.GetEntryAssembly (),
                typeof (log4net.Repository.Hierarchy.Hierarchy)
            );
            XmlConfigurator.Configure (repository, new FileInfo ("log4net.config"));
        }
    }

}