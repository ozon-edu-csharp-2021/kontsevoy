using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MerchandiseService.Infrastructure.Middlewares
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next) {}

        public async Task InvokeAsync(HttpContext context)
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            await context.Response.WriteAsync($"{{\"version\": \"{assembly.Version}\", \"serviceName\": \"{assembly.Name}\"}}");
        }
    }
}