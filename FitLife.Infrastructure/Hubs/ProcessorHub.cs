using System.Collections.Generic;
using System.Threading.Tasks;
using FitLife.Shared.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FitLife.Infrastructure.Hubs
{
    public class ProcessorHub : Hub<IProcessorHubClient>
    {
        public async Task Notify(List<string> message)
        {
            await Clients.All.Notify(message);
        }
    }
}
