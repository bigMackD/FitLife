using FitLife.Contracts.Events;

namespace FitLife.Consumer.Shared.Infrastructure.Services.Callback
{
    public interface ICallbackService
    {
        Task Callback(EventProcessed result);
    }
}
