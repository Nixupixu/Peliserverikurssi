using System;
using System.Threading.Tasks;

using projectapi.Repositories;
using projectapi.Models;

namespace projectapi.Processors
{
    public class CharacterProcessor
    {
        private readonly IPlayerRepository _repository;

        public CharacterProcessor(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Character[]> GetAllChars(Guid playerid)
        {
            return await _repository.GetAllChars(playerid);
        }

        public async Task<Character[]> GetAllCharsByUsername(string username)
        {
            return await _repository.GetAllCharsByUsername(username);
        }

        public async Task<Character> GetChar(Guid playerid, Guid charid)
        {
            return await _repository.GetChar(playerid, charid);
        }

        public async Task<Character> CreateChar(Guid playerid, NewCharacter character)
        {
            Character _character = new Character()
            {
                _CharId = Guid.NewGuid(),
                _Name = character._Name,
                _Strength = character._Strength,
                _Agility = character._Agility,
                _Intelligence = character._Intelligence
            };

            return await _repository.CreateChar(playerid, _character);
        }

        public async Task<Character> ModifyChar(Guid playerid, Guid charid, ModifiedCharacter character)
        {
            Character _character = await _repository.GetChar(playerid, charid);
            _character._Name = character._Name;
            return await _repository.ModifyChar(playerid, charid, _character); 
        }

        public async Task<Character> DeleteChar(Guid playerid, Guid charid)
        {
            return await _repository.DeleteChar(playerid, charid);
        }
    }
}