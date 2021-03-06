﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace Accolades.Maije.Distributed.WebApi
{
    public class MaijeApp<TStartup>
        where TStartup : MaijeStartup
    {

        /// <summary>
        /// Initialize a new <see cref="MaijeApp{TStartup}"/>
        /// </summary>
        /// <param name="args">The application arguments</param>
        public MaijeApp(string[] args)
        {
            Configuration = GetAppConfiguration();

            WebHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<TStartup>()
                .UseSerilog();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        /// <summary>
        /// Gets the web host builder
        /// </summary>
        public IWebHostBuilder WebHostBuilder { get; }

        /// <summary>
        /// Gets the app configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Start the web application
        /// </summary>
        /// <returns></returns>
        public int Start()
        {
            int exitCode = 0;

            try
            {
                var webHost = WebHostBuilder.Build();
                webHost.Run();
            }
            catch (Exception ex)
            {
                Log.ForContext<TStartup>().Fatal(ex, ex.Message);

                exitCode = -1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            return exitCode;
        }

        /// <summary>
        /// Gets the current application configuration
        /// from global and specific appsettings.
        /// </summary>
        /// <returns>Return the application <see cref="IConfiguration"/></returns>
        private IConfiguration GetAppConfiguration()
        {
            // Actually, before ASP.NET bootstrap, we must rely on environment variable to get environment name
            // https://docs.microsoft.com/fr-fr/aspnet/core/fundamentals/environments?view=aspnetcore-2.2
            // Pay attention to casing for Linux environment. By default it's pascal case.
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
