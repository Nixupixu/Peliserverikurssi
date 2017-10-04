using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using projectapi.Processors;
using projectapi.Models;
using projectapi.Exceptions;

namespace projectapi.Controllers
{
    [Route("/api/users")]
    public class UserController : Controller
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
                throw new InvalidInputException();
            }
        }

        [HttpPost]
        public async Task<User> CreateUser([FromBody]NewUser user)
        {
            return await _processor.CreateUser(user);
        }

        [HttpPut("{id}")]
        public async Task<User> ModifyUser(Guid id, [FromBody]ModifiedUser user)
        {
             return await _processor.ModifyUser(id, user);
        }

        [HttpDelete("{id}")]
        public async Task<User> RemoveUser(Guid id)
        {
            return await _processor.RemoveUser(id);
        }
    }
}