using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Processor;
using FitLife.Contracts.Response.Processor;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers
{
    /// <summary>
    /// Controller for sending calculation requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorController : ControllerBase
    {
        private readonly IAsyncCommandHandler<ProcessPeriodicDietCommand, ProcessPeriodicDietResponse> _proccessWeeklyDietHandler;

        public ProcessorController(IAsyncCommandHandler<ProcessPeriodicDietCommand, ProcessPeriodicDietResponse> proccessWeeklyDietHandler)
        {
            _proccessWeeklyDietHandler = proccessWeeklyDietHandler;
        }

        /// <summary>
        /// Publishes event for weekly diet calculation
        /// </summary>
        /// <response code="200">Published successfully</response>
        [HttpPost]
        [Authorize]
        [Route("periodicDiet")]
        public Task<ProcessPeriodicDietResponse> ProcessPeriodicDiet()
        {
            var command = new ProcessPeriodicDietCommand
            {
                UserId = User.Claims.First(c => c.Type == "UserID").Value
        };
            return _proccessWeeklyDietHandler.Handle(command);
        }
    }
}
