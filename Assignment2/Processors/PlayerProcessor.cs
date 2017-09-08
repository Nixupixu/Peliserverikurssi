namespace Assignment2.Processors
{
    public class PlayerProcessor
    {
        //Tänne implementaatio PlayerControllerista

        private IPlayerInMemoryRepository _repository;
        public Task<Player> Get(Guid id)
        {

        }
        public Task<Player[]> GetAll()
        {

        }
        public Task<Player> Create(NewPlayer player)
        {

        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {

        }
        public Task<Player> Delete(Guid id)
        {
            
        }
    }
}