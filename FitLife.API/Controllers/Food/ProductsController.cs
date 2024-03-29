﻿using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Products;
using FitLife.Contracts.Request.Query.Products;
using FitLife.Contracts.Response;
using FitLife.Contracts.Response.Product;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers.Food
{

    /// <summary>
    /// Controller for managing products
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IQueryHandler<GetProductsQuery, GetProductsResponse> _getProductsQueryHandler;
        private readonly IAsyncQueryHandler<GetProductDetailsQuery, GetProductDetailsResponse> _getProductDetailsQueryHandler;
        private readonly IAsyncCommandHandler<AddProductCommand, AddProductResponse> _addProductCommandHandler;
        private readonly IAsyncCommandHandler<EditProductCommand, EditProductResponse> _editProductCommandHandler;


        /// <param name="getProductsQueryHandler"></param>
        /// <param name="getProductDetailsQueryHandler"></param>
        /// <param name="addProductCommandHandler"></param>
        /// <param name="editProductCommandHandler"></param>
        public ProductsController(IQueryHandler<GetProductsQuery, GetProductsResponse> getProductsQueryHandler, IAsyncQueryHandler<GetProductDetailsQuery, GetProductDetailsResponse> getProductDetailsQueryHandler, IAsyncCommandHandler<AddProductCommand, AddProductResponse> addProductCommandHandler, IAsyncCommandHandler<EditProductCommand, EditProductResponse> editProductCommandHandler)
        {
            _getProductsQueryHandler = getProductsQueryHandler;
            _getProductDetailsQueryHandler = getProductDetailsQueryHandler;
            _addProductCommandHandler = addProductCommandHandler;
            _editProductCommandHandler = editProductCommandHandler;
        }

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">All products</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(GetProductsResponse), StatusCodes.Status200OK)]
        public GetProductsResponse Get([FromQuery] GetProductsQuery query)
        {
            return _getProductsQueryHandler.Handle(query);
        }

        /// <summary>
        /// Returns product by ID
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Product by specified ID</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(GetProductDetailsResponse), StatusCodes.Status200OK)]
        public Task<GetProductDetailsResponse> GetById([FromRoute] GetProductDetailsQuery query)
        {
            return _getProductDetailsQueryHandler.Handle(query);
        }

        /// <summary>
        /// Creates a Product
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Product created</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(AddProductResponse), StatusCodes.Status200OK)]
        public Task<AddProductResponse> Add([FromBody] AddProductCommand command)
        {
            return _addProductCommandHandler.Handle(command);
        }

        /// <summary>
        /// Updates a Product
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Product updated</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpPut]
        [Route("{Id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(EditProductResponse), StatusCodes.Status200OK)]
        public Task<EditProductResponse> Edit([FromBody] EditProductCommand command)
        {
            return _editProductCommandHandler.Handle(command);
        }
    }
}
