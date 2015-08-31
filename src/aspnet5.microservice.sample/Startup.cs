using System.Collections.Generic;
using System.IO;
using AspNet5.Microservice;
using AspNet5.Microservice.Health;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Runtime;

#if DNXCORE50
using System.Reflection;
#endif 

namespace aspnet5_microservice_sample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appenv)
        {
            _env = env;
            _appenv = appenv;
        }

        private IHostingEnvironment _env;
        private IApplicationEnvironment _appenv;

        public void ConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app)
        {
            Dictionary<string, string> collection = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" } };
            var config1 = new ConfigurationBuilder().AddInMemoryCollection(collection).Build();
            var config2 = new ConfigurationBuilder(_appenv.ApplicationBasePath).AddIniFile("hosting.ini").Build();

            AppConfig.AddConfigurationObject(config1, "memorySource");
            AppConfig.AddConfigurationObject(config2, "iniSource");

            // Register health checks
            HealthCheckRegistry.RegisterHealthCheck("MyCustomMonitor", () => HealthResponse.Healthy("Test Message"));
            HealthCheckRegistry.RegisterHealthCheck("MyCustomMonitor2", () => HealthResponse.Healthy("Test Message2"));
            HealthCheckRegistry.RegisterHealthCheck("SampleOperation", () => SampleHealthCheckOperation());

            // Activate endpoints
            app.UseHealthEndpoint();
            app.UseEnvironmentEndpoint();

            /*
             *   The compiler directive below is only required if you plan to target .NET core as well as the full CLR
             *   If you don't target dnxcore50 in your project.json you can remove the below #if and just call UseInfoEndpoint()
             *   without any parameters
             */

#if DNXCORE50
            // Required for .NET Core until the relevant APIs are added
            app.UseInfoEndpoint(typeof(Startup).GetTypeInfo().Assembly.GetName());
#else
            app.UseInfoEndpoint();
#endif

        }

        public static HealthResponse SampleHealthCheckOperation()
        {
            return HealthResponse.Unhealthy("Sample operation failed");
        }

    }
}
