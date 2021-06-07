using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EBSProject.Shared;

namespace EBSProject.Subscribers
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Initializer.SetupSubscribers(Configuration.SubscribersNumber);
            List<Subscriber> subscribers = new List<Subscriber>();
            Random random = new Random();
            for (int subscriberIndex = 1; subscriberIndex <= Configuration.SubscribersNumber; subscriberIndex++)
            {
                var brokerIndex = random.Next(1, Configuration.BrokersNumber);
                var brokerTopic= "BrokerTopicSubscriptions" + brokerIndex;
                subscribers.Add(new Subscriber(subscriberIndex,brokerTopic));
                Console.WriteLine($"Broker {brokerIndex} chosen for subscriber {subscriberIndex}");
            }

            foreach (var subscriber in subscribers)
            {
                subscriber.StartAsync(new CancellationToken());
            }

            Console.ReadLine();
        }
    }
}
