using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Processor;
using FitLife.Contracts.Response.Processor;
using FitLife.Infrastructure.Events;
using FitLife.Shared.Infrastructure.CommandHandler;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FitLife.Infrastructure.CommandHandlers.Processor
{
    public class ProcessWeeklyDietCommandHandler : IAsyncCommandHandler<ProcessWeeklyDietCommand, ProcessWeeklyDietResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProcessWeeklyDietCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;


        public ProcessWeeklyDietCommandHandler(IConfiguration configuration, 
            ILogger<ProcessWeeklyDietCommandHandler> logger,
            IPublishEndpoint publishEndpoint)
        {
            _configuration = configuration;
            _logger = logger;
            _publishEndpoint = publishEndpoint;

        }

        public async Task<ProcessWeeklyDietResponse> Handle(ProcessWeeklyDietCommand command)
        {
            try
            {
                var message = new ProcessWeeklyDietEvent()
                {
                    UserId = command.UserId
                };
                await _publishEndpoint.Publish<ProcessWeeklyDietEvent>(message);

                return new ProcessWeeklyDietResponse
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new ProcessWeeklyDietResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
