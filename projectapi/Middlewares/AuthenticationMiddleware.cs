using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

using projectapi.Models;

namespace projectapi.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly AuthKey _authKey;
        private RequestDelegate _next;

        public AuthenticationMiddleware(IOptions<AuthKey> authKey, RequestDelegate next)
        {
            _authKey = authKey.Value;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string apikey = _authKey.ApiKey;
            StringValues values;

            bool apiKeyExists = context.Request.Headers.TryGetValue("x-api-key", out values);
            bool authorized = false;

            if(apiKeyExists)
            {
                string[] keys = values.ToArray();
                foreach(string key in keys)
                {
                    if(key.Equals(apikey))
                    {
                        authorized = true;
                    }
                }
            }

            if(authorized)
            {
                await _next(context);
            }
            else if(apiKeyExists == false)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Bad request");
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized key value");
            }
        }
    }
}