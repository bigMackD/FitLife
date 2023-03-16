using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response;
using FitLife.Contracts.Response.Users;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        /// <response code="200">Current user full name</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpGet]
        [Authorize]
        [Route("")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(GetUserProfileResponse), StatusCodes.Status200OK)]
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
