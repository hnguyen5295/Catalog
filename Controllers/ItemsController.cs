using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
  [ApiController]
  [Route("items")]
  public class ItemsController : ControllerBase
  {
    private readonly IItemsRepository repository;
    public ItemsController(IItemsRepository repository)
    {
      this.repository = repository;
    }

    // GET /items
    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
      var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
      return items;
    }

    // GET /items/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(String id)
    {
      var item = await repository.GetItemAsync(id);

      if (item is null)
      {
        return NotFound();
      }

      return item.AsDto();
    }

    // POST /items
    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
    {
      Item item = new()
      {
        Id = Guid.NewGuid().ToString(),
        Name = itemDto.Name,
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      await repository.CreateItemAsync(item);

      return CreatedAtAction((nameof(CreateItemAsync)), new { id = item.Id }, item.AsDto());
    }

    // PUT /items/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItem(String id, UpdateItemDto itemDto)
    {
      var existingItem = await repository.GetItemAsync(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      Item updateItem = existingItem with
      {
        Name = itemDto.Name,
        Price = itemDto.Price
      };

      await repository.UpdateItemAsync(updateItem);

      return NoContent();
    }

    // DELETE /item/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(String id)
    {
      var existingItem = await repository.GetItemAsync(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      await repository.DeleteItemAsync(existingItem.Id);

      return NoContent();
    }
  }
}