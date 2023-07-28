namespace Catalog.Dtos
{
  public record ItemDto
  {
    public String Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
  }
}