using AutoMapper;
using CookBook.Application.Services.UserSession;
using CookBook.Communication.Request;
using CookBook.Communication.Response;
using CookBook.Domain.Entity;
using CookBook.Domain.Repository.ReceitaRepository;
using FactoryMethod.ConcreteCreator;
using Microsoft.AspNetCore.Http;

namespace CookBook.Application.UseCases.DashBoard;
public class DashBoardUseCase : IDashBoardUseCase
{

    private readonly IReceitaReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserSession _session;
    public DashBoardUseCase(IReceitaReadOnlyRepository repository, IMapper mapper, IUserSession session)
    {
        _repository = repository;
        _mapper = mapper;
        _session = session;
    }
    public async Task<GenericResponse<IList<DashBoardResponse>>> Execute(DashBoardRequest request)
    {
        var session = await _session.GetUserBySession();

        var receitas = await _repository.GetAllByUser(session.Id);

        receitas = Filter(request, receitas);

        return FactoryMethod(receitas, (int)StatusCodes.Status200OK);
    }

    private GenericResponse<IList<DashBoardResponse>> FactoryMethod(IList<Receita> receitas, int statusCode)
    {
        dynamic dynamicResponse = new System.Dynamic.ExpandoObject();
        dynamicResponse.Data = _mapper.Map<List<DashBoardResponse>>(receitas);
        dynamicResponse.StatusCode = statusCode;

        var creator = new ConcreteCreatorSuccessResponse<IList<DashBoardResponse>>();

        return creator.SomeOperation(dynamicResponse);
    }

    private static IList<Receita> Filter(DashBoardRequest request, IList<Receita> receitas)
    {
        var FilteredRecipes = receitas;
        if (request.Filter.HasValue)
        {
            FilteredRecipes = receitas.Where(r => r.Categoria == (Domain.Enum.Categoria)request.Filter.Value).ToList();
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            FilteredRecipes = receitas.Where(r => r.Titulo.Contains(request.Search) || r.Ingredientes.Any(ingrediente => ingrediente.Produto.Contains(request.Search))).ToList();
        }

        return FilteredRecipes;
    }
}
