using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Core.Utilities.MessageBrokers;

public interface IMessageBrokerHelper
{
    Task<IResult> QueueMessageAsync<T>(T messageModel);
}