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

        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            if(_players.ContainsKey(id))
            {
                _players[id]._Name = player._Name;
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
    }
}