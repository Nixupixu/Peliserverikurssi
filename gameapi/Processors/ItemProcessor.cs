using System;
using System.Threading.Tasks;

using gameapi.Repositories;
using gameapi.Models;

namespace gameapi.Processors
{
    public class ItemProcessor
    {
        private readonly IPlayerRepository _repository;

        public ItemProcessor(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Item[]> GetAllItems(Guid playerid)
        {
            return await _repository.GetAllItems(playerid);
        }

        public async Task<Item> GetItem(Guid playerid, Guid itemid)
        {
            return await _repository.GetItem(playerid, itemid);
        }

        public async Task<Item> CreateItem(Guid playerid, NewItem item)
        {
            Random rng = new Random();
            Item _item = new Item()
            {
                _ItemId = Guid.NewGuid(),
                _Name = item._Name,
                _Type = item._Type,
                _Price = rng.Next(1,2000),
                _Level = item._Level,
                _CreationDate = DateTime.Today
            };

            return await _repository.CreateItem(playerid, _item);
        }

        public async Task<Item> ModifyItem(Guid playerid, Guid itemid, ModifiedItem item)
        {
            return await _repository.ModifyItem(playerid, itemid, item);
        }

        public async Task<Item> DeleteItem(Guid playerid, Guid itemid)
        {
            return await _repository.DeleteItem(playerid, itemid);
        }
    }
}