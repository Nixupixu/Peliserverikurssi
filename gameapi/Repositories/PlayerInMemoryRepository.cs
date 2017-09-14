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
            if(_players.ContainsKey(id) == false)
            {
                throw new NotFoundException();
            }
            _players[id]._Name = player._Name;
            return Task.FromResult(_players[id]);
        }

        public Task<Player> Delete(Guid id)
        {
            if(_players.ContainsKey(id) == false)
            {
                throw new NotFoundException();
            }
            Player _player = _players[id];
            _players.Remove(id);

            return Task.FromResult(_player);
        }

        public Task<Item[]> GetAllItems(Guid playerid)
        {
            if(_players.ContainsKey(playerid) == false)
            {
                throw new NotFoundException();
            }

            return Task.FromResult(_players[playerid]._Items.ToArray());
        }

        public Task<Item> GetItem(Guid playerid, Guid itemid)
        {
            if(_players.ContainsKey(playerid) == false)
            {
                throw new NotFoundException();
            }

            Item returnedItem = null;

            foreach(Item i in _players[playerid]._Items)
            {
                if(i._ItemId == itemid)
                {
                    returnedItem = i;
                    break;
                }
            }

            if(returnedItem == null)
            {
                throw new ItemNotFoundException();
            }

            return Task.FromResult(returnedItem);
        }

        public Task<Item> CreateItem(Guid playerid, Item item)
        {
            if(_players.ContainsKey(playerid) == false)
            {
                throw new NotFoundException();
            }

            if(_players[playerid]._Level < item._Level)
            {
                throw new NotHighEnoughLevelException();
            }

            _players[playerid]._Items.Add(item);
            return Task.FromResult(item);
        }

        public Task<Item> ModifyItem(Guid playerid, Guid itemid, ModifiedItem item)
        {
            if(_players.ContainsKey(playerid) == false)
            {
                throw new NotFoundException();
            }         

            Item returnedItem = null;

            foreach(Item i in _players[playerid]._Items)
            {
                if(i._ItemId == itemid)
                {
                    i._Name = item._Name;
                    returnedItem = i;
                    break;
                }
            }
            
            if(returnedItem == null)
            {
                throw new ItemNotFoundException();
            }

            return Task.FromResult(returnedItem);
        }

        public Task<Item> DeleteItem(Guid playerid, Guid itemid)
        {
            if(_players.ContainsKey(playerid) == false)
            {
                throw new NotFoundException();
            }

            Item returnedItem = null;

            foreach(Item i in _players[playerid]._Items)
            {
                if(i._ItemId == itemid)
                {
                    returnedItem = i;
                    _players[playerid]._Items.Remove(i);
                    break;
                }
            }

            if(returnedItem == null)
            {
                throw new ItemNotFoundException();
            }

            return Task.FromResult(returnedItem);
        }
    }
}