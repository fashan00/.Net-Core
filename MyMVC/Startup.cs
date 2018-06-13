using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using MyMVC.Models;
using MyWebsite.Extensions;
using MyWebsite.Filters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace MyMVC {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc (
                    //全域註冊
                    config => {
                        config.Filters.Add (new ResultFilter ());
                        config.Filters.Add (new ExceptionFilter ());
                        config.Filters.Add (new ResourceFilter ());
                        config.Filters.Add (new ActionFilter () { Name = "Global", Order = 3 });
                    }
                )
                .AddJsonOptions (options => {
                    // DefaultContractResolver 名稱是延續 ASP.NET，雖然名稱叫 Default，但在 ASP.NET Core 它不是 Default。
                    // CamelCasePropertyNamesContractResolver 才是 ASP.NET Core 的 Default ContractResolver。
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver ();
                    // Ignore Null
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            // // 將 Session 存在 ASP.NET Core 記憶體中
            // services.AddDistributedMemoryCache ();
            // services.AddSession (
            //     // 設定Session安全性
            //     options => {
            //         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //         options.Cookie.Name = "mywebsite";
            //         options.IdleTimeout = TimeSpan.FromMinutes (5);
            //     }
            // );

            // // DI 容器中加入 IHttpContextAccessor 及 ISessionWapper
            // // ASP.NET Core 實作了 IHttpContextAccessor，讓 HttpContext 可以輕鬆的注入給需要用到的物件使用。
            // // 由於 IHttpContextAccessor 只是取用 HttpContext 實例的接口，用 Singleton 的方式就可以供其它物件使用。
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor> ();
            // services.AddSingleton<ISessionWapper, SessionWapper> ();

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

            // SessionMiddleware 加入 Pipeline
            // app.UseSession ();

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

            app.UseMvc ();
        }
    }
}