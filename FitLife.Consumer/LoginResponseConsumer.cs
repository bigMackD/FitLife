using FitLife.Contracts.Response.Authentication;
using MassTransit;

namespace FitLife.Consumer
{
    internal class LoginResponseConsumer : IConsumer<LoginUserResponse>
    {
        public async Task Consume(ConsumeContext<LoginUserResponse> context)
        {
            await Console.Out.WriteLineAsync($"Notification sent: login id {context.Message.Success}");
        }
    }
}
