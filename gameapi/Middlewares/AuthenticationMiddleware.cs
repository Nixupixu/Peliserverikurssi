using Microsoft.AspNetCore.Http;

namespace gameapi.Middlewares
{
    public class AuthenticationMiddleware
    {
        private RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            bool missing = false;
            bool unauthorized = false;

            try
            {
                await _next(context);
            }
            catch()
            {
                
            }
        }
    }
}