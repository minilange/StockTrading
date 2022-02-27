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
    }
}
