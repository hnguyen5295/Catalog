using Catalog.Entities;
using Catalog.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
  public class MongoDbItemsRepository : IItemsRepository
  {

    private readonly IMongoCollection<Item> itemsCollection;

    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

    public MongoDbItemsRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
      var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
      var mongoDb = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
      itemsCollection = mongoDb.GetCollection<Item>(mongoDbSettings.Value.CollectionName);
    }

    public async Task CreateItemAsync(Item item)
    {
      await itemsCollection.InsertOneAsync(item);
    }

    public async Task DeleteItemAsync(String id)
    {
      var filter = filterBuilder.Eq(item => item.Id, id);

      await itemsCollection.DeleteOneAsync(filter);
    }

    public async Task<Item> GetItemAsync(String id)
    {
      var filter = filterBuilder.Eq(item => item.Id, id);
      return await itemsCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
      return await itemsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateItemAsync(Item item)
    {
      var filter = filterBuilder.Eq(item => item.Id, item.Id);
      await itemsCollection.ReplaceOneAsync(filter, item);
    }
  }
}