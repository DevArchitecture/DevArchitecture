using Business.Handlers.Languages.Commands;
using Core.Utilities.Results;

namespace WebAPI.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Result> AddLanguage(CreateLanguageCommand createLanguageCommand)
        => GetResponse(await Mediator.Send(createLanguageCommand));

    public async Task<Result> UpdateLanguage(UpdateLanguageCommand updateLanguageCommand)
        => GetResponse(await Mediator.Send(updateLanguageCommand));

    public async Task<Result> DeleteLanguage(DeleteLanguageCommand deleteLanguageCommand)
        => GetResponse(await Mediator.Send(deleteLanguageCommand));
}
