using FitLife.Shared.Infrastucture.Query;
using FitLife.Shared.Infrastucture.Response;

namespace FitLife.Shared.Infrastucture.QueryHandler
{
    public interface IQueryHandler<in TQuery, out TResponse, T>
        where TQuery : IQuery
        where TResponse : IResponse<T>
    {
        TResponse Handle(TQuery query);

    }
}
