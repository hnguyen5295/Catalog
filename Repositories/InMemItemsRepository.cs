using Catalog.Entities;

namespace Catalog.Repositories
{

  public class InMemItemsRepository : IItemsRepository
  {
    private readonly List<Item> items = new()
    {
      new Item { Id = Guid.NewGuid().ToString(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid().ToString(), Name = "Potion 1", Price = 19, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid().ToString(), Name = "Potion 2", Price = 29, CreatedDate = DateTimeOffset.UtcNow }
    };

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
      return await Task.FromResult(items);
    }

    public async Task<Item> GetItemAsync(String id)
    {
      var item = items.Where(item => item.Id == id).SingleOrDefault();
      return await Task.FromResult(item);
    }

    public async Task CreateItemAsync(Item item)
    {
      items.Add(item);
      await Task.CompletedTask;
    }

    public async Task UpdateItemAsync(Item item)
    {
      var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
      items[index] = item;
      await Task.CompletedTask;
    }

    public async Task DeleteItemAsync(String id)
    {
      var index = items.FindIndex(existingItem => existingItem.Id == id);
      items.RemoveAt(index);
      await Task.CompletedTask;
    }
  }
}