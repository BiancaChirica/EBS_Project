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
        public Publisher(int index, string brokerTopic)
        {
            _messagePublisher = new MessagePublisher(Configuration.ServiceBusConnectionString, brokerTopic);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<Publication> publications = new List<Publication>();
            int totalNumberOfPublications = 0;
            var startTime = DateTime.UtcNow;
       //     while (DateTime.UtcNow - startTime < TimeSpan.FromMinutes(3))
           // {
            if (Configuration.PublisherSleep == -1)
            {
                publications = PublicationsGenerator.GeneratePublications(Configuration.PublicationsNumber);

               
                foreach (var publication in publications)
                {
                    _messagePublisher.Publish(publication);
                        totalNumberOfPublications++;
                    Console.WriteLine(publication.PublicationId + " " + publication.ToString());
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
                            totalNumberOfPublications++;
                            //    Console.WriteLine(publication.PublicationId + " "+ publication.ToString());
                        }
                    Thread.Sleep(Configuration.PublisherSleep);
                }
            }
            // dupa fiecare 10000 de publicatii, se face un sleep de 100ms
                Thread.Sleep(100);
           // }
            Console.WriteLine("Total number of messages :" + totalNumberOfPublications);
            return Task.CompletedTask;
        }
    }
}
