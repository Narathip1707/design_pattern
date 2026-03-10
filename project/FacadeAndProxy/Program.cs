using System;
using System.Collections.Generic;
namespace FacadeNara
{
    public class Program
    {
        public static void Main()
        {   
            Wallet w = new Wallet();
            MarketService m = new MarketService();
            Portfolio p = new Portfolio();
            NotificationService n = new NotificationService();
            

            TradingFacade realApp = new TradingFacade(w, m, p, n);
            ITradingService secureApp = new TradingProxy(realApp);

            secureApp.ExecuteTrade("NVDA", 10);   // ซื้อหุ้น Nvidia
            secureApp.ExecuteTrade("S&P500", 2); // ซื้อกองทุน S&P500
            secureApp.ExecuteTrade("GOOGL", 5);  // ซื้อหุ้น Google
            secureApp.ExecuteTrade("AMZN", 100); // ลองซื้อเยอะๆ เพื่อดูระบบเช็กเงิน

        

            // Console.WriteLine("=== Nara Trading System ===\n");

            // //Facade 
            // TradingFacade tradeApp = new TradingFacade(w, m, p, n);

            // tradeApp.ExecuteTrade("NVDA", 10);   // ซื้อหุ้น Nvidia
            // tradeApp.ExecuteTrade("S&P500", 2); // ซื้อกองทุน S&P500
            // tradeApp.ExecuteTrade("GOOGL", 5);  // ซื้อหุ้น Google
            // tradeApp.ExecuteTrade("AMZN", 100); // ลองซื้อเยอะๆ เพื่อดูระบบเช็กเงิน

        }
    }
}