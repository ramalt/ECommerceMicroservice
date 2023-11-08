namespace CourseApp.WebMVC.Settings;

public class ServiceApiSettings
{
    public string BaseUri { get; set; } = null!;
    public string IdentityBaseUri { get; set; } = null!;
    public ServiceApi PhotoService { get; set; } = null!;
    public ServiceApi CatalogService { get; set; } = null!;

}

public class ServiceApi
{
    public string Path { get; set; }
}
