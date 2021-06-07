using System;
using ProtoBuf;

namespace EBSProject.Models
{
    [ProtoContract]
    public class Publication
    {
        [ProtoMember(1)]
        public int StationId { get; set; }
        [ProtoMember(2)]
        public string City { get; set; }
        [ProtoMember(3)]
        public int Temp { get; set; }
        [ProtoMember(4)]
        public double Rain { get; set; }
        [ProtoMember(5)]
        public int Wind { get; set; }
        [ProtoMember(6)]
        public string Direction { get; set; }
        [ProtoMember(7)]
        public DateTime Date { get; set; }
        [ProtoMember(8)]
        public string PublicationId { get; set; }

        public override string ToString()
        {
            return "Publication{" +
                   "stationId=" + StationId +
                   ", city='" + City + '\'' +
                   ", temp=" + Temp +
                   ", rain=" + Rain +
                   ", wind=" + Wind +
                   ", direction='" + Direction + '\'' +
                   ", date=" + Date +
                   '}';
        }
    }
}
