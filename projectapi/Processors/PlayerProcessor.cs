using System;
using System.Threading.Tasks;

using projectapi.Repositories;
using projectapi.Models;

namespace projectapi.Processors
{
    public class PlayerProcessor
    {
        private readonly IPlayerRepository _repository;

        public PlayerProcessor(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Player> Get(Guid id)
        {
            return await _repository.Get(id);
        }

        public async Task<Player[]> GetAll()
        {
            return await _repository.GetAll();
        }

        public Task<Player> Create(NewPlayer player)
        {
            Player _player = new Player()
            {
                _id = Guid.NewGuid(),
                _Name = player._Name,
                _Level = 1
            };
            return _repository.Create(_player);
        }
        
        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
        {
            Player player = await _repository.Get(id);
            player._Name = modifiedPlayer._Name;
            return await _repository.Modify(player);
        }
        public async Task<Player> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async Task<Player[]> GetPlayersByMinLevel(int? level)
        {
            return await _repository.GetPlayersByMinLevel(level);
        }

        public async Task<Player[]> GetPlayersByName(string name)
        {
            return await _repository.GetPlayersByName(name);
        }
    }
}