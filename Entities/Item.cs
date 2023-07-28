using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Entities
{
  public record Item
  {
    [BsonId]
    public string? Id { get; init; }

    [BsonElement("Name")]
    public string Name { get; init; }

    public decimal Price { get; init; }

    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset CreatedDate { get; init; }
  }
}