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

        //[HttpPost]
        //public void Post(Stock stock)
        //{
        //    using (var context = new StockMarketContext())
        //    {
        //        context.Stocks.Add(stock);
        //        context.SaveChanges();
        //    }
        //}

        [HttpPatch]
        public void Patch(Stock stock)
        {
            using (var context = new StockMarketContext())
            {
                Stock res = context.Stocks.Where(i => i.Name == stock.Name).FirstOrDefault();
                res.Price = stock.Price;
                context.Update(res);
                context.SaveChanges();
            }
        }
    }
}