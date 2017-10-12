using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using projectapi.Processors;
using projectapi.Models;
using projectapi.ModelValidation;

namespace projectapi.Controllers
{
    [Route("/api/users/{userid}/characters")]
    public class CharactersController
    {
        private CharacterProcessor _processor;

        public CharactersController(CharacterProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet]
        public async Task<Character[]> GetAllChars(Guid userid)
        {
            return await _processor.GetAllChars(userid);
        }

        [HttpGet("{charid}")]
        public async Task<Character> GetChar(Guid userid, Guid charid)
        {
            return await _processor.GetChar(userid, charid);
        }

        [HttpPost]
        public async Task<Character> CreateChar(Guid userid, [FromBody]NewCharacter character)
        {
            return await _processor.CreateChar(userid, character);
        }

        [HttpPut("{charid}")]
        public async Task<Character> ModifyChar(Guid userid, Guid charid, [FromBody]ModifiedCharacter character)
        {
            return await _processor.ModifyChar(userid, charid, character);
        }

        [HttpDelete("{charid}")]
        public async Task<Character> DeleteChar(Guid userid, Guid charid)
        {
            return await _processor.DeleteChar(userid, charid);
        }
    }
}