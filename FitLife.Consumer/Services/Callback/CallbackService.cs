using System.Net.Http.Headers;
using FitLife.Consumer.Shared.Infrastructure.Services.Callback;
using FitLife.Contracts.Events;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FitLife.Consumer.Services.Callback
{
    public class CallbackService : ICallbackService
    {
        private readonly IConfiguration _configuration;

        public CallbackService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Callback(EventProcessed result)
        {
            HttpClient client = new HttpClient();
            var callbackUrl = _configuration.GetValue<string>("AppSettings:CallbackUrl");
            var myContent = JsonConvert.SerializeObject(result);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync($"{callbackUrl}", byteContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
