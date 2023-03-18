using FitLife.Contracts.Request.Query.MealCategories;
using FitLife.Contracts.Response;
using FitLife.Contracts.Response.MealCategories;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="MealCategoriesController"/> class.
        /// </summary>
        /// <param name="getMealCategoriesQueryHandler">Instance of query handler</param>
        public MealCategoriesController(IQueryHandler<GetMealCategoriesQuery, GetMealCategoriesResponse> getMealCategoriesQueryHandler)
        {
            _getMealCategoriesQueryHandler = getMealCategoriesQueryHandler;
        }

        /// <summary>
        /// Returns all meal categories
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">All meal categories</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(GetMealCategoriesResponse), StatusCodes.Status200OK)]
        public GetMealCategoriesResponse Get([FromQuery] GetMealCategoriesQuery query)
        {
            return _getMealCategoriesQueryHandler.Handle(query);
        }
    }
}
