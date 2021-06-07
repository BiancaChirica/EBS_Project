using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EBSProject.Models;

namespace EBSProject.Shared
{
    public interface IMessagePublisher
    {
        public Task Publish(Publication obj);
        public Task PublishSubscription(Subscription obj);
    }
}
