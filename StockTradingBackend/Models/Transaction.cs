using System;
using System.Collections.Generic;

namespace StockTradingBackend.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public string Stock { get; set; } = null!;
        public string Operation { get; set; } = null!;
        public string Transactor { get; set; } = null!;
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public string TimeStamp { get; set; } = null!;


        //public Transaction(string stock, string opt, string trans, double oldP, double newP)
        //{
        //    this.Stock = stock;
        //    this.Operation = opt;
        //    this.Transactor = trans;
        //    this.OldPrice = oldP;
        //    this.NewPrice = newP;
        //    DateTime time = new DateTime();
        //    this.TimeStamp = time.ToString("yyyy-MM-ddTHH:mm:ss.ff");
        //}
    }
}
