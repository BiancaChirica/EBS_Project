using System;
using System.Collections.Generic;
using System.Text;

namespace EBSProject.Shared
{
    public static class Configuration
    {
        public static string ServiceBusConnectionString =
            "Endpoint=sb://ebsproject.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=1cZM+5SoudZM2FrHZyBftHCL2jVkzyhMr8ltakwRDRY=";

        public static int BrokersNumber=3;
        public static int PublicationWindowSize=10;
        public static int SubscribersNumber = 1;
        public static int SubscriptionsNumber = 10;
        public static int PublicationWindowTimeOut = 10;
        public static int PublicationsNumber = 1000;
        public static int PublisherSleep = -1;
        public static int ReceivedPublicationsCleanupPeriod = 5;
    }
}
