using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using WebApplicationUI.Models.DataContexts;

namespace WebApplicationUI
{
    public class Startup
    {
        readonly IConfiguration conf;

        public Startup(IConfiguration configuration)
        {
            this.conf = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToDoListDbConext>(cfg =>
            {
                cfg.UseMySQL(conf.GetConnectionString("cString"));
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                     Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/npm"
            });

            app.UseRouting();

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=Todos}/{Action=Index}/{Id?}"
                    );
            });
        }
    }
}
