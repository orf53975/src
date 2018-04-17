﻿/* Copyright (C) 2013 Interactive Brokers LLC. All rights reserved.  This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skywolf.TradingService.Messages
{
    public class MktDepthExchangesMessage
    {
        public IBApi.DepthMktDataDescription[] Descriptions { get; private set; }

        public MktDepthExchangesMessage(IBApi.DepthMktDataDescription[] descriptions)
        {
            this.Descriptions = descriptions;
        }
    }
}
