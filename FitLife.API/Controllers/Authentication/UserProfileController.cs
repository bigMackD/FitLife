using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Users;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Authentication
{
    /// <summary>
    /// Controller for user profiles
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse> _getUserProfileHandler;

        /// <param name="getUserProfileHandler"></param>
        public UserProfileController(IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse> getUserProfileHandler)
        {
            _getUserProfileHandler = getUserProfileHandler;
        }

        /// <summary>
        /// Returns current user full name
        /// </summary>
        /// <response code="200">Returns current user Full Name</response>
        /// <returns>User full name</returns>
        [HttpGet]
        [Authorize]
        [Route("")]
        public Task<GetUserProfileResponse> GetUserProfile()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var query = new GetUserProfileQuery
            {
                UserId = userId
            };
            return _getUserProfileHandler.Handle(query);
        }
    }
}
