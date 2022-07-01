using Business.Handlers.Languages.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Queries;

public partial class Query
{

    public async Task<DataResult<IEnumerable<Language>>> GetLanguageList()
        => GetResponseWithResult(await Mediator.Send(new GetLanguagesQuery()));

    public async Task<DataResult<Language>> GetLanguageById(GetLanguageQuery getLanguageQuery)
        => GetResponseWithResult(await Mediator.Send(getLanguageQuery));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetLanguageLookupList()
        => GetResponseWithResult(await Mediator.Send(new GetLanguagesLookUpQuery()));

    public async Task<DataResult<IEnumerable<SelectionItem>>> GetLanguageLookupListWithCode()
        => GetResponseWithResult(await Mediator.Send(new GetLanguagesLookUpWithCodeQuery()));
}
