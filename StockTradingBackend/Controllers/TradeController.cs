using Microsoft.AspNetCore.Mvc;
using StockTradingBackend.Models;
using System.Net;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockTradingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        [HttpPut("{ticker}")]
        public HttpResponseMessage Put(string ticker, [FromBody] object JSONval)
        {
            try
            {
                using (var context = new StockMarketContext())
                {
                    Trade? val = JsonSerializer.Deserialize<Trade>(JSONval.ToString());

                    if (val == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                    }

                    Stock res = context.Stocks.Where(i => i.Ticker == val.Ticker).FirstOrDefault();

                    if (res == null || res.Ticker != val.Ticker)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    string name = res.Name;

                    double oldPrice = res.Price;
                    DateTime time = DateTime.Now;
                    string timeStamp = time.ToString("yyyy-MM-ddTHH:mm:ss.ff");

                    // Adjusting price when user traded
                    if (val.Operation == "buy")
                    {
                        // Buys stock
                        res.Price = Math.Round(res.Price * ((double)val.NumTraded / ((double)res.Issued / 2)) + 1.0, 2);
                        res.Available -= val.NumTraded;
                    }
                    else
                    {
                        // Sells stock
                        res.Price = Math.Round(1.0 - (res.Price * ((double)val.NumTraded / (double)res.Issued / 2)), 2);
                        res.Available += val.NumTraded;
                    }
                    context.Update(res);

                    // Update history table
                    History hist = new History();
                    hist.Ticker = val.Ticker;
                    hist.Price = res.Price;
                    hist.TimeStamp = timeStamp;
                    context.Histories.Add(hist);

                    // Logs transaction
                    Transaction trans = new Transaction();
                    trans.Stock = name;
                    trans.NewPrice = res.Price;
                    trans.OldPrice = oldPrice;
                    trans.Transactor = "Human One";
                    trans.Operation = val.Operation;
                    trans.TimeStamp = timeStamp;
                    context.Transactions.Add(trans);

                    context.SaveChanges();
                    Console.WriteLine("Successfully recieved and handled PUT request");
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
