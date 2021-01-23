using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.UserMeal;
using FitLife.Contracts.Response.UserMeal;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Food
{
    /// <summary>
    /// Controller for managing users meals 
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserMealController : ControllerBase
    {
        private readonly IAsyncCommandHandler<AddUserMealCommand, AddUserMealResponse> _addUserMealCommandHandler;

        /// <param name="addUserMealCommandHandler"></param>
        public UserMealController(IAsyncCommandHandler<AddUserMealCommand, AddUserMealResponse> addUserMealCommandHandler)
        {
            _addUserMealCommandHandler = addUserMealCommandHandler;
        }

        /// <summary>
        /// Creates a user meal
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">User meal created</response>
        [HttpPost]
        [Route("")]
        public Task<AddUserMealResponse> Add([FromBody] AddUserMealCommand command)
        {
            command.UserId = User.Claims.First(c => c.Type == "UserID").Value;
            return _addUserMealCommandHandler.Handle(command);
        }
    }
}
