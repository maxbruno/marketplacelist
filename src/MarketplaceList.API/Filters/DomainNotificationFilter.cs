using MarketplaceList.Domain.Interfaces.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MarketplaceList.API.Filters
{
    public class DomainNotificationFilter : IAsyncResultFilter
    {
        private readonly IDomainNotification _domainNotification;

        public DomainNotificationFilter(IDomainNotification domainNotification)
        {
            _domainNotification = domainNotification;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_domainNotification.HasNotifications)
            {
                var validations = JsonConvert.SerializeObject(_domainNotification.Notifications.Select(x => x.Value));

                var problemDetails = new ProblemDetails
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.HttpContext.Request.Path.Value,
                    Detail = validations
                };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/problem+json";

                var notifications = JsonConvert.SerializeObject(problemDetails);
                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}