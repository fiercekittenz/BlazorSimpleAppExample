using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace BlazorSimpleAppExample.Server
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
      {
         // Note: This will create the UserDataService instance.
         services.AddSingleton<UserDataService>();

         // You must add the SignalR service to your ASP.NET Core back-end otherwise it won't be able
         // to properly direct traffic to the appropriate hub.
         services.AddSignalR();

         // These are stock functions that are included by default when you create a new Blazor project.
         // They add any controllers with views (MVC) or server Razor pages.
         services.AddControllersWithViews();
         services.AddRazorPages();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
         }
         else
         {
            app.UseExceptionHandler("/Error");
         }

         app.UseBlazorFrameworkFiles();
         app.UseStaticFiles();

         app.UseRouting();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");

            // To add a hub endpoint, you need to use the "MapHub" method as seen below:
            endpoints.MapHub<DataHub>("/datahub");
         });
      }
   }
}
