using StockTradingBackend.Models;

namespace StockTradingBackend.Classes
{
    public class Market
    {
        public delegate void StockDelegate();
        public delegate void TraderDelegate();

        public List<Stock> stocks = new List<Stock>();
        public List<Thread> Traders = new List<Thread>();

        public Market()
        {
            OpenMarket();
        }

        public void OpenMarket()
        {

            StockDelegate stockDel = this.InitializeStocks;
            TraderDelegate traderDel = this.InitializeTraders;

            // Intialize stock(s)
            stockDel.Invoke();

            // Intialize Traders in threads
            traderDel.Invoke();
        }

        private void InitializeStocks()
        {
            using (var context = new StockMarketContext())
            {

                var stc = context.Stocks.ToList();
                foreach (Models.Stock stock in stc)
                {
                    Stock tmpStock = new Stock(stock.Name, stock.Ticker, stock.Price);
                    stocks.Add(tmpStock);
                }
            }
        }

        private void InitializeTraders()
        {
            List<string> traderNames = new List<string> { "Olivia-Grace Hines", "Ryley Archer", "Alan Wong", "Aayush Peters", "Rurai Allan", "Anders Holch Povlsen", 
                                                          "Henrik Andersen", "Ove Lunddal", "Carl Lee Ladefoged", "Martin Lange", "Tobias Jensen", "Warren Buffet" };
            foreach (string traderName in traderNames)
            {
                Trader trader = new Trader(stocks, traderName);
                Thread thread = new Thread(() => trader.InitializeTrader());
                
                if (traderName == "Warren Buffet")
                {
                    thread.Priority = ThreadPriority.Highest;
                }

                thread.Start();
                System.Diagnostics.Trace.WriteLine($"Thread: {thread.Name} was started with Trader: {trader.Name}");
                Thread.Sleep(1000);
            }
        }
    }
}
