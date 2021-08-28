using System;
using System.Collections.Generic;
using System.Text;

namespace BELTrader.Domain.Models
{
    public enum MajorIndexType
    {
        DowJones,
        Nasdaq,
        SP500
    }

    public class MajorIndex
    {
        public string name { get; set; }
        public double price { get; set; }
        public double change { get; set; }
        public MajorIndexType Type { get; set; }
    }
}
