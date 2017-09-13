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
            Player player = await _processor.Get(id);
            return player;
        }

        [HttpGet]
        public async Task<Player[]> GetAll()
        {
            Player[] players = await _processor.GetAll();
            return players;
        }
        
        [HttpPost]
        //[ValidateModel]
        public async Task<Player> Create([FromBody]NewPlayer player)
        {
            Player _player = await _processor.Create(player);
            return _player;
        }

        [HttpPut("{id}")]
        public async Task<Player> Modify(Guid id, [FromBody] Player player)
        {
            return await _processor.Modify(id, player);
        }

        [HttpDelete("{id}")]
        public async Task<Player> Delete(Guid id)
        {
            Player _player = await _processor.Delete(id);
            return _player;
        }
    }
}