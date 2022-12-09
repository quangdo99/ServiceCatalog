using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoExample.MessageBusObject;
using MongoExample.Models;
using System.Text.Json;

namespace MongoExample.Services
{
    public class MongoDBServiceProduct
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly Sender _sender;

        public MongoDBServiceProduct(IOptions<MongoDBSettings> mongoDBSettings, IOptions<MessageBus> messageBusSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _productCollection = database.GetCollection<Product>(mongoDBSettings.Value.CollectionProductName);
            _sender = new Sender(messageBusSettings.Value.ConnectionString, messageBusSettings.Value.QueueName);
        }

        public async Task CreateAsync(Product product)
        {
            await _productCollection.InsertOneAsync(product);

            MessageProduct msgProduct = new MessageProduct(product);
            string jsonEntity = JsonSerializer.Serialize(msgProduct);
            await _sender.sendMessage(jsonEntity);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", id);
            await _productCollection.DeleteOneAsync(filter);
            return;
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _productCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateProduct(string id, Product product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", id);
            UpdateDefinition<Product> update = Builders<Product>.Update
                .Set("code", product.code)
                .Set("name", product.name)
                .Set("status", product.status)
                .Set("price", product.price)
                .Set("tag", product.tagList)
                .Set("update_time", DateTime.UtcNow);
            await _productCollection.UpdateOneAsync(filter, update);

            MessageProduct msgProduct = new MessageProduct(product.code, product.price, product.status);
            string jsonEntity = JsonSerializer.Serialize(msgProduct);
            await _sender.sendMessage(jsonEntity);
            return;
        }

        public async Task AddToTaglistAsync(string id, string tagStr)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", id);
            UpdateDefinition<Product> update = Builders<Product>.Update
                .AddToSet<string>("tagList", tagStr)
                .Set("update_time", DateTime.UtcNow);
            await _productCollection.UpdateOneAsync(filter, update);
            return;
        }
    }
}
