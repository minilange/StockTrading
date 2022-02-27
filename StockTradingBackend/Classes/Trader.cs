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
        public string Name { get; set; }
        private int CooldownPeriod { get; set; }
        private List<Stock> TargetedStocks { get; set; }


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

                        operation = "buy";

                        if (targetedStock.StockAmount > 10)
                        {
                            int amount = rnd.Next(1, targetedStock.StockAmount / 10);
                            Console.WriteLine($"Trader: '{Name}' bought {amount} stock");
                            TradeStock(targetedStock, Action.Buy, amount);
                            
                        }
                        else
                        {
                            Console.WriteLine($"Trader: {Name} couldn't buy {targetedStock.Name}, as there are only {targetedStock.StockAmount} available stocks left...");
                        }
                    }
                    else // Sells
                    {
                        operation = "sell";
                        int amount = rnd.Next(1, (targetedStock.StockAmount / 10));

                        if (targetedStock.Price > 0.1 && (targetedStock.StockAmount + amount) <= targetedStock.IssuedStock)
                        {
                            Console.WriteLine($"Trader: '{Name}' sold {amount} stock");
                            TradeStock(targetedStock, Action.Sell, amount);
                        }
                        else
                        {
                            Console.WriteLine($"Trader: {Name} couldn't sell {targetedStock.Name}.");
                        }
                    }

                    LogToDB(targetedStock.Name, operation, Name, oldPrice, targetedStock.Price);

                    CooldownPeriod = rnd.Next(5, 10);
                    Console.WriteLine("--------------------------------------------------------");
                }

                Thread.Sleep(1000);
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

    public enum Action
    {
        Buy,
        Sell
    }
}
