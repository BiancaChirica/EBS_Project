using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace EBSProject.Shared
{
    [ProtoContract]
    public class Condition
    {
        [ProtoMember(1)]
        public string Field { get; set; }
        [ProtoMember(2)]
        public string Operator { get; set; }
        [ProtoMember(3)]
        public string Value { get; set; }

    }
}
