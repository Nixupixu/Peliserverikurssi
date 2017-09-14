using System;
using System.Threading.Tasks;
using gameapi.ModelValidation;
using gameapi.Processors;
using Microsoft.AspNetCore.Mvc;

namespace gameapi.Controllers
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
        public async Task<Player[]> GetAll()
        {
            return await _processor.GetAll();
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