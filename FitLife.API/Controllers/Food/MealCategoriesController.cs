using FitLife.Contracts.Request.Query.MealCategories;
using FitLife.Contracts.Response.MealCategories;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FitLife.API.Controllers.Food
{
    /// <summary>
    /// Controller for managing meal categories
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MealCategoriesController : ControllerBase
    {
        private readonly IQueryHandler<GetMealCategoriesQuery, GetMealCategoriesResponse> _getMealCategoriesQueryHandler;
        public MealCategoriesController(IQueryHandler<GetMealCategoriesQuery, GetMealCategoriesResponse> getMealCategoriesQueryHandler)
        {
            _getMealCategoriesQueryHandler = getMealCategoriesQueryHandler;
        }

        /// <summary>
        /// Returns all meal categories
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">All meal categories</response>
        [HttpGet]
        [Route("")]
        public GetMealCategoriesResponse Get([FromQuery] GetMealCategoriesQuery query)
        {
            return _getMealCategoriesQueryHandler.Handle(query);
        }
    }
}
