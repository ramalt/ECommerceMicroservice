using AutoMapper;

namespace CourseApp.Order.Application.Config;

public static class ObjectMapper
{
    private static readonly Lazy<IMapper> _mapper = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(opt =>
        {
            opt.AddProfile<OrderMapping>();
        });

        return config.CreateMapper();
    });

    public static IMapper Mapper => _mapper.Value;
}
