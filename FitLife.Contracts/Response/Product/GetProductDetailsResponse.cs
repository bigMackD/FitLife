using System;
using System.Collections.Generic;
using System.Text;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Product
{
   public class GetProductDetailsResponse : IBaseResponse
    {
        public Product Product { get; set; }
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
