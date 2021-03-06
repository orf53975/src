//+------------------------------------------------------------------+
//|                                                       KClock.mq4 |
//|                        Copyright 2018, MetaQuotes Software Corp. |
//|                                             https://www.mql5.com |
//+------------------------------------------------------------------+
#property copyright "Copyright 2018, MetaQuotes Software Corp."
#property link      "https://www.mql5.com"
#property version   "1.00"
#property strict
//+------------------------------------------------------------------+
//| Expert initialization function                                   |
//+------------------------------------------------------------------+
int OnInit()
  {
//--- create timer
   EventSetTimer(1);
      
//---
   return(INIT_SUCCEEDED);
  }
//+------------------------------------------------------------------+
//| Expert deinitialization function                                 |
//+------------------------------------------------------------------+
void OnDeinit(const int reason)
  {
//--- destroy timer
   EventKillTimer();
      
  }
//+------------------------------------------------------------------+
//| Expert tick function                                             |
//+------------------------------------------------------------------+
void OnTick()
  {
//---
      switch(Period())
      {      
      case 1:      
         {         
            int a=60-Seconds();         
            Comment(a);         
            break;      
         }      
      case 5:      
         {         
            int a=Period()*60-Minute()%5*60-Seconds();         
            Comment(a/60,":",a%60);         
            break;      
         }            
      case 15:      
         {         
            int a=Period()*60-Minute()%15*60-Seconds();         
            Comment(a/60,":",a%60);         
            break;      
         }      
      case 30:       
         {         
            int a=Period()*60-Minute()%30*60-Seconds();         
            Comment(a/60,":",a%60);         
            break;      
         }        
      case 60:      
         {         
            int a=Period()*60-Minute()*60-Seconds();         
            Comment(a/60,":",a%60);         
            break;      
         }      
      case 240:      
         {        
            int a=Period()*60-Hour()%4*3600-Minute()*60-Seconds();         
            Comment(a/3600,":",a%3600/60,":",a%3600%60);         
            break;      
         }      
      case 1440:      
         {         
            int a=Period()*60-Hour()*3600-Minute()*60-Seconds();         
            Comment(a/3600,":",a%3600/60,":",a%60);         
            break;      
         }      
      case 10080:      
         {         
            int a=Period()*60-TimeDayOfWeek(TimeCurrent())*1440*60-Hour()*3600-Minute()*60-Seconds();
            Comment(a/86400,":",a%86400/3600,":",a%86400%3600/60,":",a%86400%3600%60);         
            break;      
         }      
      case 43200:      
         {         
            int a=Period()*60-TimeDay(TimeCurrent())*1440*60-Hour()*3600-Minute()*60-Seconds();         
            Comment(a/86400,":",a%86400/3600,":",a%86400%3600/60,":",a%86400%3600%60);         
            break;      
         }      
      default:      
         {         
            string c="failed！！！";         
            Comment(c);        
            break;      
         }         
      }   
  }
//+------------------------------------------------------------------+
//| Timer function                                                   |
//+------------------------------------------------------------------+
void OnTimer()
  {
//---
   OnTick();
   
  }
//+------------------------------------------------------------------+
