using AutoMapper;
using CookBook.Application.Services.Cryptography;
using CookBook.Application.Services.Token;
using CookBook.Application.Services.UserSession;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using CookBook.Domain.Entity;
using CookBook.Domain.Repository.ReceitaRepository;
using CookBook.Domain.Repository.UsuarioRepository;
using CookBook.Infrastructure.RepositoryAccess;
using CookBook.Infrastructure.RepositoryAccess.Repository;
using FactoryMethod.ConcreteCreator;
using FluentMigrator.Infrastructure;
using Strategy.ConcreteStrategy;
using Strategy.Context;
using System.Net;

namespace CookBook.Application.UseCases.Revenue.Register;
public class RevenueRegisterUseCase : IRevenueRegisterUseCase
{

    private readonly IReceitaWriteOnlyRepository _writeOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IWorkUnit _workUnit;
    private readonly IUserSession _session;

    public RevenueRegisterUseCase(IReceitaWriteOnlyRepository writeOnlyRepository, IMapper mapper, IWorkUnit workUnit, IUserSession session)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _mapper = mapper;
        _workUnit = workUnit;
        _session = session;
    }

    public async Task<GenericResponse<dynamic>> Execute(RevenueRequest request)
    {
        Validate(request);

        var usuario = await _session.GetUserBySession();

        var receita = _mapper.Map<Receita>(request);

        receita.UsuarioId = usuario.Id;

        await _writeOnlyRepository.Create(receita);

        await _workUnit.Commit();

        return FactoryMethod(_mapper.Map<RevenueResponse>(receita), (int)HttpStatusCode.Created);
    }

    private GenericResponse<dynamic> FactoryMethod(RevenueResponse data, int statusCode)
    {
        dynamic dynamicResponse = new System.Dynamic.ExpandoObject();
        dynamicResponse.Data = data;
        dynamicResponse.StatusCode = statusCode;
        dynamicResponse.Message = "Receita criada com sucesso!";

        var creator = new ConcreteCreatorSuccessResponse();

        return creator.SomeOperation(dynamicResponse);
    }

    private void Validate(RevenueRequest request)
    {
        var validator = new RevenueRegisterValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(validator =>  validator.ErrorMessage).ToList();
            var context = new Context(new ConcreteStrategyValidationException());

            context.ThrowException(errorMessage);

        }
    }
}
