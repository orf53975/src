﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Skywolf.Contracts.DataContracts.Trading
{
    [DataContract(Namespace = Constants.NAMESPACE)]
    [KnownType(typeof(PositionPortfolio))]
    public class Position
    {
        [DataMember]
        public string Fund { get; set; }

        [DataMember]
        public string Strategy { get; set; }

        [DataMember]
        public string Folder { get; set; }

        [DataMember]
        public Contract Contract { get; set; }

        [DataMember]
        public string Account { get; set; }

        [DataMember]
        public double Quantity { get; set; }

        [DataMember]
        public double AverageCost { get; set; }
    }
}
