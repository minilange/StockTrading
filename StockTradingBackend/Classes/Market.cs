using StockTradingBackend.Models;

namespace StockTradingBackend.Classes
{
    public class Market
    {
        //public int MarketState { get; set; };
        public List<Stock> stocks = new List<Stock>();
        public List<Thread> Traders = new List<Thread>();


        public Market()
        {
            OpenMarket();
        }

        public void OpenMarket()
        {
            // Intialize stock(s)
            InitializeStocks();

            // Intialize Traders in threads
            InitializeTraders();

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
            List<string> traderNames = new List<string> { "Olivia-Grace Hines", "Ryley Archer", "Alan Wong", "Aayush Peters", "Rurai Allan", "Anders Holch Povlsen", "Henrik Andersen", "Ove Lunddal", "Carl Lee Ladefoged", "Martin Lange", "Tobias Jonsen"};
            foreach (string traderName in traderNames)
            {
                Trader trader = new Trader(stocks, traderName);
                Thread thread = new Thread(() => trader.InitializeTrader());
                thread.Start();
                Thread.Sleep(1000);
            }
        }
    }
}
