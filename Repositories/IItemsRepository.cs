using Catalog.Entities;

namespace Catalog.Repositories
{
  public interface IItemsRepository
  {
    Task<Item> GetItemAsync(String id);
    Task<IEnumerable<Item>> GetItemsAsync();
    Task CreateItemAsync(Item item);
    Task UpdateItemAsync(Item item);
    Task DeleteItemAsync(String id);
  }
}