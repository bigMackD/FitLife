using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FitLife.Shared.Infrastucture.Command;
using FitLife.Shared.Infrastucture.Response;

namespace FitLife.Shared.Infrastucture.CommandHandler
{
    public interface IAsyncCommandHandler<in TCommand, TResponse>
        where TCommand : ICommand
        where TResponse : IBaseResponse
    {
         Task<TResponse> Handle(TCommand command);
    }
}
