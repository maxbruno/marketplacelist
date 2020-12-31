using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceList.API.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var method = context.Request.Method;

            if (HttpMethods.IsPost(method) || HttpMethods.IsPut(method) || HttpMethods.IsPatch(method))
            {
                var body = await FormatRequestBody(context.Request);
            }

            await _next(context);
        }

        private async Task<string> FormatRequestBody(HttpRequest request)
        {
            var body = string.Empty;

            request.EnableBuffering(bufferThreshold: 1024 * 45, bufferLimit: 1024 * 100);

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }

            request.Body.Position = 0;

            return body;
        }
    }
}