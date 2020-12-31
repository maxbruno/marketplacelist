using MarketplaceList.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MarketplaceList.API.Extensions
{
     public static class LogExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}