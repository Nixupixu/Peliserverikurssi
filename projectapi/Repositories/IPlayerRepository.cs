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

        Task<Character[]> GetAllChars(Guid playerid);
        Task<Character[]> GetAllCharsByUsername(string username);
        Task<Character> GetChar(Guid playerid, Guid charid);
        Task<Character> CreateChar(Guid playerid, Character character);
        Task<Character> ModifyChar(Guid playerid, Guid charid, Character character);
        Task<Character> DeleteChar(Guid playerid, Guid charid);
    }
}