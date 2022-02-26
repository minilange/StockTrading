using StockTradingBackend.Models;
using StockTradingBackend.Controllers;

namespace StockTradingBackend.Classes
{
    public abstract class MarketItem
    {
        protected string name;
        protected double price;
        protected abstract void IncreasePrice(double amount);
        protected abstract void DecreasePrice(double amount);
        public abstract void BuyItem(int amount);
        public abstract void SellItem(int amount);
    }

    public class Stock : MarketItem
    {
        private static int publicAvailableStock;
        private int stockAmount;

        public string Name { get { return name; } }
        public int StockAmount { get { return stockAmount; } } 
        public int IssuedStock { get { return publicAvailableStock; } }
        public double Price { get { return price; } }
        public string TickerSymbol { get; }
        
        public Stock(string stockName, string ticker, double latestPrice)
        {
            name = stockName;
            TickerSymbol = ticker;
            price = latestPrice;

            // Update from database when instantiating
            using (var context = new StockMarketContext())
            {
                var res = context.Stocks.Where(i => i.Name == name).FirstOrDefault();
                publicAvailableStock = res.Issued;
                stockAmount = res.Available;
            }
        }

        public override async void BuyItem(int amount)
        {
            Task<bool> haveUpdated = ReadFromDB();
            await haveUpdated;

            stockAmount -= amount;
            IncreasePrice(((double)amount / (double)publicAvailableStock / 2) + 1.0);

            Console.WriteLine($"'{this.name}' has been bought, price is now ${Price} and {StockAmount} is left");
        }

        public override async void SellItem(int amount)
        {
            Task<bool> haveUpdated = ReadFromDB();
            await haveUpdated;

            stockAmount += amount;
            DecreasePrice(1.0 - ((double)amount / (double)publicAvailableStock / 4));

            Console.WriteLine($"'{this.name}' has been sold, price is now ${Price} and {StockAmount} is left");
        }

        protected override void IncreasePrice(double amount)
        {
            // amount is here the decimal number which represents the percentage change in the price (e.g. 1.02, representing a 2% increase in price)
            Console.WriteLine($"'{Name}' increased by +{Math.Round((price * amount) - price, 2)} ({Math.Round(amount, 2)}%)");
            price = Math.Round(price * amount, 2);

            LogToDB(name, TickerSymbol, price);
        }
        protected override void DecreasePrice(double amount)
        {
            Console.WriteLine($"'{Name}' decreased by {Math.Round((price * amount) - price, 2)} ({Math.Round(amount,2)}%)");
            price = Math.Round(price * amount, 2);

            LogToDB(name, TickerSymbol, price);
        }


        async Task<bool> ReadFromDB()
        {
            using (var context = new StockMarketContext())
            {
                var res = context.Stocks.Where(i => i.Name == name).FirstOrDefault();
                this.price = res.Price;
                this.stockAmount = res.Available;
                return true;
            }
        }

        protected void LogToDB(string name, string ticker, double price)
        {
            using (var context = new StockMarketContext())
            {
                // Update stock table with stock amount available and price
                var res = context.Stocks.Where(i => i.Name == name).FirstOrDefault();
                res.Available = StockAmount;
                res.Price = price;
                context.Update(res);

                // Insert history entry for the ticker, with timestamp and price
                History hist = new History();
                hist.Ticker = ticker;
                hist.Price = price;
                DateTime time = DateTime.Now;
                hist.TimeStamp = time.ToString("yyyy-MM-ddTHH:mm:ss.ff");
                context.Histories.Add(hist);
                context.SaveChanges();
            }
        }
    }
}