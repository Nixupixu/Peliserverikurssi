using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using projectapi.Exceptions;
using projectapi.Processors;

namespace projectapi.Middlewares
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
            bool invalidInput = false;
            bool userNotFound = false;
            bool wrongPassword = false;

            try
            {
                await _next(context);
            }
            catch(InvalidInputException)
            {
                context.Response.StatusCode = 400;
                invalidInput = true;
            }
            catch(UserNotFoundException)
            {
                context.Response.StatusCode = 404;
                userNotFound = true;
            }
            catch(WrongPasswordException)
            {
                context.Response.StatusCode = 403;
                wrongPassword = true;
            }

            if(invalidInput == true)
            {
                await context.Response.WriteAsync("Invalid input, check username and password");
            }
            if(userNotFound == true)
            {
                await context.Response.WriteAsync("User could not be found");
            }
            if(wrongPassword == true)
            {
                await context.Response.WriteAsync("Invalid password, try again");
            }
        }
    }
}