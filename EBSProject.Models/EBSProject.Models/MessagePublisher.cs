using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EBSProject.Models;
using Microsoft.Azure.ServiceBus;
using ProtoBuf;

namespace EBSProject.Shared
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly ITopicClient _topicClient;

        public MessagePublisher(string azureBusConnectionString, string topicName)
        {
            _topicClient = new TopicClient(azureBusConnectionString, topicName);
        }


        public Task Publish(Publication publication)
        {
            var message = new Message(ProtoSerialize<Publication>(publication));
            return _topicClient.SendAsync(message);
        }

        public Task PublishSubscription(Subscription subscription)
        {
            var message = new Message(ProtoSerialize<Subscription>(subscription));
            return _topicClient.SendAsync(message);
        }

        public static byte[] ProtoSerialize<T>(T obj) where T : class
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream,obj);
            return stream.ToArray();
        }
    }
}
