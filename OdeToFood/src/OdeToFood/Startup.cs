﻿namespace OdeToFood
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using OdeToFood.Services;

    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => Configuration);7
            services.AddSingleton<IGreeter, Greeter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var greeting = greeter.GetGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }
    }
}