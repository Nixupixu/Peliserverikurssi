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

            try
            {
                await _next(context);
            }
            catch(NotFoundException)
            {
                context.Response.StatusCode = 404;
                notFound = true;
            }

            if(notFound == true)
            {
                await context.Response.WriteAsync("Could not find a player");
            }
        }
    }
}