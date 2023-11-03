using AutoMapper;
using CookBook.Application.Services.AutoMapper;

namespace Utils.Test.Mapper;

public class MapperBuilder
{
    public static IMapper Instance()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<ConfigureAutoMapper>();
        });

        return configuration.CreateMapper();
    }
}
