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
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);

        Task<Item[]> GetAllItems(Guid playerid);
        Task<Item> GetItem(Guid playerid, Guid itemid);
        Task<Item> CreateItem(Guid playerid, Item item);
        Task<Item> ModifyItem(Guid playerid, Guid itemid, ModifiedItem item);
        Task<Item> DeleteItem(Guid playerid, Guid itemid);
    }
}