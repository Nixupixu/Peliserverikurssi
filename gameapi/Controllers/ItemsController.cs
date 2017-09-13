using gameapi.Processors;
using gameapi.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gameapi.Controllers
{
    [Route("/api/players/{playerid}/items")]
    public class ItemsController
    {
        private ItemProcessor _processor;

        public ItemsController(ItemProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet]
        public async Task<Item[]> GetAllItems(Guid playerid)
        {
            Item[] items = await _processor.GetAllItems(playerid);
            return items;
        }

        [HttpPost]
        public async Task<Item> CreateItem(Guid playerid, NewItem item)
        {
            Item _item = await _processor.CreateItem(playerid, item);
            return _item;
        }
    }
}