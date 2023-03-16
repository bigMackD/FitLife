using FitLife.Shared.Infrastructure.Query;

namespace FitLife.Contracts.Request.Query.Users
{
    /// <summary>
    /// Query for retrieving profile details for a user
    /// </summary>
    public sealed class GetUserProfileQuery : IQuery
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        public string UserId { get; set; }

    }
}
