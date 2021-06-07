using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace EBSProject.Shared
{
    [ProtoContract]
    public class Subscription
    {
        [ProtoMember(1)]
        public string SubscriberTopic { get; set; }
        [ProtoMember(2)]
        public List<Condition> Conditions { get; set; }
        [ProtoMember(3)]
        public bool IsComplex { get; set; }

        public override string ToString()
        {
            string result = $"Subscription : IsComplex : {IsComplex}, ";
            if (Conditions != null)
            {
                foreach (Condition cond in Conditions)
                {
                    result += cond.ToString();
                }
            }
            return result;
        }
    }


}
