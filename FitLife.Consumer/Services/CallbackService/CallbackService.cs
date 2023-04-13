using Microsoft.Extensions.Configuration;

namespace FitLife.Consumer.Services.CallbackService
{
    public class CallbackService : ICallbackService
    {
        private readonly IConfiguration _configuration;

        public CallbackService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Callback(Guid processId)
        {
            HttpClient client = new HttpClient();
            var callbackUrl = _configuration.GetValue<string>("AppSettings:CallbackUrl");
            HttpResponseMessage response = await client.GetAsync($"{callbackUrl}/{processId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
