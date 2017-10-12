using System;
using System.Threading.Tasks;
using gameapi.Repositories;
using gameapi.Models;

namespace gameapi.Processors
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
                _Level = 1,
                _Score = 0
            };
            return _repository.Create(_player);
        }
        
        public async Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer)
        {
            return await _repository.Modify(id, modifiedPlayer._Name);
        }
        public async Task<Player> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async Task<Player[]> GetPlayersByMinScore(int score)
        {
            return await _repository.GetPlayersByMinScore(score);
        }

        public async Task<Player> GetPlayerByName(string name)
        {
            return await _repository.GetPlayerByName(name);
        }

        public async Task<Player[]> GetPlayersByProperty(string property)
        {
            return await _repository.GetPlayersByProperty(property);
        }

        public async Task<Player[]> GetAllBySize(int size)
        {
            return await _repository.GetAllBySize(size);
        }

        public async Task<Player[]> GetPlayersByTag(string tag)
        {
            return await _repository.GetPlayersByTag(tag);
        }

        public async Task<int> GetCommonPlayerLevel()
        {
            return await _repository.GetCommonPlayerLevel();
        }

        public async Task<Player> AddScore(Guid id, int score)
        {
            return await _repository.AddScore(id, score);
        }

        public async Task<Player> AddTag(Guid id, string tag)
        {
            return await _repository.AddTag(id, tag);
        }
    }
}