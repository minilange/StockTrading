using System;
using System.Collections.Generic;

namespace StockTradingBackend.Models
{
    public partial class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Ticker { get; set; } = null!;
        public double Price { get; set; }
        public int Issued { get; set; }
        public int Available { get; set; }
    }
}
