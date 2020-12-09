using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Response.Meals;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Food
{
    /// <summary>
    /// Controller for managing meals
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IAsyncCommandHandler<AddMealCommand, AddMealResponse> _addMealCommandHandler;

        public MealsController(IAsyncCommandHandler<AddMealCommand, AddMealResponse> addMealCommandHandler)
        {
            _addMealCommandHandler = addMealCommandHandler;
        }

        /// <summary>
        /// Creates a Meal
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns the newly created item</response>
        [HttpPost]
        [Route("")]
        public Task<AddMealResponse> Add([FromBody] AddMealCommand command)
        {
            return _addMealCommandHandler.Handle(command);
        }
    }
}
