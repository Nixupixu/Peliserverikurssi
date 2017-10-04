using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;

using projectapi.Models;
using projectapi.MongoDB;

namespace projectapi.Repositories
{
    public class MongoDBRepository : IUserRepository
    {
        private IMongoCollection<User> _collection;

        public MongoDBRepository(MongoDBClient client)
        {
            IMongoDatabase database = client.GetDatabase("project");

            _collection = database.GetCollection<User>("users");
        }

        public async Task<User> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(string name)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("_Name", name);
            return await _collection.Find(filter).FirstAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            await _collection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> ModifyUser(Guid id, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}