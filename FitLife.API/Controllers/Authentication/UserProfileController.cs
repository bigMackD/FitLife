using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Users;
using FitLife.DB.Models.Authentication;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse> _getUserProfileHandler;
        public UserProfileController(UserManager<AppUser> userManager, IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse> getUserProfileHandler)
        {
            _userManager = userManager;
            _getUserProfileHandler = getUserProfileHandler;
        }

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
