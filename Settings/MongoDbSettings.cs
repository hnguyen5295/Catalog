namespace Catalog.Settings
{
  public class MongoDbSettings
  {
    public string Host { get; set; } = null!;

    public string Port { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CollectionName { get; set; } = null!;

    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ConnectionString
    {
      get
      {
        return $"mongodb://{User}:{Password}@{Host}:{Port}";
      }
    }
  }
}