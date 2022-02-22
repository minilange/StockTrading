using Microsoft.AspNetCore.Mvc;
using StockTradingBackend.Models;


namespace StockTradingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            using (var context = new StockMarketContext())
            {
                return context.Transactions.ToList().OrderBy(i => i.TimeStamp).Take(20);
            }
        }

        [HttpPost]
        public void Post(Transaction input)
        {
            using (var context = new StockMarketContext())
            {
                context.Transactions.Add(input);
                context.SaveChanges();
                //return context.Transactions.ToList();
            }
        }
    }
}