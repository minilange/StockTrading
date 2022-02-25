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
                return context.Transactions.ToList().OrderByDescending(i => i.Id).Take(50);
            }
        }
    }
}