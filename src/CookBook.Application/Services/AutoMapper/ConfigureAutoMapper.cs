using AutoMapper;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using CookBook.Domain.Entity;

namespace CookBook.Application.Services.AutoMapper;

public class ConfigureAutoMapper : Profile
{
    public ConfigureAutoMapper()
    {
        RequestFromEntity();
        EntityFromResponse();
    }

    private void RequestFromEntity()
    {
        CreateMap<UserRegisterRequest, Usuario>()
            .ForMember(destiny => destiny.Senha, config => config.Ignore());
        CreateMap<RevenueRequest, Receita>();
        CreateMap<IngredientRequest, Ingrediente>();
    }
    private void EntityFromResponse()
    {
        CreateMap<Receita, RevenueResponse> ();
        CreateMap<Ingrediente, IngredientResponse>();
        CreateMap<Receita, DashBoardResponse>()
            .ForMember(destiny => destiny.QuantidadeIngredientes, config => config.MapFrom(origin => origin.Ingredientes.Count));
    }
}
