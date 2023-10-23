
namespace CourseApp.WebMVC.Settings;

public class ClientSettings
{
    public Client WebClient { get; set; } = null!;
    public Client WebUserClientSettings { get; set; } = null!;
}

public class Client
{
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
}
