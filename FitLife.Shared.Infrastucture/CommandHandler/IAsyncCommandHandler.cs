using System.Threading.Tasks;
using FitLife.Shared.Infrastructure.Command;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Shared.Infrastructure.CommandHandler
{
    public interface IAsyncCommandHandler<in TCommand, TResponse>
        where TCommand : ICommand
        where TResponse : IBaseResponse
    {
         Task<TResponse> Handle(TCommand command);
    }
}
