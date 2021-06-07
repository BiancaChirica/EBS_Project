using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EBSProject.Models;
using Microsoft.Azure.ServiceBus;
using ProtoBuf;
using Azure.Messaging.ServiceBus.Administration;
using EBSProject.Shared;

namespace EBSPProject.Brokers
{
    public class Broker : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionsClient;
        private readonly ISubscriptionClient _publicationsClient;
        private readonly List<IMessagePublisher> _neighborsBrokers;
        private readonly List<SubscriberHandler> _subscribers;
        private readonly List<SubscriberHandler> _subscribersComplex;
        private readonly Matcher _matcher;
        private readonly Queue<PublicationTimeStamp> _currentWindow;
        private readonly int _brokerIndex;
        private readonly List<string> _receivedPublicationsId;
        private DateTime _lastCleanUpTime;
        public Broker(int brokerIndex,List<int> neighborsBrokerIndex)
        {
            var topicPubName = "BrokerTopicPublications" + brokerIndex;
            var topicSubName = "BrokerTopicSubscriptions" + brokerIndex;
            var subSub = "BrokerSubscriptionSubscriptions" + brokerIndex;
            var pubSub = "BrokerSubscriptionPublications" + brokerIndex;
            _brokerIndex = brokerIndex;
            _subscriptionsClient = new SubscriptionClient(Configuration.ServiceBusConnectionString, topicSubName, subSub);
            _publicationsClient = new SubscriptionClient(Configuration.ServiceBusConnectionString, topicPubName, pubSub);
            _neighborsBrokers = new List<IMessagePublisher>();
            foreach (var neighborsTopic in neighborsBrokerIndex)
            {
                var neighborsTopicPubName = "BrokerTopicPublications" + neighborsTopic;
                _neighborsBrokers.Add(new MessagePublisher(Configuration.ServiceBusConnectionString, neighborsTopicPubName));
            }

            _subscribers = new List<SubscriberHandler>();
            _subscribersComplex = new List<SubscriberHandler>();
            _matcher = new Matcher();
            _currentWindow = new Queue<PublicationTimeStamp>();
            _receivedPublicationsId = new List<string>();
            Console.WriteLine($"Broker {brokerIndex} initialized");
            _lastCleanUpTime=DateTime.Now;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionsClient.RegisterMessageHandler((message, token) =>
            {
                var subscription = Serializer.Deserialize<Subscription>(new ReadOnlyMemory<byte>(message.Body));
                if (!subscription.IsComplex)
                {
                    _subscribers.Add(new SubscriberHandler(subscription));
                }
                else
                {
                    _subscribersComplex.Add(new SubscriberHandler(subscription));
                }

                return Task.CompletedTask;
            }, new MessageHandlerOptions(args=>Task.CompletedTask));
            _publicationsClient.RegisterMessageHandler((message, token) =>
            {
                var publication = Serializer.Deserialize<Publication>(new ReadOnlyMemory<byte>(message.Body));
                if (_receivedPublicationsId.Contains(publication.PublicationId))
                {
                    if ((DateTime.Now - _lastCleanUpTime).TotalSeconds >
                        Configuration.ReceivedPublicationsCleanupPeriod)
                    {
                        _lastCleanUpTime=DateTime.Now;
                        _receivedPublicationsId.Clear();
                    }
                    return Task.CompletedTask;
                }
                Task.Run(() =>
                    {
                        foreach (var subscriber in _subscribers)
                        {
                            if (_matcher.Match(publication, subscriber.Subscription))
                            {
                                subscriber.MessagePublisher.Publish(publication);
                            }
                        }
                    }, token);
                Task.Run(() =>
                {
                    foreach (var neighbor in _neighborsBrokers)
                    {
                        neighbor.Publish(publication);
                    }
                }, token);
                Task.Run(() =>
                {
                    var now = DateTime.Now;
                    if (_currentWindow.Count==0 || (now - _currentWindow.Peek().TimeStamp).TotalSeconds > Configuration.PublicationWindowTimeOut)
                    {
                        if (_currentWindow.Count != 0)
                        {
                            _currentWindow.Clear();
                        }

                        _currentWindow.Enqueue(new PublicationTimeStamp()
                        {
                            Publication = publication,
                            TimeStamp = DateTime.Now
                        });
                    }
                    else
                    {
                        _currentWindow.Enqueue(new PublicationTimeStamp()
                        {
                            Publication = publication,
                            TimeStamp = DateTime.Now
                        });
                        if (_currentWindow.Count == Configuration.PublicationWindowSize)
                        {
                            var publications = _currentWindow.Select(pt => pt.Publication).ToList();
                            foreach (var subscriber in _subscribersComplex)
                            {
                                if (_matcher.MatchComplex(publications, subscriber.Subscription))
                                {
                                    subscriber.MessagePublisher.Publish(publication);
                                }
                            }
                            _currentWindow.Clear();
                        }
                    }
                }, token);

                return Task.CompletedTask;
            }, new MessageHandlerOptions(args => Task.CompletedTask));
            return Task.CompletedTask;
        }
    }
}
