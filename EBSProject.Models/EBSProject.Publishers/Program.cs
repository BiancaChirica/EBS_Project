using System;
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
            int brokerIndex = random.Next(1, Configuration.BrokersNumber);
            Console.WriteLine($"Broker {1} chosen");
            var brokerTopic = "BrokerTopicPublications" + 1;
            Publisher publisher = new Publisher(brokerTopic);
            publisher.StartAsync(new CancellationToken());
            Console.ReadLine();
        }
    }
}
