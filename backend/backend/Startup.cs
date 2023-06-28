using AspNetCore.RouteAnalyzer;
using Microsoft.AspNetCore.Builder;

namespace backend
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddRouteAnalyzer(); // Add
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseMvc(routes =>
            {
                routes.MapRouteAnalyzer("/routes"); // Add
                });


        }
    }
}
