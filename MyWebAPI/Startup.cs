using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MyWebAPI {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ();

            services.AddTransient<ISampleTransient, Sample> ();
            services.AddScoped<ISampleScoped, Sample> ();
            services.AddSingleton<ISampleSingleton, Sample> ();
            // Singleton 也可以用以下方法註冊
            // services.AddSingleton<ISampleSingleton>(new Sample());

            services.AddScoped<CustomService, CustomService> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseMvc ();

            // UseDefaultFiles 必須註冊在 UseStaticFiles 之前。
            // 如果先註冊 UseStaticFiles，當 URL 是 / 時，UseStaticFiles 找不到該檔案，就會直接回傳找不到；所以就沒有機會進到 UseDefaultFiles。

            // 啟用預設檔案: http://localhost:5000/ 可以自動指向到 index.html
            app.UseDefaultFiles ();
            // 啟用靜態檔案: http://localhost:5000/index.html
            app.UseStaticFiles ();
            // 啟用指定目錄: http://localhost:5000/third-party/index.html
            app.UseStaticFiles (new StaticFileOptions () {
                FileProvider = new PhysicalFileProvider (
                        Path.Combine (env.ContentRootPath, @"wwwpublic")),
                    RequestPath = new PathString ("/third-party")
            });
            // 啟用檔案清單: http://localhost:5000/StaticFiles
            app.UseFileServer (new FileServerOptions () {
                FileProvider = new PhysicalFileProvider (
                        Path.Combine (env.ContentRootPath, @"bin")
                    ),
                    RequestPath = new PathString ("/StaticFiles"),
                    EnableDirectoryBrowsing = true
            });
        }
    }
}