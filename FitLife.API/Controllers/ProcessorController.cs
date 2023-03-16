using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Processor;
using FitLife.Contracts.Response;
using FitLife.Contracts.Response.Processor;
using FitLife.Shared.Infrastructure.CommandHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IAsyncCommandHandler<ProcessPeriodicDietCommand, ProcessPeriodicDietResponse> _processWeeklyDietHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorController"/> class.
        /// </summary>
        /// <param name="processWeeklyDietHandler">Instance of handler</param>
        public ProcessorController(IAsyncCommandHandler<ProcessPeriodicDietCommand, ProcessPeriodicDietResponse> processWeeklyDietHandler)
        {
            _processWeeklyDietHandler = processWeeklyDietHandler;
        }

        /// <summary>
        /// Publishes event for weekly diet calculation
        /// </summary>
        /// <response code="200">Published successfully</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpPost]
        [Authorize]
        [Route("periodicDiet")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProcessPeriodicDietResponse), StatusCodes.Status200OK)]
        public Task<ProcessPeriodicDietResponse> ProcessPeriodicDiet()
        {
            var command = new ProcessPeriodicDietCommand
            {
                UserId = User.Claims.First(c => c.Type == "UserID").Value
        };
            return _processWeeklyDietHandler.Handle(command);
        }
    }
}
