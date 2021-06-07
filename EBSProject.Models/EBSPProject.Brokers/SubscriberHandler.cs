using System;
using System.Collections.Generic;
using System.Text;
using EBSProject.Models;
using EBSProject.Shared;

namespace EBSPProject.Brokers
{
    public class SubscriberHandler
    {
        public Subscription Subscription;
        public IMessagePublisher MessagePublisher;

        public SubscriberHandler(Subscription subscription)
        {
            Subscription = subscription;
            MessagePublisher =
                new MessagePublisher(Configuration.ServiceBusConnectionString, subscription.SubscriberTopic);
        }
    }
}
