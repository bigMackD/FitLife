using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Events;
using FitLife.Contracts.Request.Command.Processor;
using FitLife.Contracts.Response;
using FitLife.Contracts.Response.Processor;
using FitLife.Infrastructure.Hubs;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

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
        private IHubContext<ProcessorHub, IProcessorHubClient> _processorHub;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorController"/> class.
        /// </summary>
        /// <param name="processWeeklyDietHandler">Instance of handler</param>
        public ProcessorController(IAsyncCommandHandler<ProcessPeriodicDietCommand,
            ProcessPeriodicDietResponse> processWeeklyDietHandler,
            IHubContext<ProcessorHub, IProcessorHubClient> processorHub)
        {
            _processWeeklyDietHandler = processWeeklyDietHandler;
            _processorHub = processorHub;
        }

        /// <summary>
        /// Publishes event for weekly diet calculation
        /// </summary>
        /// <response code="200">Published successfully</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpPost]
        [Authorize]
        [Route("periodicDiet/{id:guid}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProcessPeriodicDietResponse), StatusCodes.Status200OK)]
        public Task<ProcessPeriodicDietResponse> ProcessPeriodicDiet([FromRoute] Guid id)
        {
            var command = new ProcessPeriodicDietCommand
            {
                EventId = id,
                UserId = User.Claims.First(c => c.Type == "UserID").Value
            };
            return _processWeeklyDietHandler.Handle(command);
        }

        /// <summary>
        /// Callback endpoint for successfully computed event 
        /// </summary>
        /// <response code="200">Published successfully</response>
        /// <response code="409">Entity processed with errors</response>
        [HttpPost]
        [Route("callback")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Callback([FromBody] EventProcessed eventProcessed)
        {
            List<string> messages = new List<string> { eventProcessed.Id.ToString() };
            await _processorHub.Clients.All.Notify(messages);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
