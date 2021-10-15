using System.Threading.Tasks;

namespace TaskSubscriber.Func.Services
{
    public interface IMessagingService
    {
        Task SendSmsAsync(string message); 
    }
}
