using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

using gameapi.Models;
using gameapi.MongoDB;
using gameapi.Exceptions;


namespace gameapi.Repositories
{
    public class MongoDBRepository : IPlayerRepository
    {
        private IMongoCollection<Player> _collection;
        private IMongoDatabase _database;

        public MongoDBRepository(MongoDBClient client)
        {
            _database = client.GetDatabase("game");
            _collection = _database.GetCollection<Player>("players");
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

        public async Task<Player[]> GetPlayersByMinScore(int score)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("_Score", score);
            List<Player> players = await _collection.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player> GetPlayerByName(string name)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p._Name, name);
            var cursor = await _collection.FindAsync(filter);
            Player player = await cursor.FirstAsync();
            return player;
        }

        public async Task<Player[]> GetPlayersByTag(string tag)
        {
            var players = await _collection.Find(_ => true).ToListAsync();
            List<Player> list = new List<Player>();
            foreach(Player p in players)
            {
                if(p._Tags.Contains(tag) == true)
                {
                    list.Add(p);
                }
            }
            return list.ToArray();
        }

        public async Task<Player[]> GetPlayersByProperty(string property)
        {
            var filter = Builders<Player>.Filter.ElemMatch(p => p._Items, i => i._Type == property);
            var list = await _collection.Find(filter).ToListAsync();
            return list.ToArray();
        }

        public async Task<Player[]> GetAllBySize(int size)
        {
            var filter = Builders<Player>.Filter.Size(p => p._Items, size);
            var list = await _collection.Find(filter).ToListAsync();
            return list.ToArray();
        }

        public async Task<Player> Create(Player player)
        {
            await _collection.InsertOneAsync(player);
            return player;
        }

        public async Task<Player> Modify(Guid id, string name)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, id);
            var update = Builders<Player>.Update.Set(p => p._Name, name);
            return await _collection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<Player> Delete(Guid playerid)
        {
            Player player = await Get(playerid);
            var filter = Builders<Player>.Filter.Eq(p => p._id, playerid);
            await _collection.DeleteOneAsync(filter);
            return player;
        }

        public async Task<Player> AddScore(Guid id, int score)
        {
            var filter = Builders<Player>.Filter.Eq(p => p._id, id);
            var update = Builders<Player>.Update.Inc(p => p._Score, score);
            return await _collection.FindOneAndUpdateAsync(filter, update);
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
            var update = Builders<Player>.Update.Push(p => p._Items, item);
            await _collection.FindOneAndUpdateAsync(filter, update);
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

        public async Task<Player> RemoveItem(Guid itemid, int amount)
        {
            var filter = Builders<Player>.Filter.ElemMatch(p => p._Items, i => i._ItemId == itemid);
            var update = Builders<Player>.Update.Inc(p => p._Score, amount).PullFilter(p => p._Items, i => i._ItemId == itemid);
            return await _collection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<int> GetCommonPlayerLevel()
        {
            var collection = _database.GetCollection<BsonDocument>("players");
            var aggregate = collection.Aggregate()
            .Project(new BsonDocument{{"_Level", 1}})
            .Group(new BsonDocument{{"_id", "$_Level"}, {"Count", new BsonDocument("$sum", 1)}})
            .Sort(new BsonDocument{{"Count", -1}})
            .Limit(1);

            BsonDocument result = await aggregate.FirstAsync();
            return result["_id"].AsInt32;
        }
    }
}