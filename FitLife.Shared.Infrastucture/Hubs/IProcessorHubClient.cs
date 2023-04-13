using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitLife.Shared.Infrastructure.Hubs
{
    public interface IProcessorHubClient
    {
        Task Notify(List<string> message);
    }
}
