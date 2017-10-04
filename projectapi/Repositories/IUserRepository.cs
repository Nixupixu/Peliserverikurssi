using System;
using System.Threading.Tasks;
using projectapi.Models;

namespace projectapi.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUser(string name);
        Task<User> CreateUser(User user);
        Task<User> Get(Guid id);
        Task<User> ModifyUser(Guid id, User user);
        Task<User> Delete(Guid id);
    }
}