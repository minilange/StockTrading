namespace StockTradingBackend.Models
{
    public class Trade
    {
        public string Ticker { get; set; }
        public int NumTraded { get; set; }
        public string Operation { get; set; }
    }

}
