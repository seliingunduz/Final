using Newtonsoft.Json;

namespace Final.model
{
    public class Root
    {
        [JsonProperty("USD")]
        public Currency USD { get; set; }

        [JsonProperty("EUR")]
        public Currency EUR { get; set; }

        [JsonProperty("GBP")]
        public Currency GBP { get; set; }

        [JsonProperty("gram-altin")]
        public Currency GramAltin { get; set; }

        [JsonProperty("ceyrek-altin")]
        public Currency CeyrekAltin { get; set; }

        [JsonProperty("tam-altin")]
        public Currency TamAltin { get; set; }

        [JsonProperty("gumus")]
        public Currency Gumus { get; set; }

        [JsonProperty("BTC")]
        public Currency BTC { get; set; }

        [JsonProperty("ETH")]
        public Currency ETH { get; set; }

        // Daha fazla döviz veya değer eklemek için buraya JsonProperty eklenebilir
    }

    public class Currency
    {
        [JsonProperty("Al")]
        public decimal? Alis { get; set; }

        [JsonProperty("Sat")]
        public decimal? Satis { get; set; }

        [JsonProperty("Deiim")]
        public string Degisim { get; set; }
    }
}
