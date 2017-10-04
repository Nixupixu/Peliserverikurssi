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

            _collection = database.GetCollection<Player>("players");
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

        public async Task<Item[]> GetAllItems(Guid playerid)
        {
            Player player = await Get(playerid);
            return player._Items.ToArray();
        }

        public async Task<Item> GetItem(Guid playerid, Guid itemid)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            //_collection.FindOneAndUpdateAsync()
            return player._Items.Find(i => i._ItemId == itemid);
        }

        public async Task<Item> CreateItem(Guid playerid, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            player._Items.Add(item);
            await _collection.ReplaceOneAsync(filter, player);
            return item;
        }

        public async Task<Item> ModifyItem(Guid playerid, Guid itemid, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            Item modifiedItem = player._Items.Find(i => i._ItemId == itemid);
            player._Items.Remove(modifiedItem);
            player._Items.Add(item);

            await _collection.ReplaceOneAsync(filter, player);
            return item;
        }
        
        public async Task<Item> DeleteItem(Guid playerid, Guid itemid)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            Item deletedItem = player._Items.Find(i => i._ItemId == itemid);
            player._Items.Remove(deletedItem);

            await _collection.ReplaceOneAsync(filter, player);
            return deletedItem;
        }

        
    }
}