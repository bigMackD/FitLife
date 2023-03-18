using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.UserMeal;
using FitLife.Contracts.Request.Query.UserMeals;
using FitLife.Contracts.Response;
using FitLife.Contracts.Response.UserMeals;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Food
{
    /// <summary>
    /// Controller for managing users meals 
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserMealsController : ControllerBase
    {
        private readonly IAsyncCommandHandler<AddUserMealCommand, AddUserMealResponse> _addUserMealCommandHandler;
        private readonly IQueryHandler<GetUserMealsByDateInternalQuery, GetUserMealsByDateResponse> _getUserMealsByDate;
        private readonly IAsyncCommandHandler<DeleteUserMealsCommand, DeleteUserMealsReponse> _deleteUserMeals;

        /// <param name="addUserMealCommandHandler"></param>
        /// <param name="getUserMealsByDate"></param>
        /// <param name="deleteUserMeals"></param>
        public UserMealsController(IAsyncCommandHandler<AddUserMealCommand, AddUserMealResponse> addUserMealCommandHandler,
            IQueryHandler<GetUserMealsByDateInternalQuery, GetUserMealsByDateResponse> getUserMealsByDate,
            IAsyncCommandHandler<DeleteUserMealsCommand, DeleteUserMealsReponse> deleteUserMeals)
        {
            _addUserMealCommandHandler = addUserMealCommandHandler;
            _getUserMealsByDate = getUserMealsByDate;
            _deleteUserMeals = deleteUserMeals;
        }

        /// <summary>
        /// Creates a user meal for a currently logged in user
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">User meal created</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(AddUserMealResponse), StatusCodes.Status200OK)]
        public Task<AddUserMealResponse> Add([FromBody] AddUserMealCommand command)
        {
            command.UserId = User.Claims.First(c => c.Type == "UserID").Value;
            return _addUserMealCommandHandler.Handle(command);
        }

        /// <summary>
        /// Returns all meals for a logged in user by date
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">User meals by date</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpGet]
        [Route("{Date}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(GetUserMealsByDateResponse), StatusCodes.Status200OK)]
        public GetUserMealsByDateResponse Get([FromRoute] GetUserMealsByDateQuery query)
        {
            var internalQuery = new GetUserMealsByDateInternalQuery
            {
                Date = query.Date,
                Id = User.Claims.First(c => c.Type == "UserID").Value
            };
            return _getUserMealsByDate.Handle(internalQuery);
        }

        /// <summary>
        /// Deletes user meals specified by ids
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">User meals deleted</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(DeleteUserMealsReponse), StatusCodes.Status200OK)]
        public Task<DeleteUserMealsReponse> Get([FromBody] DeleteUserMealsCommand command)
        {
            return _deleteUserMeals.Handle(command);
        }
    }
}
