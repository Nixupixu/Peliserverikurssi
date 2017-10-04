using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using projectapi.Processors;
using projectapi.Models;
using projectapi.ModelValidation;

namespace projectapi.Controllers
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
            return await _processor.GetAllItems(playerid);
        }

        [HttpGet("{itemid}")]
        public async Task<Item> GetItem(Guid playerid, Guid itemid)
        {
            return await _processor.GetItem(playerid, itemid);
        }

        [HttpPost]
        public async Task<Item> CreateItem(Guid playerid, [FromBody] NewItem item)
        {
            return await _processor.CreateItem(playerid, item);
        }

        [HttpPut("{itemid}")]
        public async Task<Item> ModifyItem(Guid playerid, Guid itemid, [FromBody] ModifiedItem item)
        {
            return await _processor.ModifyItem(playerid, itemid, item);
        }

        [HttpDelete("{itemid}")]
        public async Task<Item> DeleteItem(Guid playerid, Guid itemid)
        {
            return await _processor.DeleteItem(playerid, itemid);
        }
    }
}