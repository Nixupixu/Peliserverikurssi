using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using projectapi.ModelValidation;
using projectapi.Processors;

namespace projectapi.Controllers
{
    [Route("/api/players")]
    public class PlayersController : Controller
    {
        private PlayerProcessor _processor;

        public PlayersController(PlayerProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet("{id}")]
        public async Task<Player> Get(Guid id){
            return await _processor.Get(id);
        }

        [HttpGet]
        public async Task<Player[]> GetAll(int? level, string name)
        {
            if(level.HasValue)
            {
                return await _processor.GetPlayersByMinLevel(level);
            }
            else if(string.IsNullOrEmpty(name) == false)
            {
                return await _processor.GetPlayersByName(name);
            }
            else
            {
                return await _processor.GetAll();
            } 
        }
        
        [HttpPost]
        [ValidateModel]
        public async Task<Player> Create([FromBody]NewPlayer player)
        {
            return await _processor.Create(player);
        }
        

        [HttpPut("{id}")]
        public async Task<Player> Modify(Guid id, [FromBody]ModifiedPlayer player)
        {
            return await _processor.Modify(id, player);
        }

        [HttpDelete("{id}")]
        public async Task<Player> Delete(Guid id)
        {
            return await _processor.Delete(id);
        }
    }
}