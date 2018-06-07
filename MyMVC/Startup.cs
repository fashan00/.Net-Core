using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyMVC {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles ();

            // http://localhost:5000 會對應到 HomeController 的 Index()。
            // http://localhost:5000/about 會對應到 HomeController 的 About()。
            // http://localhost:5000/home/test 會對應到 HomeController 的 Test()。
            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "about",
                    template: "about",
                    defaults : new { controller = "Home", action = "About" }
                );
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
                // 跟上面設定的 default 效果一樣
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller}/{action}/{id?}",
                //    defaults: new { controller = "Home", action = "Index" }
                //);
            });
        }
    }
}