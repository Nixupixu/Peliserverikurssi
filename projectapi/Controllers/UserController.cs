using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using projectapi.Processors;
using projectapi.Models;

namespace projectapi.Controllers
{
    [Route("/api/users")]
    public class UserController
    {
        private UserProcessor _processor;

        public UserController(UserProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet]
        public async Task<User> GetUser(string username, string password)
        {
            if(string.IsNullOrEmpty(username) == false && string.IsNullOrEmpty(password) == false)
            {
                return await _processor.GetUser(username, password);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}