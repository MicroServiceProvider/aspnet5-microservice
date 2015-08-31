using System.Reflection;
using AspNet5.Microservice.Health;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;

namespace AspNet5.Microservice
{
    public static class MiddlewareHandler
    {
        public static void UseHealthEndpoint(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.StartsWith("/health"))
                {
                    HealthCheckRegistry.HealthStatus status = HealthCheckRegistry.GetStatus();
                    context.Response.Headers.Set("Content-Type", "application/json");
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(status));
                }
                else
                {
                    await next();
                }
            });
        }

        public static void UseEnvironmentEndpoint(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Equals("/env"))
                {
                    // Get current application environment
                    ApplicationEnvironment env = ApplicationEnvironment.GetApplicationEnvironment();

                    context.Response.Headers.Set("Content-Type", "application/json");
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(env, new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii}));
                }
                else
                {
                    await next();
                }
            });
        }

        // For DNX running on the full CLR we can use the GetExecutingAssembly() method to get our entry assembly
#if DNX451
        public static void UseInfoEndpoint(this IApplicationBuilder app)
        {
            Assembly entryAssembly = Assembly.GetCallingAssembly();
            UseInfoEndpoint(app, entryAssembly.GetName());
        }
#endif

        // .NET core does not contain the GetExecutingAssembly() method so we must pass in a reference to the entry assembly
        public static void UseInfoEndpoint(this IApplicationBuilder app, AssemblyName entryAssembly)
        {
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Equals("/info"))
                {
                    var appInfo = new
                    {
                        Name = entryAssembly.Name,
                        Version = entryAssembly.Version.ToString(3)
                    };
                    context.Response.Headers.Set("Content-Type", "application/json");
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(appInfo));
                }
                else
                {
                    await next();
                }
            });
        }
    }
}
