using System;
using System.Collections.Generic;
using System.Text;
using EBSProject.Models;
using EBSProject.Shared;

namespace EBSProject.Subscribers
{
    public class SubscriptionGenerator
    {
        public static List<Subscription> GenerateSubscriptions(int numberOfSub)
        {
            List<Subscription> subscriptions = new List<Subscription>();
            Condition c = new Condition()
            {
                Field = "City",
                Value = "Iasi",
                Operator = "="
            };

            Condition c2 = new Condition()
            {
                Field = "Direction",
                Value = "S",
                Operator = "="
            };
            //Condition c3 = new Condition()
            //{
            //    Field = "abc",
            //    Value = "5",
            //    Operator = "="
            //};
            Subscription s = new Subscription()
            {
                Conditions = new List<Condition>()
            };
            s.Conditions.Add(c);
            s.Conditions.Add(c2);
            //s.Conditions.Add(c2);
            //s.Conditions.Add(c3);
            subscriptions.Add(s);
            return subscriptions;

        }
    }
}
