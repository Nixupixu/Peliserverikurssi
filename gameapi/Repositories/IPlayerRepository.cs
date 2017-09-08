using System;
using System.Threading.Tasks;
//using gameapi.Models;

namespace gameapi.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);
    }
}