using StockTradingBackend.Models;

namespace StockTradingBackend.Classes
  
{
    public abstract class MarketParticipant
    {
        public void TradeStock(Stock targetedStock, Action action, int orderVolume)
        {
            /* Lock stock */
            lock (targetedStock)
            {
                // Adjust price
                if (action == Action.Buy)
                {
                    // Add
                    targetedStock.BuyItem(orderVolume);
                }
                else if (action == Action.Sell)
                {
                    // Deduct
                    targetedStock.SellItem(orderVolume);
                }
            }
        }
    }

    public class Trader : MarketParticipant
    {
        // Cooldown in seconds
        public string Name { get; set; }
        private double CooldownPeriod { get; set; }
        private List<Stock> TargetedStocks { get; set; }
        // Holdings of stock?


        public Trader(List<Stock> target, string name)
        {
            TargetedStocks = target;
            Name = name;
        }

        public void InitializeTrader()
        {
            Random rnd = new Random();
            while (true)
            {
                if (CooldownPeriod > 0)
                {
                    CooldownPeriod -= 1;
                }
                else
                {
                    int action = rnd.Next(0, 2);
                    Stock targetedStock = TargetedStocks[rnd.Next(0, TargetedStocks.Count)];
                    
                    double oldPrice = targetedStock.Price;
                    string operation;
                    if (action == 0) // Buys
                    {
                        int amount = rnd.Next(1, targetedStock.StockAmount / 10);
                        Console.WriteLine($"Trader: '{Name}' bought {amount} stock");
                        TradeStock(targetedStock, Action.Buy, amount);
                        operation = "buy";
                        
                    }
                    else // Sells
                    {
                        int amount = rnd.Next(1, (targetedStock.StockAmount / 10));
                        Console.WriteLine($"Trader: '{Name}' sold {amount} stock");
                        TradeStock(targetedStock, Action.Sell, amount);
                        operation = "sell";
                    }

                    LogToDB(targetedStock.Name, operation, Name, oldPrice, targetedStock.Price);


                    CooldownPeriod = rnd.Next(2, 8);
                    Console.WriteLine("--------------------------------------------------------");
                }

                //Thread.Sleep(rnd.Next(450, 555));
                
                Thread.Sleep(5000);
            }
        }

        public void LogToDB(string stock, string opt, string trans, double oldP, double newP)
        {
            using (var context = new StockMarketContext())
            {
                Transaction trnac = new Transaction();
                trnac.Stock = stock;
                trnac.NewPrice = newP;
                trnac.OldPrice = oldP;
                trnac.Transactor = trans;
                trnac.Operation = opt;
                DateTime time = DateTime.Now;
                trnac.TimeStamp = time.ToString("yyyy-MM-ddTHH:mm:ss.ff");


                context.Transactions.Add(trnac);
                context.SaveChanges();
            }
        }
    }


    public class Fund : MarketParticipant
    {
        // Fund with larger amounts of money, but fewer transactions

    }

    public enum Action
    {
        Buy,
        Sell
    }
}
