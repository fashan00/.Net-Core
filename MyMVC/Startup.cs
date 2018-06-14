using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebsite.Filters;

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

            // 1. 指派錯誤頁面
            app.UseExceptionHandler ("/Home/Error");

            // 2. 指派錯誤頁面(Debug)
            // if (env.IsDevelopment ()) {
            //     app.UseDeveloperExceptionPage ();
            // } else {
            //     app.UseExceptionHandler ("/Home/Error");
            // }

            // 3. 回復客製JSON (測試網址: http://localhost:5000/api/test)
            // app.UseExceptionHandler (new ExceptionHandlerOptions () {
            //     ExceptionHandler = async context => {
            //         bool isApi = Regex.IsMatch (context.Request.Path.Value, "^/api/", RegexOptions.IgnoreCase);
            //         if (isApi) {
            //             context.Response.ContentType = "application/json";
            //             var json = @"{ ""Message"": ""Internal Server Error"" }";
            //             await context.Response.WriteAsync (json);
            //             return;
            //         }
            //         context.Response.Redirect ("/Home/Error");
            //     }
            // });

            // 4.使用客製ExceptionMiddleware
            // Middleware 的註冊順序很重要，越先註冊的會包在越外層。
            // 把 ExceptionMiddleware 註冊在越外層，能涵蓋的範圍就越多。
            // app.UseMiddleware<ExceptionMiddleware> ();

            app.UseStaticFiles ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}