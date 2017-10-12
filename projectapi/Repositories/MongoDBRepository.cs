using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;

using projectapi.Models;
using projectapi.MongoDB;
using projectapi.Exceptions;

namespace projectapi.Repositories
{
    public class MongoDBRepository : IPlayerRepository
    {
        private IMongoCollection<Player> _collection;

        public MongoDBRepository(MongoDBClient client)
        {
            IMongoDatabase database = client.GetDatabase("game");

            _collection = database.GetCollection<Player>("users");
        }

        public async Task<Player> Get(Guid playerid)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            var cursor = await _collection.FindAsync(filter);
            bool playerFound = await cursor.AnyAsync();
            if(playerFound)
            {
                cursor = await _collection.FindAsync(filter);
                return await cursor.FirstAsync();
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public async Task<Player[]> GetAll()
        {
            var list = await _collection.Find(_ => true).ToListAsync();
            return list.ToArray();
        }

        public async Task<Player[]> GetPlayersByMinLevel(int? level)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("_Level", level);
            List<Player> players = await _collection.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player[]> GetPlayersByName(string name)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_Name", name);
            List<Player> players = await _collection.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player> Create(Player player)
        {
            await _collection.InsertOneAsync(player);
            return player;
        }

        public async Task<Player> Modify(Player player)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, player._id);
            await _collection.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Player> Delete(Guid playerid)
        {
            Player player = await Get(playerid);
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            await _collection.DeleteOneAsync(filter);
            return player;
        }

        public async Task<Character[]> GetAllChars(Guid playerid)
        {
            Player player = await Get(playerid);
            return player._Characters.ToArray();
        }

        public async Task<Character[]> GetAllCharsByUsername(string username)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_Name", username);
            Player players = await _collection.Find(filter).FirstAsync();
            return players._Characters.ToArray();
        }

        public async Task<Character> GetChar(Guid playerid, Guid charid)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            return player._Characters.Find(i => i._CharId == charid);
        }

        public async Task<Character> CreateChar(Guid playerid, Character character)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            player._Characters.Add(character);
            await _collection.ReplaceOneAsync(filter, player);
            return character;
        }

        public async Task<Character> ModifyChar(Guid playerid, Guid charid, Character character)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            Character modifiedChar = player._Characters.Find(i => i._CharId == charid);
            player._Characters.Remove(modifiedChar);
            player._Characters.Add(character);

            await _collection.ReplaceOneAsync(filter, player);
            return character;
        }

        public async Task<Character> DeleteChar(Guid playerid, Guid charid)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            Character deletedChar = player._Characters.Find(i => i._CharId == charid);
            player._Characters.Remove(deletedChar);

            await _collection.ReplaceOneAsync(filter, player);
            return deletedChar;
        }
    }
}