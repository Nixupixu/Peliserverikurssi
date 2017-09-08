using System.Threading.Tasks;
using gameapi.Exceptions;
using Microsoft.AspNetCore.Http;

namespace gameapi
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
            bool failed = false;

            try
            {
                await _next(context);
            }
            catch(NotFoundException)
            {
                context.Response.StatusCode = 404;
                failed = true;
            }

            if(failed == true)
            {
                await context.Response.WriteAsync("Could not find a player");
            }
        }
    }
}