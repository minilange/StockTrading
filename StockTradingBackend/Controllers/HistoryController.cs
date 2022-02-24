using Microsoft.AspNetCore.Mvc;
using StockTradingBackend.Models;


namespace StockTradingBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<History> Get(string ticker)
        {
            using (var context = new StockMarketContext())
            {
                return context.Histories.Where(hist => hist.Ticker == ticker).ToList();
            }
        }
    }
}