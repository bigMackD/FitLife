using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Request.Query.Meals;
using FitLife.Contracts.Response;
using FitLife.Contracts.Response.Meals;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Food
{
    /// <summary>
    /// Controller for managing meals
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IAsyncCommandHandler<AddMealCommand, AddMealResponse> _addMealCommandHandler;
        private readonly IQueryHandler<GetMealsQuery, GetMealsResponse> _getMealsQueryHandler;
        private readonly IAsyncQueryHandler<GetMealDetailsQuery, GetMealDetailsResponse> _mealDetailsQueryHandler;
        private readonly IAsyncCommandHandler<EditMealCommand, EditMealResponse> _editMealCommandHandler;
        private readonly IAsyncCommandHandler<DeleteMealCommand, DeleteMealResponse> _deleteMealCommandHandler;

        
        /// <param name="addMealCommandHandler"></param>
        /// <param name="getMealsQueryHandler"></param>
        /// <param name="mealDetailsQueryHandler"></param>
        /// <param name="editMealCommandHandler"></param>
        /// <param name="deleteMealCommandHandler"></param>
        public MealsController(IAsyncCommandHandler<AddMealCommand, AddMealResponse> addMealCommandHandler,
            IQueryHandler<GetMealsQuery, GetMealsResponse> getMealsQueryHandler,
            IAsyncQueryHandler<GetMealDetailsQuery, GetMealDetailsResponse> mealDetailsQueryHandler,
            IAsyncCommandHandler<EditMealCommand, EditMealResponse> editMealCommandHandler,
            IAsyncCommandHandler<DeleteMealCommand, DeleteMealResponse> deleteMealCommandHandler)
        {
            _addMealCommandHandler = addMealCommandHandler;
            _getMealsQueryHandler = getMealsQueryHandler;
            _mealDetailsQueryHandler = mealDetailsQueryHandler;
            _editMealCommandHandler = editMealCommandHandler;
            _deleteMealCommandHandler = deleteMealCommandHandler;
        }

        /// <summary>
        /// Creates a Meal
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Meal created</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(AddMealResponse), StatusCodes.Status200OK)]
        public Task<AddMealResponse> Add([FromBody] AddMealCommand command)
        {
            return _addMealCommandHandler.Handle(command);
        }

        /// <summary>
        /// Returns all meals
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">All meals</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(GetMealsResponse), StatusCodes.Status200OK)]
        public GetMealsResponse Get([FromQuery] GetMealsQuery query)
        {
            return  _getMealsQueryHandler.Handle(query);
        }

        /// <summary>
        /// Returns meal by ID
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">All meals</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(GetMealDetailsResponse), StatusCodes.Status200OK)]
        public async Task<GetMealDetailsResponse> Details([FromRoute] GetMealDetailsQuery query)
        {
            return await _mealDetailsQueryHandler.Handle(query);
        }

        /// <summary>
        /// Updates a meal
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Meal updated</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpPut]
        [Route("{Id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(EditMealResponse), StatusCodes.Status200OK)]
        public async Task<EditMealResponse> Edit([FromBody] EditMealCommand command)
        {
            return await _editMealCommandHandler.Handle(command);
        }

        /// <summary>
        /// Deletes a meal
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Meal deleted</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpDelete]
        [Route("{Id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(DeleteMealResponse), StatusCodes.Status200OK)]
        public async Task<DeleteMealResponse> Delete([FromRoute] DeleteMealCommand command)
        {
            return await _deleteMealCommandHandler.Handle(command);
        }
    }
}
