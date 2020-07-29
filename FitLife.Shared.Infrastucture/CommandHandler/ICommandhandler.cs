using FitLife.Shared.Infrastucture.Command;
using FitLife.Shared.Infrastucture.Response;

namespace FitLife.Shared.Infrastucture.CommandHandler
{
    public interface ICommandHandler<in TCommand, out TResponse>
        where TCommand : ICommand
        where TResponse : IBaseResponse
    {
        TResponse Handle(TCommand command);
    }
}
