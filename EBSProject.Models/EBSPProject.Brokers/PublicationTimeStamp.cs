using System;
using System.Collections.Generic;
using System.Text;
using EBSProject.Models;

namespace EBSPProject.Brokers
{
    public class PublicationTimeStamp
    {
        public Publication Publication { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
