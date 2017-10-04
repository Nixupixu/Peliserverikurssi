using System;
using System.Threading.Tasks;


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
            return await _repository.GetUser(name, password);
        }
    }
}