using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EBSProject.Models;
using EBSProject.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
namespace EBSPProject.Brokers
{
    class Program
    {
        public static string busConnectionString =
            "Endpoint=sb://ebsproject.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=1cZM+5SoudZM2FrHZyBftHCL2jVkzyhMr8ltakwRDRY=";
        static async Task Main(string[] args)
        {
            await Initializer.SetupBrokers(Configuration.BrokersNumber);
            List<Broker> brokers = new List<Broker>();
            Random random = new Random();
            for (int brokerIndex = 1; brokerIndex <= Configuration.BrokersNumber; brokerIndex++)
            {
                List<int> neighborsBrokerIndex = new List<int>();
                if (brokerIndex != Configuration.BrokersNumber)
                {
                    neighborsBrokerIndex.Add(brokerIndex+1);
                }
                neighborsBrokerIndex.Remove(brokerIndex);
                brokers.Add(new Broker(brokerIndex, neighborsBrokerIndex));
            }

            foreach (var broker in brokers)
            {
                broker.StartAsync(new CancellationToken());
            }

            Console.ReadLine();
        }
    }
}
