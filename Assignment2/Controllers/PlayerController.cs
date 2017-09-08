using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    public class PlayerController : Controller
    {
        private PlayerProcessor _processor;

        public Task<Player> Get(Guid id);
        public Task<Player[]> GetAll();
        
        public Task<Player> Create(NewPlayer player);

        [HttpPut("{id}")]
        public Task<Player> Modify(Guid id, ModifiedPlayer player);

        [HttpDelete("{id}")]
        public Task<Player> Delete(Guid id)
        {
            
        }
    }
}