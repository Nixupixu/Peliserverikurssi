using gameapi.Repositories;
using gameapi.Models;
using System;
using System.Threading.Tasks;
namespace gameapi.Processors
{
    public class ItemProcessor
    {
        private readonly IPlayerRepository _repository;

        public ItemProcessor(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Item[]> GetAllItems(Guid id)
        {
            return await _repository.GetAllItems(id);
        }

        public async Task<Item> CreateItem(Guid playerid, NewItem item)
        {
            Random rng = new Random();
            DateTime creationDate = DateTime.Now;
            Item _item = new Item(item._Name, item._Level, rng.Next(1,1000), creationDate);
            return await _repository.CreateItem(playerid, _item);
        }
    }
}