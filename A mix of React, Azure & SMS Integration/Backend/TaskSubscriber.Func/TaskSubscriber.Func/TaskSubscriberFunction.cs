using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskSubscriber.Func.Services;

namespace TaskSubscriber.Func
{
    public class TaskSubscriberFunction
    {
        private readonly IMessagingService _messagingService;

        public TaskSubscriberFunction(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        [FunctionName("todoItemSubscriber")]
        public async Task Run([ServiceBusTrigger("task", "tasksub", Connection = "busConnectionString")]
                               string serviceBusMessage, ILogger logger)
        {
            logger.LogInformation($"Message received. Going to send SMS");

            try
            { 
                await _messagingService.SendSmsAsync(serviceBusMessage);

                logger.LogInformation($"Successfully sent SMS with text: {serviceBusMessage}. DONT LOG SENSITIVE DATA!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                // message should be deadlettered or abandoned in real world depending on scenario
            }
        } 
    }
}
