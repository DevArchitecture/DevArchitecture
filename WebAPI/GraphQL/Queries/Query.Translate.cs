using Business.Handlers.Translates.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{
    public async Task<DataResult<IEnumerable<Translate>>> GetTranslateList()
        => GetResponseWithResult(await Mediator.Send(new GetTranslatesQuery()));

    public async Task<DataResult<IEnumerable<TranslateDto>>> GetTranslateListDto()
        => GetResponseWithResult(await Mediator.Send(new GetTranslateListDtoQuery()));

    public async Task<DataResult<Translate>> GetTranslateById(GetTranslateQuery getTranslateQuery)
        => GetResponseWithResult(await Mediator.Send(getTranslateQuery));

    public async Task<DataResult<Dictionary<string, string>>> GetTranslateByLanguage(GetTranslatesByLangQuery getTranslatesByLangQuery)
        => GetResponseWithResult(await Mediator.Send(getTranslatesByLangQuery));
}
