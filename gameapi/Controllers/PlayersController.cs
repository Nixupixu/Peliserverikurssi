using System;
using System.Threading.Tasks;
using gameapi.ModelValidation;
using gameapi.Processors;
using Microsoft.AspNetCore.Mvc;

namespace gameapi.Controllers
{
    [Route("api/players")]
    public class PlayersController : Controller
    {
        private PlayerProcessor _processor;

        public PlayersController(PlayerProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet("id/{id:Guid}")]
        [HttpGet("name/{name}")]
        public async Task<Player> Get(Guid id, string name){
            //Assignment 5.2
            if(string.IsNullOrEmpty(name) == false)
            {
                return await _processor.GetPlayerByName(name);
            } 
            return await _processor.Get(id);
        }

        [HttpGet]
        [HttpGet("size/{size:int}")]
        [HttpGet("score/{score:int}")]
        [HttpGet("property/{property}")]
        [HttpGet("tag/{tag}")]
        public async Task<Player[]> GetAll(int? size, int? score, string property, string tag)
        {
            //Assignment 5.1
            if(score.HasValue == true)
            {
                return await _processor.GetPlayersByMinScore((int)score);
            }
            //Assignment 5.3
            else if(string.IsNullOrEmpty(tag) == false)
            {
                return await _processor.GetPlayersByTag(tag);
            }
            //Assignment 5.4
            else if(string.IsNullOrEmpty(property) == false)
            {
                return await _processor.GetPlayersByProperty(property);
            }
            //Assignment 5.5
            else if(size.HasValue == true)
            {
                return await _processor.GetAllBySize((int)size);
            }
            else
            {
                return await _processor.GetAll();
            }
        }

        //Assignment 5.11
        [HttpGet("commonlevel")]
        public async Task<int> GetCommonPlayerLevel()
        {
            return await _processor.GetCommonPlayerLevel();
        }
        
        [HttpPost]
        [ValidateModel]
        public async Task<Player> Create([FromBody]NewPlayer player)
        {
            return await _processor.Create(player);
        }

        [HttpPut("{id:Guid}/addscore/{score:int}")]
        public async Task<Player> AddScore(Guid id, int score)
        {
            return await _processor.AddScore(id, score);
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