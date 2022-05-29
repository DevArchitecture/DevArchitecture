using Business.Handlers.Translates.Commands;
using Core.Utilities.Results;
using MediatR;

namespace WebAPI.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Result> AddTranslate(CreateTranslateCommand createTranslateCommand)
        => GetResponse(await Mediator.Send(createTranslateCommand));

    public async Task<Result> UpdateTranslate(UpdateTranslateCommand updateTranslateCommand)
        => GetResponse(await Mediator.Send(updateTranslateCommand));

    public async Task<Result> DeleteTranslate(DeleteTranslateCommand deleteTranslateCommand)
        => GetResponse(await Mediator.Send(deleteTranslateCommand));
}
