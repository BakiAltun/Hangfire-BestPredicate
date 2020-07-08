using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Ninject;
using HangFire.Ninject;

namespace UI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IKernel kernel = new StandardKernel();
            
            string connectionString = _configuration.GetSection("ConnectionStrings").GetValue<string>("HangFire");
            string client = _configuration.GetSection("Clients").GetValue<string>("Optimus");

            services.AddHangfire(configuration => configuration
                                       .UseColouredConsoleLogProvider()
                                       .UseRecommendedSerializerSettings()
                                       .UseActivator(new NinjectJobActivator(kernel, new[] { client }))
                                       .UseSqlServerStorage(connectionString)
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHangfireDashboard();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
