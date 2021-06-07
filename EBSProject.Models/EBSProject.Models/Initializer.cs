using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EBSProject.Shared;

namespace EBSProject.Shared
{
    public static class Initializer
    {
        public static async Task SetupBrokers(int brokerNumber)
        {
            ServiceBusUtils utils = new ServiceBusUtils();
            for (int brokerIndex = 1; brokerIndex <= brokerNumber; brokerIndex++)
            {
                var topicPubName = "BrokerTopicPublications" + brokerIndex;
                var topicSubName = "BrokerTopicSubscriptions" + brokerIndex;
                var subSub = "BrokerSubscriptionSubscriptions" + brokerIndex;
                var pubSub = "BrokerSubscriptionPublications" + brokerIndex;
                await utils.CreateTopic(topicPubName);
                await utils.CreateTopic(topicSubName);
                await utils.CreateSubscription(topicPubName, pubSub);
                await utils.CreateSubscription(topicSubName, subSub);
                Console.WriteLine($"Setup for broker {brokerIndex} done");
                
            }

        }
        public static async Task SetupSubscribers(int subscribersNumber)
        {
            ServiceBusUtils utils = new ServiceBusUtils();
            for (int subscriberIndex = 1; subscriberIndex <= subscribersNumber; subscriberIndex++)
            {
                var topicPubName = "SubscriberTopic" + subscriberIndex;
                var topicSubName = "SubscriberSubscription" + subscriberIndex;
                await utils.CreateTopic(topicPubName);
                await utils.CreateSubscription(topicPubName, topicSubName);
                Console.WriteLine($"Setup for subscriber {subscriberIndex} done");

            }

        }
    }
}
