namespace CourseApp.WebMVC.Settings;

public class ServiceApiSettings
{
    public string BaseUri { get; set; } = null!;
    public string IdentityBaseUri { get; set; } = null!;
    public string PhotoStockUri { get; set; } = null!;
    public ServiceApi CatalogService { get; set; }

}

public class ServiceApi
{
    public string Path { get; set; }
}
