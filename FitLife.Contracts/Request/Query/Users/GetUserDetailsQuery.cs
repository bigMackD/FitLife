using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Users
{
    /// <summary>
    /// Query for retrieving details of a signed in user
    /// </summary>
    public sealed class GetUserDetailsQuery : IQuery
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        public string Id { get; set; }
    }
}
