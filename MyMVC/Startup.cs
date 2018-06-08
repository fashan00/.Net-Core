using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMVC.Models;
using MyWebsite.Extensions;

namespace MyMVC {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ();

            // 將 Session 存在 ASP.NET Core 記憶體中
            services.AddDistributedMemoryCache ();
            services.AddSession (
                // 設定Session安全性
                options => {
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.Name = "mywebsite";
                    options.IdleTimeout = TimeSpan.FromMinutes (5);
                }
            );

            // DI 容器中加入 IHttpContextAccessor 及 ISessionWapper
            // ASP.NET Core 實作了 IHttpContextAccessor，讓 HttpContext 可以輕鬆的注入給需要用到的物件使用。
            // 由於 IHttpContextAccessor 只是取用 HttpContext 實例的接口，用 Singleton 的方式就可以供其它物件使用。
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor> ();
            services.AddSingleton<ISessionWapper, SessionWapper> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {

            // SessionMiddleware 加入 Pipeline
            app.UseSession ();
            // Sample Session
            /*
            app.Run (async (context) => {

                context.Session.SetString ("Sample", "This is Session.");
                string message = context.Session.GetString ("Sample");
                await context.Response.WriteAsync ($"{message}");
            });
            */

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}