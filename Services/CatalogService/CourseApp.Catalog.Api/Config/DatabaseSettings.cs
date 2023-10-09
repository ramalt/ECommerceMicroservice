namespace CourseApp.Catalog.Api.Config;

public class DatabaseSettings : IDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CategoryCollectionName { get; set; } = null!;
    public string CourseCollectionName { get; set; } = null!;
}
