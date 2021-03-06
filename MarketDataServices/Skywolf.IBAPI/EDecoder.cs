/* Copyright (C) 2017 Interactive Brokers LLC. All rights reserved.  This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace Skywolf.IBApi
{
    class EDecoder : IDecoder
    {
        private EClientMsgSink eClientMsgSink;
        private EWrapper eWrapper;
        private int serverVersion;
        private BinaryReader dataReader;
        private int nDecodedLen;

        public EDecoder(int serverVersion, EWrapper callback, EClientMsgSink sink = null)
        {
            this.serverVersion = serverVersion;
            this.eWrapper = callback;
            this.eClientMsgSink = sink;
        }

        public int ParseAndProcessMsg(byte[] buf)
        {
            if (dataReader != null)
                dataReader.Dispose();

            dataReader = new BinaryReader(new MemoryStream(buf));
            nDecodedLen = 0;

            if (serverVersion == 0)
            {
                ProcessConnectAck();

                return nDecodedLen;
            }

            return ProcessIncomingMessage(ReadInt()) ? nDecodedLen : -1;
        }

        private void ProcessConnectAck()
        {
            serverVersion = ReadInt();

            if (serverVersion == -1)
            {
                var srv = ReadString();

                serverVersion = 0;

                if (eClientMsgSink != null)
                    eClientMsgSink.redirect(srv);

                return;
            }

            var serverTime = "";

            if (serverVersion >= 20)
            {
                serverTime = ReadString();
            }

            if (eClientMsgSink != null)
                eClientMsgSink.serverVersion(serverVersion, serverTime);

            eWrapper.connectAck();
        }

        private bool ProcessIncomingMessage(int incomingMessage)
        {
            if (incomingMessage == IncomingMessage.NotValid)
                return false;

            switch (incomingMessage)
            {
                case IncomingMessage.TickPrice:
                    TickPriceEvent();
                    break;

                case IncomingMessage.TickSize:
                    TickSizeEvent();
                    break;

                case IncomingMessage.Tickstring:
                    TickStringEvent();
                    break;

                case IncomingMessage.TickGeneric:
                    TickGenericEvent();
                    break;

                case IncomingMessage.TickEFP:
                    TickEFPEvent();
                    break;

                case IncomingMessage.TickSnapshotEnd:
                    TickSnapshotEndEvent();
                    break;

                case IncomingMessage.Error:
                    ErrorEvent();
                    break;

                case IncomingMessage.CurrentTime:
                    CurrentTimeEvent();
                    break;

                case IncomingMessage.ManagedAccounts:
                    ManagedAccountsEvent();
                    break;

                case IncomingMessage.NextValidId:
                    NextValidIdEvent();
                    break;

                case IncomingMessage.DeltaNeutralValidation:
                    DeltaNeutralValidationEvent();
                    break;

                case IncomingMessage.TickOptionComputation:
                    TickOptionComputationEvent();
                    break;

                case IncomingMessage.AccountSummary:
                    AccountSummaryEvent();
                    break;

                case IncomingMessage.AccountSummaryEnd:
                    AccountSummaryEndEvent();
                    break;

                case IncomingMessage.AccountValue:
                    AccountValueEvent();
                    break;

                case IncomingMessage.PortfolioValue:
                    PortfolioValueEvent();
                    break;

                case IncomingMessage.AccountUpdateTime:
                    AccountUpdateTimeEvent();
                    break;

                case IncomingMessage.AccountDownloadEnd:
                    AccountDownloadEndEvent();
                    break;

                case IncomingMessage.OrderStatus:
                    OrderStatusEvent();
                    break;

                case IncomingMessage.OpenOrder:
                    OpenOrderEvent();
                    break;

                case IncomingMessage.OpenOrderEnd:
                    OpenOrderEndEvent();
                    break;

                case IncomingMessage.ContractData:
                    ContractDataEvent();
                    break;

                case IncomingMessage.ContractDataEnd:
                    ContractDataEndEvent();
                    break;

                case IncomingMessage.ExecutionData:
                    ExecutionDataEvent();
                    break;

                case IncomingMessage.ExecutionDataEnd:
                    ExecutionDataEndEvent();
                    break;

                case IncomingMessage.CommissionsReport:
                    CommissionReportEvent();
                    break;

                case IncomingMessage.FundamentalData:
                    FundamentalDataEvent();
                    break;

                case IncomingMessage.HistoricalData:
                    HistoricalDataEvent();
                    break;

                case IncomingMessage.MarketDataType:
                    MarketDataTypeEvent();
                    break;

                case IncomingMessage.MarketDepth:
                    MarketDepthEvent();
                    break;

                case IncomingMessage.MarketDepthL2:
                    MarketDepthL2Event();
                    break;

                case IncomingMessage.NewsBulletins:
                    NewsBulletinsEvent();
                    break;

                case IncomingMessage.Position:
                    PositionEvent();
                    break;

                case IncomingMessage.PositionEnd:
                    PositionEndEvent();
                    break;

                case IncomingMessage.RealTimeBars:
                    RealTimeBarsEvent();
                    break;

                case IncomingMessage.ScannerParameters:
                    ScannerParametersEvent();
                    break;

                case IncomingMessage.ScannerData:
                    ScannerDataEvent();
                    break;

                case IncomingMessage.ReceiveFA:
                    ReceiveFAEvent();
                    break;

                case IncomingMessage.BondContractData:
                    BondContractDetailsEvent();
                    break;

                case IncomingMessage.VerifyMessageApi:
                    VerifyMessageApiEvent();
                    break;

                case IncomingMessage.VerifyCompleted:
                    VerifyCompletedEvent();
                    break;

                case IncomingMessage.DisplayGroupList:
                    DisplayGroupListEvent();
                    break;

                case IncomingMessage.DisplayGroupUpdated:
                    DisplayGroupUpdatedEvent();
                    break;

                case IncomingMessage.VerifyAndAuthMessageApi:
                    VerifyAndAuthMessageApiEvent();
                    break;

                case IncomingMessage.VerifyAndAuthCompleted:
                    VerifyAndAuthCompletedEvent();
                    break;

                case IncomingMessage.PositionMulti:
                    PositionMultiEvent();
                    break;

                case IncomingMessage.PositionMultiEnd:
                    PositionMultiEndEvent();
                    break;

                case IncomingMessage.AccountUpdateMulti:
                    AccountUpdateMultiEvent();
                    break;

                case IncomingMessage.AccountUpdateMultiEnd:
                    AccountUpdateMultiEndEvent();
                    break;

                case IncomingMessage.SecurityDefinitionOptionParameter:
                    SecurityDefinitionOptionParameterEvent();
                    break;

                case IncomingMessage.SecurityDefinitionOptionParameterEnd:
                    SecurityDefinitionOptionParameterEndEvent();
                    break;

                case IncomingMessage.SoftDollarTier:
                    SoftDollarTierEvent();
                    break;

                case IncomingMessage.FamilyCodes:
                    FamilyCodesEvent();
                    break;

                case IncomingMessage.SymbolSamples:
                    SymbolSamplesEvent();
                    break;

                case IncomingMessage.MktDepthExchanges:
                    MktDepthExchangesEvent();
                    break;

                case IncomingMessage.TickNews:
                    TickNewsEvent();
                    break;

                case IncomingMessage.TickReqParams:
                    TickReqParamsEvent();
                    break;

                case IncomingMessage.SmartComponents:
                    SmartComponentsEvent();
                    break;

                case IncomingMessage.NewsProviders:
                    NewsProvidersEvent();
                    break;

                case IncomingMessage.NewsArticle:
                    NewsArticleEvent();
                    break;

                case IncomingMessage.HistoricalNews:
                    HistoricalNewsEvent();
                    break;

                case IncomingMessage.HistoricalNewsEnd:
                    HistoricalNewsEndEvent();
                    break;

                case IncomingMessage.HeadTimestamp:
                    HeadTimestampEvent();
                    break;

                case IncomingMessage.HistogramData:
                    HistogramDataEvent();
                    break;

                case IncomingMessage.HistoricalDataUpdate:
                    HistoricalDataUpdateEvent();
                    break;

                case IncomingMessage.RerouteMktDataReq:
                    RerouteMktDataReqEvent();
                    break;

                case IncomingMessage.RerouteMktDepthReq:
                    RerouteMktDepthReqEvent();
                    break;

                case IncomingMessage.MarketRule:
                    MarketRuleEvent();
                    break;

                case IncomingMessage.PnL:
                    PnLEvent();
                    break;

                case IncomingMessage.PnLSingle:
                    PnLSingleEvent();
                    break;

                default:
                    eWrapper.error(IncomingMessage.NotValid, EClientErrors.UNKNOWN_ID.Code, EClientErrors.UNKNOWN_ID.Message);
                    return false;
            }

            return true;
        }

        private void MarketRuleEvent()
        {
            int marketRuleId = ReadInt();
            PriceIncrement[] priceIncrements = new PriceIncrement[0];
            int nPriceIncrements = ReadInt();

            if (nPriceIncrements > 0)
            {
                Array.Resize(ref priceIncrements, nPriceIncrements);

                for (int i = 0; i < nPriceIncrements; ++i)
                {
                    priceIncrements[i] = new PriceIncrement(ReadDouble(), ReadDouble());
                }
            }

            eWrapper.marketRule(marketRuleId, priceIncrements);
        }

        private void RerouteMktDepthReqEvent()
        {
            var reqId = ReadInt();
            var conId = ReadInt();
            string exchange = ReadString();

            eWrapper.rerouteMktDepthReq(reqId, conId, exchange);
        }

        private void RerouteMktDataReqEvent()
        {
            var reqId = ReadInt();
            var conId = ReadInt();
            string exchange = ReadString();

            eWrapper.rerouteMktDataReq(reqId, conId, exchange);
        }

        private void HistoricalDataUpdateEvent()
        {
            int requestId = ReadInt();
            int barCount = ReadInt();
            string date = ReadString();
            double open = ReadDouble();
            double close = ReadDouble();
            double high = ReadDouble();
            double low = ReadDouble();
            double WAP = ReadDouble();
            long volume = ReadLong();

            eWrapper.historicalDataUpdate(requestId, new Bar(date, open, high, low,
                                    close, volume, barCount, WAP));
        }


        private void PnLSingleEvent()
        {
            int reqId = ReadInt();
            int pos = ReadInt();
            double dailyPnL = ReadDouble();
            double unrealizedPnL = double.MaxValue;

            if (serverVersion >= MinServerVer.UNREALIZED_PNL)
            {
                unrealizedPnL = ReadDouble();
            }

            double value = ReadDouble();

            eWrapper.pnlSingle(reqId, pos, dailyPnL, unrealizedPnL, value);
        }

        private void PnLEvent()
        {
            int reqId = ReadInt();
            double dailyPnL = ReadDouble();
            double unrealizedPnL = double.MaxValue;

            if (serverVersion >= MinServerVer.UNREALIZED_PNL)
            {
                unrealizedPnL = ReadDouble();
            }

            eWrapper.pnl(reqId, dailyPnL, unrealizedPnL);
        }

        private void HistogramDataEvent()
        {
            var reqId = ReadInt();
            var n = ReadInt();
            var data = new HistogramEntry[n];

            for (int i = 0; i < n; i++)
            {
                data[i].Price = ReadDouble();
                data[i].Size = ReadLong();
            }

            eWrapper.histogramData(reqId, data);
        }

        private void HeadTimestampEvent()
        {
            int reqId = ReadInt();
            string headTimestamp = ReadString();

            eWrapper.headTimestamp(reqId, headTimestamp);
        }

        private void HistoricalNewsEvent()
        {
            int requestId = ReadInt();
            string time = ReadString();
            string providerCode = ReadString();
            string articleId = ReadString();
            string headline = ReadString();

            eWrapper.historicalNews(requestId, time, providerCode, articleId, headline);
        }

        private void HistoricalNewsEndEvent()
        {
            int requestId = ReadInt();
            bool hasMore = ReadBoolFromInt();

            eWrapper.historicalNewsEnd(requestId, hasMore);
        }

        private void NewsArticleEvent()
        {
            int requestId = ReadInt();
            int articleType = ReadInt();
            string articleText = ReadString();

            eWrapper.newsArticle(requestId, articleType, articleText);
        }

        private void NewsProvidersEvent()
        {
            NewsProvider[] newsProviders = new NewsProvider[0];
            int nNewsProviders = ReadInt();

            if (nNewsProviders > 0)
            {
                Array.Resize(ref newsProviders, nNewsProviders);

                for (int i = 0; i < nNewsProviders; ++i)
                {
                    newsProviders[i] = new NewsProvider(ReadString(), ReadString());
                }
            }

            eWrapper.newsProviders(newsProviders);
        }

        private void SmartComponentsEvent()
        {
            int reqId = ReadInt();
            int n = ReadInt();
            var theMap = new Dictionary<int, KeyValuePair<string, char>>();

            for (int i = 0; i < n; i++)
            {
                int bitNumber = ReadInt();
                String exchange = ReadString();
                char exchangeLetter = ReadChar();

                theMap.Add(bitNumber, new KeyValuePair<string, char>(exchange, exchangeLetter));
            }

            eWrapper.smartComponents(reqId, theMap);
        }

        private void TickReqParamsEvent()
        {
            int tickerId = ReadInt();
            double minTick = ReadDouble();
            String bboExchange = ReadString();
            int snapshotPermissions = ReadInt();

            eWrapper.tickReqParams(tickerId, minTick, bboExchange, snapshotPermissions);
        }

        private void TickNewsEvent()
        {
            int tickerId = ReadInt();
            long timeStamp = ReadLong();
            string providerCode = ReadString();
            string articleId = ReadString();
            string headline = ReadString();
            string extraData = ReadString();

            eWrapper.tickNews(tickerId, timeStamp, providerCode, articleId, headline, extraData);
        }

        private void SymbolSamplesEvent()
        {
            int reqId = ReadInt();
            ContractDescription[] contractDescriptions = new ContractDescription[0];
            int nContractDescriptions = ReadInt();

            if (nContractDescriptions > 0)
            {
                Array.Resize(ref contractDescriptions, nContractDescriptions);

                for (int i = 0; i < nContractDescriptions; ++i)
                {
                    // read contract fields
                    Contract contract = new Contract();
                    contract.ConId = ReadInt();
                    contract.Symbol = ReadString();
                    contract.SecType = ReadString();
                    contract.PrimaryExch = ReadString();
                    contract.Currency = ReadString();

                    // read derivative sec types list
                    string[] derivativeSecTypes = new string[0];
                    int nDerivativeSecTypes = ReadInt();
                    if (nDerivativeSecTypes > 0)
                    {
                        Array.Resize(ref derivativeSecTypes, nDerivativeSecTypes);
                        for (int j = 0; j < nDerivativeSecTypes; ++j)
                        {
                            derivativeSecTypes[j] = ReadString();
                        }
                    }

                    ContractDescription contractDescription = new ContractDescription(contract, derivativeSecTypes);
                    contractDescriptions[i] = contractDescription;
                }
            }

            eWrapper.symbolSamples(reqId, contractDescriptions);
        }

        private void FamilyCodesEvent()
        {
            FamilyCode[] familyCodes = new FamilyCode[0];
            int nFamilyCodes = ReadInt();

            if (nFamilyCodes > 0)
            {
                Array.Resize(ref familyCodes, nFamilyCodes);

                for (int i = 0; i < nFamilyCodes; ++i)
                {
                    familyCodes[i] = new FamilyCode(ReadString(), ReadString());
                }
            }

            eWrapper.familyCodes(familyCodes);
        }

        private void MktDepthExchangesEvent()
        {
            DepthMktDataDescription[] depthMktDataDescriptions = new DepthMktDataDescription[0];
            int nDescriptions = ReadInt();

            if (nDescriptions > 0)
            {
                Array.Resize(ref depthMktDataDescriptions, nDescriptions);

                for (int i = 0; i < nDescriptions; i++)
                {
                    if (serverVersion >= MinServerVer.SERVICE_DATA_TYPE)
                    {
                        depthMktDataDescriptions[i] = new DepthMktDataDescription(ReadString(), ReadString(), ReadString(), ReadString(), ReadIntMax());
                    }
                    else
                    {
                        depthMktDataDescriptions[i] = new DepthMktDataDescription(ReadString(), ReadString(), "", ReadBoolFromInt() ? "Deep2" : "Deep", Int32.MaxValue);
                    }
                }
            }

            eWrapper.mktDepthExchanges(depthMktDataDescriptions);
        }

        private void SoftDollarTierEvent()
        {
            int reqId = ReadInt();
            int nTiers = ReadInt();
            SoftDollarTier[] tiers = new SoftDollarTier[nTiers];

            for (int i = 0; i < nTiers; i++)
            {
                tiers[i] = new SoftDollarTier(ReadString(), ReadString(), ReadString());
            }

            eWrapper.softDollarTiers(reqId, tiers);
        }

        private void SecurityDefinitionOptionParameterEndEvent()
        {
            int reqId = ReadInt();

            eWrapper.securityDefinitionOptionParameterEnd(reqId);
        }

        private void SecurityDefinitionOptionParameterEvent()
        {
            int reqId = ReadInt();
            string exchange = ReadString();
            int underlyingConId = ReadInt();
            string tradingClass = ReadString();
            string multiplier = ReadString();
            int expirationsSize = ReadInt();
            HashSet<string> expirations = new HashSet<string>();
            HashSet<double> strikes = new HashSet<double>();

            for (int i = 0; i < expirationsSize; i++)
            {
                expirations.Add(ReadString());
            }

            int strikesSize = ReadInt();

            for (int i = 0; i < strikesSize; i++)
            {
                strikes.Add(ReadDouble());
            }

            eWrapper.securityDefinitionOptionParameter(reqId, exchange, underlyingConId, tradingClass, multiplier, expirations, strikes);
        }

        private void DisplayGroupUpdatedEvent()
        {
            int msgVersion = ReadInt();
            int reqId = ReadInt();
            string contractInfo = ReadString();

            eWrapper.displayGroupUpdated(reqId, contractInfo);
        }

        private void DisplayGroupListEvent()
        {
            int msgVersion = ReadInt();
            int reqId = ReadInt();
            string groups = ReadString();

            eWrapper.displayGroupList(reqId, groups);
        }

        private void VerifyCompletedEvent()
        {
            int msgVersion = ReadInt();
            bool isSuccessful = String.Compare(ReadString(), "true", true) == 0;
            string errorText = ReadString();

            eWrapper.verifyCompleted(isSuccessful, errorText);
        }

        private void VerifyMessageApiEvent()
        {
            int msgVersion = ReadInt();
            string apiData = ReadString();

            eWrapper.verifyMessageAPI(apiData);
        }

        private void VerifyAndAuthCompletedEvent()
        {
            int msgVersion = ReadInt();
            bool isSuccessful = String.Compare(ReadString(), "true", true) == 0;
            string errorText = ReadString();

            eWrapper.verifyAndAuthCompleted(isSuccessful, errorText);
        }

        private void VerifyAndAuthMessageApiEvent()
        {
            int msgVersion = ReadInt();
            string apiData = ReadString();
            string xyzChallenge = ReadString();

            eWrapper.verifyAndAuthMessageAPI(apiData, xyzChallenge);
        }

        private void TickPriceEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            double price = ReadDouble();
            int size = 0;
            
            if (msgVersion >= 2)
                size = ReadInt();

            TickAttrib attr = new TickAttrib();

            if (msgVersion >= 3)
            {
                int attrMask = ReadInt();

                attr.CanAutoExecute = attrMask == 1;

                if (serverVersion >= MinServerVer.PAST_LIMIT)
                {
                    BitMask mask = new BitMask(attrMask);

                    attr.CanAutoExecute = mask[0];
                    attr.PastLimit = mask[1];
                }
            }


            eWrapper.tickPrice(requestId, tickType, price, attr);

            if (msgVersion >= 2)
            {
                int sizeTickType = -1;//not a tick
                switch (tickType)
                {
                    case TickType.BID:
                        sizeTickType = TickType.BID_SIZE;
                        break;
                    case TickType.ASK:
                        sizeTickType = TickType.ASK_SIZE;
                        break;
                    case TickType.LAST:
                        sizeTickType = TickType.LAST_SIZE;
                        break;
                    case TickType.DELAYED_BID:
                        sizeTickType = TickType.DELAYED_BID_SIZE;
                        break;
                    case TickType.DELAYED_ASK:
                        sizeTickType = TickType.DELAYED_ASK_SIZE;
                        break;
                    case TickType.DELAYED_LAST:
                        sizeTickType = TickType.DELAYED_LAST_SIZE;
                        break;
                }
                if (sizeTickType != -1)
                {
                    eWrapper.tickSize(requestId, sizeTickType, size);
                }
            }
        }

        private void TickSizeEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            int size = ReadInt();
            eWrapper.tickSize(requestId, tickType, size);
        }

        private void TickStringEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            string value = ReadString();
            eWrapper.tickString(requestId, tickType, value);
        }

        private void TickGenericEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            double value = ReadDouble();
            eWrapper.tickGeneric(requestId, tickType, value);
        }

        private void TickEFPEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            double basisPoints = ReadDouble();
            string formattedBasisPoints = ReadString();
            double impliedFuturesPrice = ReadDouble();
            int holdDays = ReadInt();
            string futureLastTradeDate = ReadString();
            double dividendImpact = ReadDouble();
            double dividendsToLastTradeDate = ReadDouble();
            eWrapper.tickEFP(requestId, tickType, basisPoints, formattedBasisPoints, impliedFuturesPrice, holdDays, futureLastTradeDate, dividendImpact, dividendsToLastTradeDate);
        }

        private void TickSnapshotEndEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            eWrapper.tickSnapshotEnd(requestId);
        }

        private void ErrorEvent()
        {
            int msgVersion = ReadInt();
            if (msgVersion < 2)
            {
                string msg = ReadString();
                eWrapper.error(msg);
            }
            else
            {
                int id = ReadInt();
                int errorCode = ReadInt();
                string errorMsg = ReadString();
                eWrapper.error(id, errorCode, errorMsg);
            }
        }

        private void CurrentTimeEvent()
        {
            int msgVersion = ReadInt();//version
            long time = ReadLong();
            eWrapper.currentTime(time);
        }

        private void ManagedAccountsEvent()
        {
            int msgVersion = ReadInt();
            string accountsList = ReadString();
            eWrapper.managedAccounts(accountsList);
        }

        private void NextValidIdEvent()
        {
            int msgVersion = ReadInt();
            int orderId = ReadInt();
            eWrapper.nextValidId(orderId);
        }

        private void DeltaNeutralValidationEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            UnderComp underComp = new UnderComp();
            underComp.ConId = ReadInt();
            underComp.Delta = ReadDouble();
            underComp.Price = ReadDouble();
            eWrapper.deltaNeutralValidation(requestId, underComp);
        }

        private void TickOptionComputationEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            double impliedVolatility = ReadDouble();
            if (impliedVolatility < 0)
                impliedVolatility = Double.MaxValue;
            double delta = ReadDouble();
            if (Math.Abs(delta) > 1)
                delta = Double.MaxValue;
            double optPrice = Double.MaxValue;
            double pvDividend = Double.MaxValue;
            double gamma = Double.MaxValue;
            double vega = Double.MaxValue;
            double theta = Double.MaxValue;
            double undPrice = Double.MaxValue;
            if (msgVersion >= 6 || tickType == TickType.MODEL_OPTION || tickType == TickType.DELAYED_MODEL_OPTION)
            {
                optPrice = ReadDouble();
                if (optPrice < 0)
                { // -1 is the "not yet computed" indicator
                    optPrice = Double.MaxValue;
                }
                pvDividend = ReadDouble();
                if (pvDividend < 0)
                { // -1 is the "not yet computed" indicator
                    pvDividend = Double.MaxValue;
                }
            }
            if (msgVersion >= 6)
            {
                gamma = ReadDouble();
                if (Math.Abs(gamma) > 1)
                { // -2 is the "not yet computed" indicator
                    gamma = Double.MaxValue;
                }
                vega = ReadDouble();
                if (Math.Abs(vega) > 1)
                { // -2 is the "not yet computed" indicator
                    vega = Double.MaxValue;
                }
                theta = ReadDouble();
                if (Math.Abs(theta) > 1)
                { // -2 is the "not yet computed" indicator
                    theta = Double.MaxValue;
                }
                undPrice = ReadDouble();
                if (undPrice < 0)
                { // -1 is the "not yet computed" indicator
                    undPrice = Double.MaxValue;
                }
            }

            eWrapper.tickOptionComputation(requestId, tickType, impliedVolatility, delta, optPrice, pvDividend, gamma, vega, theta, undPrice);
        }

        private void AccountSummaryEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            string account = ReadString();
            string tag = ReadString();
            string value = ReadString();
            string currency = ReadString();
            eWrapper.accountSummary(requestId, account, tag, value, currency);
        }

        private void AccountSummaryEndEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            eWrapper.accountSummaryEnd(requestId);
        }

        private void AccountValueEvent()
        {
            int msgVersion = ReadInt();
            string key = ReadString();
            string value = ReadString();
            string currency = ReadString();
            string accountName = null;
            if (msgVersion >= 2)
                accountName = ReadString();
            eWrapper.updateAccountValue(key, value, currency, accountName);
        }

        private void BondContractDetailsEvent()
        {
            int msgVersion = ReadInt();
            int requestId = -1;
            if (msgVersion >= 3)
            {
                requestId = ReadInt();
            }

            ContractDetails contract = new ContractDetails();

            contract.Summary.Symbol = ReadString();
            contract.Summary.SecType = ReadString();
            contract.Cusip = ReadString();
            contract.Coupon = ReadDouble();
            contract.Maturity = ReadString();
            contract.IssueDate = ReadString();
            contract.Ratings = ReadString();
            contract.BondType = ReadString();
            contract.CouponType = ReadString();
            contract.Convertible = ReadBoolFromInt();
            contract.Callable = ReadBoolFromInt();
            contract.Putable = ReadBoolFromInt();
            contract.DescAppend = ReadString();
            contract.Summary.Exchange = ReadString();
            contract.Summary.Currency = ReadString();
            contract.MarketName = ReadString();
            contract.Summary.TradingClass = ReadString();
            contract.Summary.ConId = ReadInt();
            contract.MinTick = ReadDouble();
            if (serverVersion >= MinServerVer.MD_SIZE_MULTIPLIER)
            {
                contract.MdSizeMultiplier = ReadInt();
            }
            contract.OrderTypes = ReadString();
            contract.ValidExchanges = ReadString();
            if (msgVersion >= 2)
            {
                contract.NextOptionDate = ReadString();
                contract.NextOptionType = ReadString();
                contract.NextOptionPartial = ReadBoolFromInt();
                contract.Notes = ReadString();
            }
            if (msgVersion >= 4)
            {
                contract.LongName = ReadString();
            }
            if (msgVersion >= 6)
            {
                contract.EvRule = ReadString();
                contract.EvMultiplier = ReadDouble();
            }
            if (msgVersion >= 5)
            {
                int secIdListCount = ReadInt();
                if (secIdListCount > 0)
                {
                    contract.SecIdList = new List<TagValue>();
                    for (int i = 0; i < secIdListCount; ++i)
                    {
                        TagValue tagValue = new TagValue();
                        tagValue.Tag = ReadString();
                        tagValue.Value = ReadString();
                        contract.SecIdList.Add(tagValue);
                    }
                }
            }
            if (serverVersion >= MinServerVer.AGG_GROUP)
            {
                contract.AggGroup = ReadInt();
            }
            if (serverVersion >= MinServerVer.MARKET_RULES)
            {
                contract.MarketRuleIds = ReadString();
            }

            eWrapper.bondContractDetails(requestId, contract);
        }

        private void PortfolioValueEvent()
        {
            int msgVersion = ReadInt();
            Contract contract = new Contract();
            if (msgVersion >= 6)
                contract.ConId = ReadInt();
            contract.Symbol = ReadString();
            contract.SecType = ReadString();
            contract.LastTradeDateOrContractMonth = ReadString();
            contract.Strike = ReadDouble();
            contract.Right = ReadString();
            if (msgVersion >= 7)
            {
                contract.Multiplier = ReadString();
                contract.PrimaryExch = ReadString();
            }
            contract.Currency = ReadString();
            if (msgVersion >= 2)
            {
                contract.LocalSymbol = ReadString();
            }
            if (msgVersion >= 8)
            {
                contract.TradingClass = ReadString();
            }

            var position = serverVersion >= MinServerVer.FRACTIONAL_POSITIONS ? ReadDouble() : (double)ReadInt();
            double marketPrice = ReadDouble();
            double marketValue = ReadDouble();
            double averageCost = 0.0;
            double unrealizedPNL = 0.0;
            double realizedPNL = 0.0;
            if (msgVersion >= 3)
            {
                averageCost = ReadDouble();
                unrealizedPNL = ReadDouble();
                realizedPNL = ReadDouble();
            }

            string accountName = null;
            if (msgVersion >= 4)
            {
                accountName = ReadString();
            }

            if (msgVersion == 6 && serverVersion == 39)
            {
                contract.PrimaryExch = ReadString();
            }

            eWrapper.updatePortfolio(contract, position, marketPrice, marketValue,
                            averageCost, unrealizedPNL, realizedPNL, accountName);
        }

        private void AccountUpdateTimeEvent()
        {
            int msgVersion = ReadInt();
            string timestamp = ReadString();
            eWrapper.updateAccountTime(timestamp);
        }

        private void AccountDownloadEndEvent()
        {
            int msgVersion = ReadInt();
            string account = ReadString();
            eWrapper.accountDownloadEnd(account);
        }

        private void OrderStatusEvent()
        {
            int msgVersion = ReadInt();
            int id = ReadInt();
            string status = ReadString();
            double filled = serverVersion >= MinServerVer.FRACTIONAL_POSITIONS ? ReadDouble() : (double)ReadInt();
            double remaining = serverVersion >= MinServerVer.FRACTIONAL_POSITIONS ? ReadDouble() : (double)ReadInt();
            double avgFillPrice = ReadDouble();

            int permId = 0;
            if (msgVersion >= 2)
            {
                permId = ReadInt();
            }

            int parentId = 0;
            if (msgVersion >= 3)
            {
                parentId = ReadInt();
            }

            double lastFillPrice = 0;
            if (msgVersion >= 4)
            {
                lastFillPrice = ReadDouble();
            }

            int clientId = 0;
            if (msgVersion >= 5)
            {
                clientId = ReadInt();
            }

            string whyHeld = null;
            if (msgVersion >= 6)
            {
                whyHeld = ReadString();
            }

            eWrapper.orderStatus(id, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld);
        }

        private void OpenOrderEvent()
        {
            int msgVersion = ReadInt();
            // read order id
            Order order = new Order();
            order.OrderId = ReadInt();

            // read contract fields
            Contract contract = new Contract();
            if (msgVersion >= 17)
            {
                contract.ConId = ReadInt();
            }
            contract.Symbol = ReadString();
            contract.SecType = ReadString();
            contract.LastTradeDateOrContractMonth = ReadString();
            contract.Strike = ReadDouble();
            contract.Right = ReadString();
            if (msgVersion >= 32)
            {
                contract.Multiplier = ReadString();
            }
            contract.Exchange = ReadString();
            contract.Currency = ReadString();
            if (msgVersion >= 2)
            {
                contract.LocalSymbol = ReadString();
            }
            if (msgVersion >= 32)
            {
                contract.TradingClass = ReadString();
            }

            // read order fields
            order.Action = ReadString();
            order.TotalQuantity = serverVersion >= MinServerVer.FRACTIONAL_POSITIONS ? ReadDouble() : (double)ReadInt();
            order.OrderType = ReadString();
            if (msgVersion < 29)
            {
                order.LmtPrice = ReadDouble();
            }
            else
            {
                order.LmtPrice = ReadDoubleMax();
            }
            if (msgVersion < 30)
            {
                order.AuxPrice = ReadDouble();
            }
            else
            {
                order.AuxPrice = ReadDoubleMax();
            }
            order.Tif = ReadString();
            order.OcaGroup = ReadString();
            order.Account = ReadString();
            order.OpenClose = ReadString();
            order.Origin = ReadInt();
            order.OrderRef = ReadString();

            if (msgVersion >= 3)
            {
                order.ClientId = ReadInt();
            }

            if (msgVersion >= 4)
            {
                order.PermId = ReadInt();
                if (msgVersion < 18)
                {
                    // will never happen
                    /* order.ignoreRth = */
                    ReadBoolFromInt();
                }
                else
                {
                    order.OutsideRth = ReadBoolFromInt();
                }
                order.Hidden = ReadInt() == 1;
                order.DiscretionaryAmt = ReadDouble();
            }

            if (msgVersion >= 5)
            {
                order.GoodAfterTime = ReadString();
            }

            if (msgVersion >= 6)
            {
                // skip deprecated sharesAllocation field
                ReadString();
            }

            if (msgVersion >= 7)
            {
                order.FaGroup = ReadString();
                order.FaMethod = ReadString();
                order.FaPercentage = ReadString();
                order.FaProfile = ReadString();
            }

            if (serverVersion >= MinServerVer.MODELS_SUPPORT)
            {
                order.ModelCode = ReadString();
            }

            if (msgVersion >= 8)
            {
                order.GoodTillDate = ReadString();
            }

            if (msgVersion >= 9)
            {
                order.Rule80A = ReadString();
                order.PercentOffset = ReadDoubleMax();
                order.SettlingFirm = ReadString();
                order.ShortSaleSlot = ReadInt();
                order.DesignatedLocation = ReadString();
                if (serverVersion == 51)
                {
                    ReadInt(); // exemptCode
                }
                else if (msgVersion >= 23)
                {
                    order.ExemptCode = ReadInt();
                }
                order.AuctionStrategy = ReadInt();
                order.StartingPrice = ReadDoubleMax();
                order.StockRefPrice = ReadDoubleMax();
                order.Delta = ReadDoubleMax();
                order.StockRangeLower = ReadDoubleMax();
                order.StockRangeUpper = ReadDoubleMax();
                order.DisplaySize = ReadInt();
                if (msgVersion < 18)
                {
                    // will never happen
                    /* order.rthOnly = */
                    ReadBoolFromInt();
                }
                order.BlockOrder = ReadBoolFromInt();
                order.SweepToFill = ReadBoolFromInt();
                order.AllOrNone = ReadBoolFromInt();
                order.MinQty = ReadIntMax();
                order.OcaType = ReadInt();
                order.ETradeOnly = ReadBoolFromInt();
                order.FirmQuoteOnly = ReadBoolFromInt();
                order.NbboPriceCap = ReadDoubleMax();
            }

            if (msgVersion >= 10)
            {
                order.ParentId = ReadInt();
                order.TriggerMethod = ReadInt();
            }

            if (msgVersion >= 11)
            {
                order.Volatility = ReadDoubleMax();
                order.VolatilityType = ReadInt();
                if (msgVersion == 11)
                {
                    int receivedInt = ReadInt();
                    order.DeltaNeutralOrderType = ((receivedInt == 0) ? "NONE" : "MKT");
                }
                else
                { // msgVersion 12 and up
                    order.DeltaNeutralOrderType = ReadString();
                    order.DeltaNeutralAuxPrice = ReadDoubleMax();

                    if (msgVersion >= 27 && !Util.StringIsEmpty(order.DeltaNeutralOrderType))
                    {
                        order.DeltaNeutralConId = ReadInt();
                        order.DeltaNeutralSettlingFirm = ReadString();
                        order.DeltaNeutralClearingAccount = ReadString();
                        order.DeltaNeutralClearingIntent = ReadString();
                    }

                    if (msgVersion >= 31 && !Util.StringIsEmpty(order.DeltaNeutralOrderType))
                    {
                        order.DeltaNeutralOpenClose = ReadString();
                        order.DeltaNeutralShortSale = ReadBoolFromInt();
                        order.DeltaNeutralShortSaleSlot = ReadInt();
                        order.DeltaNeutralDesignatedLocation = ReadString();
                    }
                }
                order.ContinuousUpdate = ReadInt();
                if (serverVersion == 26)
                {
                    order.StockRangeLower = ReadDouble();
                    order.StockRangeUpper = ReadDouble();
                }
                order.ReferencePriceType = ReadInt();
            }

            if (msgVersion >= 13)
            {
                order.TrailStopPrice = ReadDoubleMax();
            }

            if (msgVersion >= 30)
            {
                order.TrailingPercent = ReadDoubleMax();
            }

            if (msgVersion >= 14)
            {
                order.BasisPoints = ReadDoubleMax();
                order.BasisPointsType = ReadIntMax();
                contract.ComboLegsDescription = ReadString();
            }

            if (msgVersion >= 29)
            {
                int comboLegsCount = ReadInt();
                if (comboLegsCount > 0)
                {
                    contract.ComboLegs = new List<ComboLeg>(comboLegsCount);
                    for (int i = 0; i < comboLegsCount; ++i)
                    {
                        int conId = ReadInt();
                        int ratio = ReadInt();
                        String action = ReadString();
                        String exchange = ReadString();
                        int openClose = ReadInt();
                        int shortSaleSlot = ReadInt();
                        String designatedLocation = ReadString();
                        int exemptCode = ReadInt();

                        ComboLeg comboLeg = new ComboLeg(conId, ratio, action, exchange, openClose,
                                shortSaleSlot, designatedLocation, exemptCode);
                        contract.ComboLegs.Add(comboLeg);
                    }
                }

                int orderComboLegsCount = ReadInt();
                if (orderComboLegsCount > 0)
                {
                    order.OrderComboLegs = new List<OrderComboLeg>(orderComboLegsCount);
                    for (int i = 0; i < orderComboLegsCount; ++i)
                    {
                        double price = ReadDoubleMax();

                        OrderComboLeg orderComboLeg = new OrderComboLeg(price);
                        order.OrderComboLegs.Add(orderComboLeg);
                    }
                }
            }

            if (msgVersion >= 26)
            {
                int smartComboRoutingParamsCount = ReadInt();
                if (smartComboRoutingParamsCount > 0)
                {
                    order.SmartComboRoutingParams = new List<TagValue>(smartComboRoutingParamsCount);
                    for (int i = 0; i < smartComboRoutingParamsCount; ++i)
                    {
                        TagValue tagValue = new TagValue();
                        tagValue.Tag = ReadString();
                        tagValue.Value = ReadString();
                        order.SmartComboRoutingParams.Add(tagValue);
                    }
                }
            }

            if (msgVersion >= 15)
            {
                if (msgVersion >= 20)
                {
                    order.ScaleInitLevelSize = ReadIntMax();
                    order.ScaleSubsLevelSize = ReadIntMax();
                }
                else
                {
                    /* int notSuppScaleNumComponents = */
                    ReadIntMax();
                    order.ScaleInitLevelSize = ReadIntMax();
                }
                order.ScalePriceIncrement = ReadDoubleMax();
            }

            if (msgVersion >= 28 && order.ScalePriceIncrement > 0.0 && order.ScalePriceIncrement != Double.MaxValue)
            {
                order.ScalePriceAdjustValue = ReadDoubleMax();
                order.ScalePriceAdjustInterval = ReadIntMax();
                order.ScaleProfitOffset = ReadDoubleMax();
                order.ScaleAutoReset = ReadBoolFromInt();
                order.ScaleInitPosition = ReadIntMax();
                order.ScaleInitFillQty = ReadIntMax();
                order.ScaleRandomPercent = ReadBoolFromInt();
            }

            if (msgVersion >= 24)
            {
                order.HedgeType = ReadString();
                if (!Util.StringIsEmpty(order.HedgeType))
                {
                    order.HedgeParam = ReadString();
                }
            }

            if (msgVersion >= 25)
            {
                order.OptOutSmartRouting = ReadBoolFromInt();
            }

            if (msgVersion >= 19)
            {
                order.ClearingAccount = ReadString();
                order.ClearingIntent = ReadString();
            }

            if (msgVersion >= 22)
            {
                order.NotHeld = ReadBoolFromInt();
            }

            if (msgVersion >= 20)
            {
                if (ReadBoolFromInt())
                {
                    UnderComp underComp = new UnderComp();
                    underComp.ConId = ReadInt();
                    underComp.Delta = ReadDouble();
                    underComp.Price = ReadDouble();
                    contract.UnderComp = underComp;
                }
            }

            if (msgVersion >= 21)
            {
                order.AlgoStrategy = ReadString();
                if (!Util.StringIsEmpty(order.AlgoStrategy))
                {
                    int algoParamsCount = ReadInt();
                    if (algoParamsCount > 0)
                    {
                        order.AlgoParams = new List<TagValue>(algoParamsCount);
                        for (int i = 0; i < algoParamsCount; ++i)
                        {
                            TagValue tagValue = new TagValue();
                            tagValue.Tag = ReadString();
                            tagValue.Value = ReadString();
                            order.AlgoParams.Add(tagValue);
                        }
                    }
                }
            }

            if (msgVersion >= 33)
            {
                order.Solicited = ReadBoolFromInt();
            }

            OrderState orderState = new OrderState();
            if (msgVersion >= 16)
            {
                order.WhatIf = ReadBoolFromInt();
                orderState.Status = ReadString();
                orderState.InitMargin = ReadString();
                orderState.MaintMargin = ReadString();
                orderState.EquityWithLoan = ReadString();
                orderState.Commission = ReadDoubleMax();
                orderState.MinCommission = ReadDoubleMax();
                orderState.MaxCommission = ReadDoubleMax();
                orderState.CommissionCurrency = ReadString();
                orderState.WarningText = ReadString();
            }

            if (msgVersion >= 34)
            {
                order.RandomizeSize = ReadBoolFromInt();
                order.RandomizePrice = ReadBoolFromInt();
            }

            if (serverVersion >= MinServerVer.PEGGED_TO_BENCHMARK)
            {
                if (order.OrderType == "PEG BENCH")
                {
                    order.ReferenceContractId = ReadInt();
                    order.IsPeggedChangeAmountDecrease = ReadBoolFromInt();
                    order.PeggedChangeAmount = ReadDoubleMax();
                    order.ReferenceChangeAmount = ReadDoubleMax();
                    order.ReferenceExchange = ReadString();
                }

                int nConditions = ReadInt();

                if (nConditions > 0)
                {
                    for (int i = 0; i < nConditions; i++)
                    {
                        OrderConditionType orderConditionType = (OrderConditionType)ReadInt();
                        OrderCondition condition = OrderCondition.Create(orderConditionType);

                        condition.Deserialize(this);
                        order.Conditions.Add(condition);
                    }

                    order.ConditionsIgnoreRth = ReadBoolFromInt();
                    order.ConditionsCancelOrder = ReadBoolFromInt();
                }

                order.AdjustedOrderType = ReadString();
                order.TriggerPrice = ReadDoubleMax();
                order.TrailStopPrice = ReadDoubleMax();
                order.LmtPriceOffset = ReadDoubleMax();
                order.AdjustedStopPrice = ReadDoubleMax();
                order.AdjustedStopLimitPrice = ReadDoubleMax();
                order.AdjustedTrailingAmount = ReadDoubleMax();
                order.AdjustableTrailingUnit = ReadInt();
            }

            if (serverVersion >= MinServerVer.SOFT_DOLLAR_TIER)
            {
                order.Tier = new SoftDollarTier(ReadString(), ReadString(), ReadString());
            }

            if (serverVersion >= MinServerVer.CASH_QTY)
            {
                order.CashQty = ReadDoubleMax();
            }

            eWrapper.openOrder(order.OrderId, contract, order, orderState);
        }

        private void OpenOrderEndEvent()
        {
            int msgVersion = ReadInt();
            eWrapper.openOrderEnd();
        }

        private void ContractDataEvent()
        {
            int msgVersion = ReadInt();
            int requestId = -1;
            if (msgVersion >= 3)
                requestId = ReadInt();
            ContractDetails contract = new ContractDetails();
            contract.Summary.Symbol = ReadString();
            contract.Summary.SecType = ReadString();
            contract.Summary.LastTradeDateOrContractMonth = ReadString();
            contract.Summary.Strike = ReadDouble();
            contract.Summary.Right = ReadString();
            contract.Summary.Exchange = ReadString();
            contract.Summary.Currency = ReadString();
            contract.Summary.LocalSymbol = ReadString();
            contract.MarketName = ReadString();
            contract.Summary.TradingClass = ReadString();
            contract.Summary.ConId = ReadInt();
            contract.MinTick = ReadDouble();
            if (serverVersion >= MinServerVer.MD_SIZE_MULTIPLIER)
            {
                contract.MdSizeMultiplier = ReadInt();
            }
            contract.Summary.Multiplier = ReadString();
            contract.OrderTypes = ReadString();
            contract.ValidExchanges = ReadString();
            if (msgVersion >= 2)
            {
                contract.PriceMagnifier = ReadInt();
            }
            if (msgVersion >= 4)
            {
                contract.UnderConId = ReadInt();
            }
            if (msgVersion >= 5)
            {
                contract.LongName = ReadString();
                contract.Summary.PrimaryExch = ReadString();
            }
            if (msgVersion >= 6)
            {
                contract.ContractMonth = ReadString();
                contract.Industry = ReadString();
                contract.Category = ReadString();
                contract.Subcategory = ReadString();
                contract.TimeZoneId = ReadString();
                contract.TradingHours = ReadString();
                contract.LiquidHours = ReadString();
            }
            if (msgVersion >= 8)
            {
                contract.EvRule = ReadString();
                contract.EvMultiplier = ReadDouble();
            }
            if (msgVersion >= 7)
            {
                int secIdListCount = ReadInt();
                if (secIdListCount > 0)
                {
                    contract.SecIdList = new List<TagValue>(secIdListCount);
                    for (int i = 0; i < secIdListCount; ++i)
                    {
                        TagValue tagValue = new TagValue();
                        tagValue.Tag = ReadString();
                        tagValue.Value = ReadString();
                        contract.SecIdList.Add(tagValue);
                    }
                }
            }
            if (serverVersion >= MinServerVer.AGG_GROUP)
            {
                contract.AggGroup = ReadInt();
            }
            if (serverVersion >= MinServerVer.UNDERLYING_INFO)
            {
                contract.UnderSymbol = ReadString();
                contract.UnderSecType = ReadString();
            }
            if (serverVersion >= MinServerVer.MARKET_RULES)
            {
                contract.MarketRuleIds = ReadString();
            }

            eWrapper.contractDetails(requestId, contract);
        }


        private void ContractDataEndEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            eWrapper.contractDetailsEnd(requestId);
        }

        private void ExecutionDataEvent()
        {
            int msgVersion = ReadInt();
            int requestId = -1;
            if (msgVersion >= 7)
                requestId = ReadInt();
            int orderId = ReadInt();
            Contract contract = new Contract();
            if (msgVersion >= 5)
            {
                contract.ConId = ReadInt();
            }
            contract.Symbol = ReadString();
            contract.SecType = ReadString();
            contract.LastTradeDateOrContractMonth = ReadString();
            contract.Strike = ReadDouble();
            contract.Right = ReadString();
            if (msgVersion >= 9)
            {
                contract.Multiplier = ReadString();
            }
            contract.Exchange = ReadString();
            contract.Currency = ReadString();
            contract.LocalSymbol = ReadString();
            if (msgVersion >= 10)
            {
                contract.TradingClass = ReadString();
            }

            Execution exec = new Execution();
            exec.OrderId = orderId;
            exec.ExecId = ReadString();
            exec.Time = ReadString();
            exec.AcctNumber = ReadString();
            exec.Exchange = ReadString();
            exec.Side = ReadString();
            exec.Shares = serverVersion >= MinServerVer.FRACTIONAL_POSITIONS ? ReadDouble() : (double)ReadInt();
            exec.Price = ReadDouble();
            if (msgVersion >= 2)
            {
                exec.PermId = ReadInt();
            }
            if (msgVersion >= 3)
            {
                exec.ClientId = ReadInt();
            }
            if (msgVersion >= 4)
            {
                exec.Liquidation = ReadInt();
            }
            if (msgVersion >= 6)
            {
                exec.CumQty = ReadDouble();
                exec.AvgPrice = ReadDouble();
            }
            if (msgVersion >= 8)
            {
                exec.OrderRef = ReadString();
            }
            if (msgVersion >= 9)
            {
                exec.EvRule = ReadString();
                exec.EvMultiplier = ReadDouble();
            }
            if (serverVersion >= MinServerVer.MODELS_SUPPORT)
            {
                exec.ModelCode = ReadString();
            }

            eWrapper.execDetails(requestId, contract, exec);
        }

        private void ExecutionDataEndEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            eWrapper.execDetailsEnd(requestId);
        }

        private void CommissionReportEvent()
        {
            int msgVersion = ReadInt();
            CommissionReport commissionReport = new CommissionReport();
            commissionReport.ExecId = ReadString();
            commissionReport.Commission = ReadDouble();
            commissionReport.Currency = ReadString();
            commissionReport.RealizedPNL = ReadDouble();
            commissionReport.Yield = ReadDouble();
            commissionReport.YieldRedemptionDate = ReadInt();
            eWrapper.commissionReport(commissionReport);
        }

        private void FundamentalDataEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            string fundamentalData = ReadString();
            eWrapper.fundamentalData(requestId, fundamentalData);
        }

        private void HistoricalDataEvent()
        {
            int msgVersion = int.MaxValue;

            if (serverVersion < MinServerVer.SYNT_REALTIME_BARS)
            {
                msgVersion = ReadInt();
            }

            int requestId = ReadInt();
            string startDateStr = "";
            string endDateStr = "";

            if (msgVersion >= 2)
            {
                startDateStr = ReadString();
                endDateStr = ReadString();
            }

            int itemCount = ReadInt();
            
            for (int ctr = 0; ctr < itemCount; ctr++)
            {
                string date = ReadString();
                double open = ReadDouble();
                double high = ReadDouble();
                double low = ReadDouble();
                double close = ReadDouble();
                long volume = serverVersion < MinServerVer.SYNT_REALTIME_BARS ? ReadInt() : ReadLong();
                double WAP = ReadDouble();

                if (serverVersion < MinServerVer.SYNT_REALTIME_BARS)
                {
                    /*string hasGaps = */ReadString();
                }

                int barCount = -1;
                
                if (msgVersion >= 3)
                {
                    barCount = ReadInt();
                }

                eWrapper.historicalData(requestId, new Bar(date, open, high, low,
                                        close, volume, barCount, WAP));
            }

            // send end of dataset marker.
            eWrapper.historicalDataEnd(requestId, startDateStr, endDateStr);
        }

        private void MarketDataTypeEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int marketDataType = ReadInt();
            eWrapper.marketDataType(requestId, marketDataType);
        }

        private void MarketDepthEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int position = ReadInt();
            int operation = ReadInt();
            int side = ReadInt();
            double price = ReadDouble();
            int size = ReadInt();
            eWrapper.updateMktDepth(requestId, position, operation, side, price, size);
        }

        private void MarketDepthL2Event()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int position = ReadInt();
            string marketMaker = ReadString();
            int operation = ReadInt();
            int side = ReadInt();
            double price = ReadDouble();
            int size = ReadInt();
            eWrapper.updateMktDepthL2(requestId, position, marketMaker, operation, side, price, size);
        }

        private void NewsBulletinsEvent()
        {
            int msgVersion = ReadInt();
            int newsMsgId = ReadInt();
            int newsMsgType = ReadInt();
            string newsMessage = ReadString();
            string originatingExch = ReadString();
            eWrapper.updateNewsBulletin(newsMsgId, newsMsgType, newsMessage, originatingExch);
        }

        private void PositionEvent()
        {
            int msgVersion = ReadInt();
            string account = ReadString();
            Contract contract = new Contract();
            contract.ConId = ReadInt();
            contract.Symbol = ReadString();
            contract.SecType = ReadString();
            contract.LastTradeDateOrContractMonth = ReadString();
            contract.Strike = ReadDouble();
            contract.Right = ReadString();
            contract.Multiplier = ReadString();
            contract.Exchange = ReadString();
            contract.Currency = ReadString();
            contract.LocalSymbol = ReadString();
            if (msgVersion >= 2)
            {
                contract.TradingClass = ReadString();
            }

            var pos = serverVersion >= MinServerVer.FRACTIONAL_POSITIONS ? ReadDouble() : (double)ReadInt();
            double avgCost = 0;
            if (msgVersion >= 3)
                avgCost = ReadDouble();
            eWrapper.position(account, contract, pos, avgCost);
        }

        private void PositionEndEvent()
        {
            int msgVersion = ReadInt();
            eWrapper.positionEnd();
        }

        private void RealTimeBarsEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            long time = ReadLong();
            double open = ReadDouble();
            double high = ReadDouble();
            double low = ReadDouble();
            double close = ReadDouble();
            long volume = ReadLong();
            double wap = ReadDouble();
            int count = ReadInt();
            eWrapper.realtimeBar(requestId, time, open, high, low, close, volume, wap, count);
        }

        private void ScannerParametersEvent()
        {
            int msgVersion = ReadInt();
            string xml = ReadString();
            eWrapper.scannerParameters(xml);
        }

        private void ScannerDataEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int numberOfElements = ReadInt();
            for (int i = 0; i < numberOfElements; i++)
            {
                int rank = ReadInt();
                ContractDetails conDet = new ContractDetails();
                if (msgVersion >= 3)
                    conDet.Summary.ConId = ReadInt();
                conDet.Summary.Symbol = ReadString();
                conDet.Summary.SecType = ReadString();
                conDet.Summary.LastTradeDateOrContractMonth = ReadString();
                conDet.Summary.Strike = ReadDouble();
                conDet.Summary.Right = ReadString();
                conDet.Summary.Exchange = ReadString();
                conDet.Summary.Currency = ReadString();
                conDet.Summary.LocalSymbol = ReadString();
                conDet.MarketName = ReadString();
                conDet.Summary.TradingClass = ReadString();
                string distance = ReadString();
                string benchmark = ReadString();
                string projection = ReadString();
                string legsStr = null;
                if (msgVersion >= 2)
                {
                    legsStr = ReadString();
                }
                eWrapper.scannerData(requestId, rank, conDet, distance,
                    benchmark, projection, legsStr);
            }
            eWrapper.scannerDataEnd(requestId);
        }

        private void ReceiveFAEvent()
        {
            int msgVersion = ReadInt();
            int faDataType = ReadInt();
            string faData = ReadString();
            eWrapper.receiveFA(faDataType, faData);
        }

        private void PositionMultiEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            string account = ReadString();
            Contract contract = new Contract();
            contract.ConId = ReadInt();
            contract.Symbol = ReadString();
            contract.SecType = ReadString();
            contract.LastTradeDateOrContractMonth = ReadString();
            contract.Strike = ReadDouble();
            contract.Right = ReadString();
            contract.Multiplier = ReadString();
            contract.Exchange = ReadString();
            contract.Currency = ReadString();
            contract.LocalSymbol = ReadString();
            contract.TradingClass = ReadString();
            var pos = ReadDouble();
            double avgCost = ReadDouble();
            string modelCode = ReadString();
            eWrapper.positionMulti(requestId, account, modelCode, contract, pos, avgCost);
        }

        private void PositionMultiEndEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            eWrapper.positionMultiEnd(requestId);
        }

        private void AccountUpdateMultiEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            string account = ReadString();
            string modelCode = ReadString();
            string key = ReadString();
            string value = ReadString();
            string currency = ReadString();
            eWrapper.accountUpdateMulti(requestId, account, modelCode, key, value, currency);
        }

        private void AccountUpdateMultiEndEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            eWrapper.accountUpdateMultiEnd(requestId);
        }

        public double ReadDouble()
        {
            string doubleAsstring = ReadString();
            if (string.IsNullOrEmpty(doubleAsstring) ||
                doubleAsstring == "0")
            {
                return 0;
            }
            else return Double.Parse(doubleAsstring, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public double ReadDoubleMax()
        {
            string str = ReadString();
            return (str == null || str.Length == 0) ? Double.MaxValue : Double.Parse(str, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public long ReadLong()
        {
            string longAsstring = ReadString();
            if (string.IsNullOrEmpty(longAsstring) ||
                longAsstring == "0")
            {
                return 0;
            }
            else return Int64.Parse(longAsstring);
        }

        public int ReadInt()
        {
            string intAsstring = ReadString();
            if (string.IsNullOrEmpty(intAsstring) ||
                intAsstring == "0")
            {
                return 0;
            }
            else return Int32.Parse(intAsstring);
        }

        public int ReadIntMax()
        {
            string str = ReadString();
            return (str == null || str.Length == 0) ? Int32.MaxValue : Int32.Parse(str);
        }

        public bool ReadBoolFromInt()
        {
            string str = ReadString();
            return str == null ? false : (Int32.Parse(str) != 0);
        }

        public char ReadChar()
        {
            string str = ReadString();
            return str == null ? '\0' : str[0];
        }

        public string ReadString()
        {
            byte b = dataReader.ReadByte();

            nDecodedLen++;

            if (b == 0)
            {
                return null;
            }
            else
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append((char)b);
                while (true)
                {
                    b = dataReader.ReadByte();
                    if (b == 0)
                    {
                        break;
                    }
                    else
                    {
                        strBuilder.Append((char)b);
                    }
                }

                nDecodedLen += strBuilder.Length;

                return strBuilder.ToString();
            }
        }
    }
}
