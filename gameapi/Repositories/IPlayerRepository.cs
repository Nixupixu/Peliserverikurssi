using System;
using System.Threading.Tasks;
using gameapi.Models;

namespace gameapi.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player[]> GetPlayersByMinScore(int score);
        Task<Player[]> GetPlayersByTag(string tag);
        Task<Player[]> GetPlayersByProperty(string property);
        Task<Player[]> GetAllBySize(int size);
        Task<Player> GetPlayerByName(string name);
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, string name);
        Task<Player> Delete(Guid id);
        Task<Player> AddScore(Guid id, int score);

        Task<Item[]> GetAllItems(Guid playerid);
        Task<Item> GetItem(Guid playerid, Guid itemid);
        Task<Item> CreateItem(Guid playerid, Item item);
        Task<Item> ModifyItem(Guid playerid, Guid itemid, Item item);
        Task<Item> DeleteItem(Guid playerid, Guid itemid);
        Task<Player> RemoveItem(Guid itemid, int amount);
        
        Task<int> GetCommonPlayerLevel();
        
    }
}