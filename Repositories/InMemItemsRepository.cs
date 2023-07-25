using Catalog.Entities;

namespace Catalog.Repositories
{

  public class InMemItemsRepository : IItemsRepository
  {
    private readonly List<Item> items = new()
    {
      new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Potion 1", Price = 19, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Potion 2", Price = 29, CreatedDate = DateTimeOffset.UtcNow }
    };

    public IEnumerable<Item> GetItems()
    {
      return items;
    }

    public Item GetItem(Guid id)
    {
      return items.Where(item => item.Id == id).SingleOrDefault();
    }
  }
}