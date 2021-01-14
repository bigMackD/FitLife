using System.Collections.Generic;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Product
{
    public class GetProductsResponse : IBaseResponse, IPagingResponse
    {
     
        public IEnumerable<Product> Products { get; set; }
        public int Count { get; set; }
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Calories { get; set; }
        public decimal ProteinsGrams { get; set; }
        public decimal CarbsGrams { get; set; }
        public decimal FatsGrams { get; set; }
        public bool Deleted { get; set; }

    }
}
