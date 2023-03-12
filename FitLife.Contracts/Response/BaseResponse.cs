using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response
{
    public sealed class BaseResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
