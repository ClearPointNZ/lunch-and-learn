using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TodoList.Api.Configuration;

namespace TodoList.Api.Services
{
    public interface IMessagingService
    {
        Task SendServiceBusMessageAsync(string message);
    }

    public class MessagingService : IMessagingService
    {
        private readonly ApiSettings _settings;
        private readonly ServiceBusClient _serviceBusClient; 
        private readonly ServiceBusSender _serviceBusSender;  

        public MessagingService(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;

            _serviceBusClient = new ServiceBusClient(_settings.BusConnectionString);
            _serviceBusSender = _serviceBusClient.CreateSender(_settings.TopicName);
        } 

        public async Task SendServiceBusMessageAsync(string message)
        { 
            // create a batch 
            using ServiceBusMessageBatch messageBatch = await _serviceBusSender.CreateMessageBatchAsync();

            try
            {
                // try adding a message to the batch
                messageBatch.TryAddMessage(new ServiceBusMessage(message));

                // Use the producer client to send the batch of messages to the Service Bus topic
                await _serviceBusSender.SendMessagesAsync(messageBatch);
            }
            catch (Exception)
            {
                throw new Exception("Service bus message failed to send");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await _serviceBusSender.DisposeAsync();
                await _serviceBusClient.DisposeAsync();
            } 
        } 
    }
}
