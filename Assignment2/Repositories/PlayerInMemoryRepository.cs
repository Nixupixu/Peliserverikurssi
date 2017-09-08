using System;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Repositories
{
    public class PlayerInMemoryRepository : IPlayerRepository
    {
        public class PlayerController : Controller
    {
        private PlayerProcessor _processor;

    public Task<Player> Get(Guid id);
    public Task<Player[]> GetAll();
    
    public async Task<Player> Create(NewPlayer player)
    {

    }

    [HttpPut("{id}")]
    public Task<Player> Modify(Guid id, ModifiedPlayer player)
    {

    }
    public Task<Player> Delete(Guid id);
    }
    }
}