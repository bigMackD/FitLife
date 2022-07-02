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
        private readonly IAsyncCommandHandler<ProcessWeeklyDietCommand, ProcessWeeklyDietResponse> _proccessWeeklyDietHandler;

        public ProcessorController(IAsyncCommandHandler<ProcessWeeklyDietCommand, ProcessWeeklyDietResponse> proccessWeeklyDietHandler)
        {
            _proccessWeeklyDietHandler = proccessWeeklyDietHandler;
        }

        /// <summary>
        /// Publishes event for weekly diet calculation
        /// </summary>
        /// <response code="200">Published successfully</response>
        [HttpGet]
        [Authorize]
        [Route("")]
        public Task<ProcessWeeklyDietResponse> ProcessWeeklyDiet()
        {
            var command = new ProcessWeeklyDietCommand
            {
                UserId = User.Claims.First(c => c.Type == "UserID").Value
        };
            return _proccessWeeklyDietHandler.Handle(command);
        }
    }
}
