using System;
using System.Threading.Tasks;
using gameapi.Models;

namespace gameapi.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, Player player);
        Task<Player> Delete(Guid id);
        Task<Item[]> GetAllItems(Guid id);
        Task<Item> CreateItem(Guid id, Item item);
    }
}