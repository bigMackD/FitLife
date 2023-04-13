namespace FitLife.Consumer.Services.CallbackService
{
    public interface ICallbackService
    {
        Task Callback(Guid processId);
    }
}
