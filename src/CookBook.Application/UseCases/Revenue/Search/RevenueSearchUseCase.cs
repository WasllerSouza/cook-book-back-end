using AutoMapper;
using CookBook.Application.Services.UserSession;
using CookBook.Communication.Response;
using CookBook.Domain.Entity;
using CookBook.Domain.Repository.ReceitaRepository;
using CookBook.Exceptions;
using FactoryMethod.ConcreteCreator;
using Microsoft.AspNetCore.Http;
using Strategy.ConcreteStrategy;
using Strategy.Context;

namespace CookBook.Application.UseCases.Revenue.Search;
public class RevenueSearchUseCase : IRevenueSearchUseCase
{
    private readonly IReceitaReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserSession _session;
    public RevenueSearchUseCase(IReceitaReadOnlyRepository repository, IMapper mapper, IUserSession session)
    {
        _repository = repository;
        _mapper = mapper;
        _session = session;
    }
    public async Task<GenericResponse<RevenueResponse>> Execute(Guid id)
    {
        var session = await _session.GetUserBySession();

        var receita = await _repository.GetById(id);
        Validate(session, receita);

        return FactoryMethod(receita, (int)StatusCodes.Status200OK);
    }

    private GenericResponse<RevenueResponse> FactoryMethod(Receita receitas, int statusCode)
    {
        dynamic dynamicResponse = new System.Dynamic.ExpandoObject();
        dynamicResponse.Data = _mapper.Map<RevenueResponse>(receitas);
        dynamicResponse.StatusCode = statusCode;

        var creator = new ConcreteCreatorSuccessResponse<RevenueResponse>();

        return creator.SomeOperation(dynamicResponse);
    }

    private void Validate(Usuario usuario, Receita receita)
    {
        if (receita == null || !receita.UsuarioId.Equals(usuario.Id))
        {
            var context = new Context(new ConcreteStrategyValidationException());
            context.ThrowException(new List<string>
            {
                ResourceMessageError.RECEITA_NAO_ENCONTRADA
            });
        }
    }

}

