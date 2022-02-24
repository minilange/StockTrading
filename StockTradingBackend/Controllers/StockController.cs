using Microsoft.AspNetCore.Mvc;
using StockTradingBackend.Models;


namespace StockTradingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Stock> Get()
        {
            using (var context = new StockMarketContext())
            {
                return context.Stocks.ToList();
            }
        }


        [HttpPatch]
        public void Patch(string Name, int numTraded, string Operation)
        {
            using (var context = new StockMarketContext())
            {
                Stock res = context.Stocks.Where(i => i.Name == Name).FirstOrDefault();

                double oldPrice = res.Price;
                string ticker = res.Ticker;
                DateTime time = DateTime.Now;
                string timeStamp = time.ToString("yyyy-MM-ddTHH:mm:ss.ff");

                // Adjusting price when user traded
                if (Operation == "buy")
                {
                    // Buys stock
                    res.Price = Math.Round(res.Price * ((double)numTraded / ((double)res.Issued / 2)) + 1.0, 2);
                    res.Available -= numTraded;
                }
                else
                {
                    // Sells stock
                    res.Price = Math.Round(1.0 - (res.Price * ((double)numTraded / (double)res.Issued / 2)), 2);
                    res.Available += numTraded;
                }
                context.Update(res);

                // Update history table
                History hist = new History();
                hist.Ticker = ticker;
                hist.Price = res.Price;
                hist.TimeStamp = timeStamp;
                context.Histories.Add(hist);

                // Logs transaction
                Transaction trans = new Transaction();
                trans.Stock = Name;
                trans.NewPrice = res.Price;
                trans.OldPrice = oldPrice;
                trans.Transactor = "Human One";
                trans.Operation = Operation;
                trans.TimeStamp = timeStamp;
                context.Transactions.Add(trans);

                context.SaveChanges();
            }
        }
    }
}