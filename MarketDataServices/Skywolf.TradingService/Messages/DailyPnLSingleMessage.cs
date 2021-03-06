﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skywolf.TradingService.Messages
{
    public class PnLSingleMessage
    {
        public int ReqId { get; private set; }
        public int Pos { get; private set; }
        public double DailyPnL { get; private set; }
        public double Value { get; private set; }
        public double UnrealizedPnL { get; private set; }

        public PnLSingleMessage(int reqId, int pos, double dailyPnL, double unrealizedPnL, double value)
        {
            ReqId = reqId;
            Pos = pos;
            DailyPnL = dailyPnL;
            Value = value;
            UnrealizedPnL = unrealizedPnL;
        }
    }
}
