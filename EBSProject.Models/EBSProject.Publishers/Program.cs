using System;
using System.Collections.Generic;
using System.Threading;
using EBSProject.Models;
using EBSProject.Shared;

namespace EBSProject.Publishers
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<Publisher> publishers = new List<Publisher>();

            for (int publisherIndex = 1; publisherIndex <= Configuration.PublisherNumber; publisherIndex++)
            {
                int brokerIndex = random.Next(1, Configuration.BrokersNumber);
             
                var brokerTopic = "BrokerTopicPublications" + brokerIndex;
                publishers.Add(new Publisher(publisherIndex, brokerTopic));
                Console.WriteLine($"Broker {brokerIndex} chosen for publisher {publisherIndex}");
            }

            foreach (var publisher in publishers)
            {
               publisher.StartAsync(new CancellationToken());
            }
      
            Console.ReadLine();
        }
    }
}
