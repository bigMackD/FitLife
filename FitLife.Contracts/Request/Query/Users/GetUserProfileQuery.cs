using System.Reflection.Metadata;
using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Users
{
    public class GetUserProfileQuery : IQuery
    {
        public string UserId { get; set; }

    }
}
