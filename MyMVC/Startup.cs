using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using MyWebsite.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace MyMVC {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddMvc ();

            services.AddSwaggerGen (c => {
                c.SwaggerDoc (
                    // name: 攸關 SwaggerDocument 的 URL 位置。
                    name: "v1",
                    // info: 是用於 SwaggerDocument 版本資訊的顯示(內容非必填)。
                    info : new Info {
                        Title = "RESTful API",
                            Version = "1.0.0",
                            Description = "This is ASP.NET Core RESTful API Sample.",
                            TermsOfService = "None",
                            Contact = new Contact {
                                Name = "John Wu",
                                    Url = "https://blog.johnwu.cc"
                            },
                            License = new License {
                                Name = "CC BY-NC-SA 4.0",
                                    Url = "https://creativecommons.org/licenses/by-nc-sa/4.0/"
                            }
                    }
                );

                var filePath = Path.Combine (PlatformServices.Default.Application.ApplicationBasePath, "Api.xml");
                c.IncludeXmlComments (filePath);
            });

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

            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint (
                    // url: 需配合 SwaggerDoc 的 name。 "/swagger/{SwaggerDoc name}/swagger.json"
                    url: "/swagger/v1/swagger.json",
                    // description: 用於 Swagger UI 右上角選擇不同版本的 SwaggerDocument 顯示名稱使用。
                    name: "RESTful API v1.0.0"
                );
                c.RoutePrefix = string.Empty;
            });
            app.UseStaticFiles ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}