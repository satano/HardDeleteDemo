using Kros.AspNetCore.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(HardDeleteDemo.Startup))]

namespace HardDeleteDemo
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"),
                    optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"),
                    optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.local.json"),
                    optional: true, reloadOnChange: false);

            builder.ConfigurationBuilder.AddAzureAppConfig(GetAzureFunctionsEnvironment());
            builder.ConfigurationBuilder.AddEnvironmentVariables();
        }

        // Prevzaté z fakturácie.
        private static string GetAzureFunctionsEnvironment()
            => Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");

        public override void Configure(IFunctionsHostBuilder builder)
        {
        }
    }
}
