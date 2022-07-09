using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Processor
{
    public class ProcessPeriodicDietResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
