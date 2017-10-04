using System;
using System.Threading.Tasks;

using projectapi.Repositories;
using projectapi.Models;
using projectapi.Exceptions;

namespace projectapi.Processors
{
    public class UserProcessor
    {
        private readonly IUserRepository _repository;

        public UserProcessor(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUser(string name, string password)
        {
            User user = await _repository.GetUser(name);
            if(user._Password.Equals(password) == true)
            {
                return user;
            }
            else
            {
                throw new WrongPasswordException();
            }
        }

        public Task<User> CreateUser(NewUser user)
        {
            //Check if user exists already
            
            User _user = new User()
            {
                _id = Guid.NewGuid(),
                _Name = user._Name,
                _Password = user._Password
            };

            return _repository.CreateUser(_user);
        }

        public async Task<User> ModifyUser(Guid id, ModifiedUser user)
        {
            User _user = await _repository.Get(id);
            _user._Password = user._Password;
            return await _repository.ModifyUser(id, _user);
        }

        public async Task<User> RemoveUser(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}