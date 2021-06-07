using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus.Administration;
using EBSProject.Models;
using EBSProject.Shared;

namespace EBSProject.Shared
{
    public class ServiceBusUtils
    {
        private readonly ServiceBusAdministrationClient _administrationClient;

        public ServiceBusUtils()
        {
            _administrationClient = new ServiceBusAdministrationClient(Configuration.ServiceBusConnectionString);
        }

        public async Task CreateTopic(string topicName)
        {
            bool topicExists = await _administrationClient.TopicExistsAsync(topicName);
            if (!topicExists)
            {
                var options = new CreateTopicOptions(topicName)
                {
                    MaxSizeInMegabytes = 1024
                };
                var response=await _administrationClient.CreateTopicAsync(options);
            }
        }
        public async Task CreateSubscription(string topicName,string subscriptionName)
        {
            bool subscriptionExists = await _administrationClient.SubscriptionExistsAsync(topicName, subscriptionName);
            if (!subscriptionExists)
            {
                var options = new CreateSubscriptionOptions(topicName, subscriptionName)
                {
                    DefaultMessageTimeToLive = new TimeSpan(2, 0, 0, 0)
                };
                var response=await _administrationClient.CreateSubscriptionAsync(options);
            }
        }
    }
}
