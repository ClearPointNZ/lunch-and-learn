using Microsoft.Extensions.Options;
using SMSGlobal.api;
using System;
using System.Net;
using System.Threading.Tasks;
using TaskSubscriber.Func.Configuration;

namespace TaskSubscriber.Func.Services
{
    // https://mxt.smsglobal.com/
    // https://github.com/smsglobal/smsglobal-dotnet#sending-a-message

    public class MessagingService : IMessagingService
    {
        private readonly ApiSettings _settings;
        private readonly Client _client;

        public MessagingService(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
            _client = new Client(new Credentials(_settings.SmsKey, _settings.SmSecret));
        }

        public async Task SendSmsAsync(string message)
        {
            var response = await _client.SMS.SMSSend(new
            {
                origin = "API",
                destination = "+64xxxxxxx",
                message = message
            });

            if (response.statuscode == (int)HttpStatusCode.OK)
            {
                // do stuff
            }
            else
            {
                throw new Exception("SMS failed to send");
            }
        } 
    }
}
