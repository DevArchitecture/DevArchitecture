namespace Core.Utilities.MessageBrokers;
using System.Threading.Tasks;

public interface IMessageConsumer
{
    Task GetQueue();
}