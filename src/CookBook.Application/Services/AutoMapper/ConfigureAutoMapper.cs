using AutoMapper;
using CookBook.Communication.Request;
using CookBook.Domain.Entity;

namespace CookBook.Application.Services.AutoMapper;

public class ConfigureAutoMapper : Profile
{
    public ConfigureAutoMapper()
    {
        CreateMap<UserRegisterRequest, Usuario>()
            .ForMember(destiny => destiny.Senha, config => config.Ignore());
    }
}
