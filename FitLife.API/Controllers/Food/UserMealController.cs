using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.UserMeal;
using FitLife.Contracts.Request.Query.UserMeals;
using FitLife.Contracts.Response.UserMeals;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.QueryHandler;
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
        private readonly IAsyncQueryHandler<GetUserMealsByDateInternalQuery, GetUserMealsByDateResponse> _getUserMealsByDate;

        /// <param name="addUserMealCommandHandler"></param>
        /// <param name="getUserMealsByDate"></param>
        public UserMealController(IAsyncCommandHandler<AddUserMealCommand, AddUserMealResponse> addUserMealCommandHandler,
            IAsyncQueryHandler<GetUserMealsByDateInternalQuery, GetUserMealsByDateResponse> getUserMealsByDate)
        {
            _addUserMealCommandHandler = addUserMealCommandHandler;
            _getUserMealsByDate = getUserMealsByDate;
        }

        /// <summary>
        /// Creates a user meal for a currently logged in user
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

        /// <summary>
        /// Returns all user meals for a logged in user by date
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">User meals by date</response>
        [HttpGet]
        [Route("{Date}")]
        public Task<GetUserMealsByDateResponse> Get([FromRoute] GetUserMealsByDateQuery query)
        {
            var internalQuery = new GetUserMealsByDateInternalQuery
            {
                Date = query.Date,
                Id = User.Claims.First(c => c.Type == "UserID").Value
            };
            return _getUserMealsByDate.Handle(internalQuery);
        }
    }
}
