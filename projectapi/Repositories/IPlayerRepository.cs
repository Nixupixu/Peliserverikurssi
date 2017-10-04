using System;
using System.Threading.Tasks;

using projectapi.Models;

namespace projectapi.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player[]> GetPlayersByMinLevel(int? level);
        Task<Player[]> GetPlayersByName(string name);
        Task<Player> Create(Player player);
        Task<Player> Modify(Player player);
        Task<Player> Delete(Guid id);
        

        Task<Item[]> GetAllItems(Guid playerid);
        Task<Item> GetItem(Guid playerid, Guid itemid);
        Task<Item> CreateItem(Guid playerid, Item item);
        Task<Item> ModifyItem(Guid playerid, Guid itemid, Item item);
        Task<Item> DeleteItem(Guid playerid, Guid itemid);
        
    }
}