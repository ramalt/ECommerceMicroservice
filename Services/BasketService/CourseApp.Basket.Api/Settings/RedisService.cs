using StackExchange.Redis;

namespace CourseApp.Basket.Api.Settings;

public class RedisService
{
    private readonly string _host;
    private readonly int _port;
    private ConnectionMultiplexer _connectionMultiplexer;

    public RedisService(string host, int port)
    {
        _host = host;
        _port = port;
    }


    public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
    public IDatabase GetDb(int id=1) => _connectionMultiplexer.GetDatabase(id);
}
