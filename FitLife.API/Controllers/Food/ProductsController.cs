using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Products;
using FitLife.Contracts.Request.Query.Products;
using FitLife.Contracts.Response.Product;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Food
{

    /// <summary>
    /// Controller for managing products
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IQueryHandler<GetProductsQuery, GetProductsResponse> _getProductsQueryHandler;
        private readonly IAsyncQueryHandler<GetProductDetailsQuery, GetProductDetailsResponse> _getProductDetailsQueryHandler;
        private readonly IAsyncCommandHandler<AddProductCommand, AddProductResponse> _addProductCommandHandler;

        
        /// <param name="getProductsQueryHandler"></param>
        /// <param name="getProductDetailsQueryHandler"></param>
        /// <param name="addProductCommandHandler"></param>
        public ProductsController(IQueryHandler<GetProductsQuery, GetProductsResponse> getProductsQueryHandler, IAsyncQueryHandler<GetProductDetailsQuery, GetProductDetailsResponse> getProductDetailsQueryHandler, IAsyncCommandHandler<AddProductCommand, AddProductResponse> addProductCommandHandler)
        {
            _getProductsQueryHandler = getProductsQueryHandler;
            _getProductDetailsQueryHandler = getProductDetailsQueryHandler;
            _addProductCommandHandler = addProductCommandHandler;
        }

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Returns all products</response>
        [HttpGet]
        [Route("")]
        public GetProductsResponse Get([FromQuery] GetProductsQuery query)
        {
            return _getProductsQueryHandler.Handle(query);
        }

        /// <summary>
        /// Returns product by ID
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Returns product by specified ID</response>
        [HttpGet]
        [Route("{Id}")]
        public Task<GetProductDetailsResponse> GetById([FromRoute] GetProductDetailsQuery query)
        {
            return _getProductDetailsQueryHandler.Handle(query);
        }

        /// <summary>
        /// Creates a Product
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns the newly created item</response>
        [HttpPost]
        [Route("")]
        public Task<AddProductResponse> Add([FromBody] AddProductCommand command)
        {
            return _addProductCommandHandler.Handle(command);
        }
    }
}
