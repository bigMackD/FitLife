using System;
using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Users
{
   public class GetUserDetailsQuery : IQuery
    {
        public string Id { get; set; }
    }
}
