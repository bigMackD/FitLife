using System.Threading.Tasks;
using FitLife.Contracts.Request.Query.Products;
using FitLife.Contracts.Response.Product;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Food
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IQueryHandler<GetProductsQuery, GetProductsResponse> _getProductsQueryHandler;
        private readonly IAsyncQueryHandler<GetProductDetailsQuery, GetProductDetailsResponse> _getProductDetailsQueryHandler;

        public ProductsController(IQueryHandler<GetProductsQuery, GetProductsResponse> getProductsQueryHandler, IAsyncQueryHandler<GetProductDetailsQuery, GetProductDetailsResponse> getProductDetailsQueryHandler)
        {
            _getProductsQueryHandler = getProductsQueryHandler;
            _getProductDetailsQueryHandler = getProductDetailsQueryHandler;
        }

        [HttpGet]
        [Route("")]
        public GetProductsResponse Get([FromQuery] GetProductsQuery command)
        {
            return _getProductsQueryHandler.Handle(command);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<GetProductDetailsResponse> GetById([FromRoute] GetProductDetailsQuery command)
        {
            return _getProductDetailsQueryHandler.Handle(command);
        }
    }
}
