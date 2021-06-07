using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EBSProject.Models;
using EBSProject.Shared;
using ProtoBuf;
using publisher;

namespace EBSProject.Publishers
{
    public class Publisher : BackgroundService
    {
        private readonly IMessagePublisher _messagePublisher;
        public Publisher(string brokerTopic)
        {
            _messagePublisher = new MessagePublisher(Configuration.ServiceBusConnectionString, brokerTopic);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<Publication> publications = new List<Publication>();
            if (Configuration.PublisherSleep == -1)
            {
                publications = PublicationsGenerator.GeneratePublications(Configuration.PublicationsNumber);
                foreach (var publication in publications)
                {
                    _messagePublisher.Publish(publication);
                }
            }
            else
            {
                while (true)
                {
                    publications = PublicationsGenerator.GeneratePublications(Configuration.PublicationsNumber);
                    foreach (var publication in publications)
                    {
                        _messagePublisher.Publish(publication);
                    }
                    Thread.Sleep(Configuration.PublisherSleep);
                }
            }
            return Task.CompletedTask;
        }
    }
}
