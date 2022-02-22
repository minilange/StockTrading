using System;
using System.Collections.Generic;

namespace StockTradingBackend.Models
{
    public partial class History
    {
        public int Id { get; set; }
        public string Ticker { get; set; } = null!;
        public double Price { get; set; }
        public string TimeStamp { get; set; } = null!;

        //public History(string ticker, double price )
        //{
        //    this.Ticker = ticker;
        //    this.Price = price;
        //    DateTime time = new DateTime();
        //    this.TimeStamp = time.ToString("yyyy-MM-ddTHH:mm:ss.ff");
        //}
    }
}
