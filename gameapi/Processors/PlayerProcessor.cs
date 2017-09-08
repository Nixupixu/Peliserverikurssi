using System;
using System.Threading.Tasks;
using gameapi.Repositories;

namespace gameapi.Processors
{
    public class PlayerProcessor
    {
        private PlayerInMemoryRepository _repository;

        public PlayerProcessor(PlayerInMemoryRepository repository)
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
        public async Task<Player> Create(NewPlayer player)
        {
            Player _player = new Player(Guid.NewGuid(), player._Name);
            return await _repository.Create(_player);
        }
        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            return await _repository.Modify(id, player);
        }
        public async Task<Player> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}