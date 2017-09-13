using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using gameapi.Processors;
using gameapi.Models;
using gameapi.Exceptions;

using Microsoft.AspNetCore.Mvc;


namespace gameapi.Repositories
{
    public class PlayerInMemoryRepository : IPlayerRepository
    {
        Dictionary<Guid, Player> _players = new Dictionary<Guid, Player>();

        public PlayerInMemoryRepository()
        {
             Player test = new Player(Guid.NewGuid(), "Testi");
             Player test2 = new Player(Guid.NewGuid(), "Testi2");
             _players.Add(test._id, test);
             _players.Add(test2._id, test2);
        }
        public Task<Player> Get(Guid id)
        {
            if(_players.ContainsKey(id) == false)
            {
                throw new NotFoundException();
            }
            return Task.FromResult(_players[id]);
        }

        public Task<Player[]> GetAll()
        {
            return Task.FromResult(_players.Values.ToArray());
        }
    
        public Task<Player> Create(Player player)
        {
            _players.Add(player._id, player);
            return Task.FromResult(player);
        }

        public Task<Player> Modify(Guid id, Player player)
        {
            if(_players.ContainsKey(id))
            {
                _players[id]._Name = player._Name;
                return Task.FromResult(player);
            }

            return null;
        }

        public Task<Player> Delete(Guid id)
        {
            if(_players.ContainsKey(id))
            {
                Player _player = _players[id];
                _players.Remove(id);

                return Task.FromResult(_player);
            }

            return null;
        }

        public Task<Item[]> GetAllItems(Guid playerid)
        {
            if(_players.ContainsKey(playerid) == false)
            {
                throw new NotFoundException();
            }
            return Task.FromResult(_players[playerid]._Items);
        }

        public Task<Item> CreateItem(Guid playerid, Item item)
        {
            Player player;
            if(_players.ContainsKey(playerid) == false)
            {
                throw new NotFoundException();
            }

            _players[playerid]._Items.Append(item);
            return Task.FromResult(item);
            
        }
    }
}