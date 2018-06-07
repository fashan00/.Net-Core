using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
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

            // URL Rewrite: 
            // http://localhost:5000/about.aspx
            // URL Redirect: 
            // http://localhost:5000/first
            // http://localhost:5000/api.aspx/p123
            // http://localhost:5000/api/one/two/three
            var rewrite = new RewriteOptions ()
                .AddRewrite ("about.aspx", "home/about", skipRemainingRules : true)
                .AddRedirect ("first", "home/index", 301)
                .AddRedirect ("api.aspx/(.*)", "home/api1/$1", 301)
                .AddRedirect ("api/(.*)/(.*)/(.*)", "home/api2?p1=$1&p2=$2&p3=$3", 301);
            app.UseRewriter (rewrite);

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