using EBSProject.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EBSProject.Models;
using Microsoft.Azure.ServiceBus;
using ProtoBuf;

namespace EBSProject.Subscribers
{
    public class Subscriber : BackgroundService
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly ISubscriptionClient _subscriptionsClient;
        private readonly int _simpleSubscriptionsCount;
        private readonly int _complexSubscriptionsCount;
        private string _topic;
        public Subscriber(int subscriberIndex,string brokerTopic)
        {
            _messagePublisher = new MessagePublisher(Configuration.ServiceBusConnectionString, brokerTopic);
            _topic = "SubscriberTopic" + subscriberIndex;
            var subscription = "SubscriberSubscription" + subscriberIndex;
            _subscriptionsClient =
                new SubscriptionClient(Configuration.ServiceBusConnectionString, _topic, subscription);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<Subscription> subscriptions =
                SubscriptionGenerator.GenerateSubscriptions(Configuration.SubscriptionsNumber);
            foreach (var subscription in subscriptions)
            {
                subscription.SubscriberTopic = _topic;
                _messagePublisher.PublishSubscription(subscription);
            }

            _subscriptionsClient.RegisterMessageHandler((message, token) =>
            {
                var publication = Serializer.Deserialize<Publication>(new ReadOnlyMemory<byte>(message.Body));
                Console.WriteLine(publication.ToString());
                return Task.CompletedTask;
            }, new MessageHandlerOptions(args => Task.CompletedTask));
            return Task.CompletedTask;
        }
    }
}
