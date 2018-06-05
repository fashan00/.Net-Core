using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace MyWebsite {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services) {
            services.AddRouting ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {

            // http://localhost:5000/default
            var defaultRouteHandler = new RouteHandler (context => {
                var routeValues = context.GetRouteData ().Values;
                return context.Response.WriteAsync ($"Route values: {string.Join(", ", routeValues)}");
            });
            // http://localhost:5000/home/about
            var routeBuilder = new RouteBuilder (app, defaultRouteHandler);
            routeBuilder.MapRoute ("default", "{first:regex(^(default|home)$)}/{second?}");

            // http://localhost:5000/user/john
            routeBuilder.MapGet ("user/{name}", context => {
                var name = context.GetRouteValue ("name");
                return context.Response.WriteAsync ($"Get user. name: {name}");
            });
            // http://localhost:5000/user/john
            routeBuilder.MapPost ("user/{name}", context => {
                var name = context.GetRouteValue ("name");
                return context.Response.WriteAsync ($"Create user. name: {name}");
            });

            var routes = routeBuilder.Build ();
            app.UseRouter (routes);
        }
    }
}