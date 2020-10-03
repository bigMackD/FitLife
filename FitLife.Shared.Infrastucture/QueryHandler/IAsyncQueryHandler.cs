using System.Threading.Tasks;
using FitLife.Shared.Infrastructure.Query;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Shared.Infrastructure.QueryHandler
{
    public interface IAsyncQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery
        where TResponse : IBaseResponse
    {
        Task<TResponse> Handle(TQuery query);

    }
}
