using FitLife.Shared.Infrastructure.Command;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Shared.Infrastructure.CommandHandler
{
    public interface ICommandHandler<in TCommand, out TResponse>
        where TCommand : ICommand
        where TResponse : IBaseResponse
    {
        TResponse Handle(TCommand command);
    }
}
