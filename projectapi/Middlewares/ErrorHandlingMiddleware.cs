using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using projectapi.Exceptions;

namespace projectapi
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            bool notFound = false;
            bool notHighEnoughLevel = false;
            bool itemNotFound = false;

            try
            {
                await _next(context);
            }
            catch(NotFoundException)
            {
                context.Response.StatusCode = 404;
                notFound = true;
            }
            catch(NotHighEnoughLevelException)
            {
                context.Response.StatusCode = 406;
                notHighEnoughLevel = true;
            }
            catch(ItemNotFoundException)
            {
                context.Response.StatusCode = 404;
                itemNotFound = true;
            }

            if(notFound == true)
            {
                await context.Response.WriteAsync("Could not find a player");
            }
            if(notHighEnoughLevel == true)
            {
                await context.Response.WriteAsync("Player level not high enough");
            }
            if(itemNotFound == true)
            {
                await context.Response.WriteAsync("Item not found");
            }
        }
    }
}