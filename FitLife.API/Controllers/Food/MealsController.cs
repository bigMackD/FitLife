using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Request.Query.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAsyncQueryHandler<GetMealsQuery, GetMealsResponse> _getMealsQueryHandler;
        private readonly IAsyncQueryHandler<GetMealDetailsQuery, GetMealDetailsResponse> _mealDetailsQueryHandler;
        private readonly IAsyncCommandHandler<EditMealCommand, EditMealResponse> _editMealCommandHandler;
        private readonly IAsyncCommandHandler<DeleteMealCommand, DeleteMealResponse> _deleteMealCommandHandler;

        
        /// <param name="addMealCommandHandler"></param>
        /// <param name="getMealsQueryHandler"></param>
        /// <param name="mealDetailsQueryHandler"></param>
        /// <param name="editMealCommandHandler"></param>
        /// <param name="deleteMealCommandHandler"></param>
        public MealsController(IAsyncCommandHandler<AddMealCommand, AddMealResponse> addMealCommandHandler,
            IAsyncQueryHandler<GetMealsQuery, GetMealsResponse> getMealsQueryHandler,
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
        [HttpPost]
        [Route("")]
        public Task<AddMealResponse> Add([FromBody] AddMealCommand command)
        {
            return _addMealCommandHandler.Handle(command);
        }

        /// <summary>
        /// Returns all meals
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">All meals</response>
        [HttpGet]
        [Route("")]
        public async Task<GetMealsResponse> Get([FromQuery] GetMealsQuery query)
        {
            return  await _getMealsQueryHandler.Handle(query);
        }

        /// <summary>
        /// Returns meal by ID
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">All meals</response>
        [HttpGet]
        [Route("{Id}")]
        public async Task<GetMealDetailsResponse> Details([FromRoute] GetMealDetailsQuery query)
        {
            return await _mealDetailsQueryHandler.Handle(query);
        }

        /// <summary>
        /// Updates a meal
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Meal updated</response>
        [HttpPut]
        [Route("{Id}")]
        public async Task<EditMealResponse> Edit([FromBody] EditMealCommand command)
        {
            return await _editMealCommandHandler.Handle(command);
        }

        /// <summary>
        /// Deletes a meal
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Meal deleted</response>
        [HttpDelete]
        [Route("{Id}")]
        public async Task<DeleteMealResponse> Delete([FromRoute] DeleteMealCommand command)
        {
            return await _deleteMealCommandHandler.Handle(command);
        }
    }
}
