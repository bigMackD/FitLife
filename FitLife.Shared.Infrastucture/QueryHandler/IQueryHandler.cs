﻿using FitLife.Shared.Infrastructure.Query;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Shared.Infrastructure.QueryHandler
{
    public interface IQueryHandler<in TQuery, out TResponse>
        where TQuery : IQuery
        where TResponse : IBaseResponse
    {
        TResponse Handle(TQuery query);

    }
}
