using Cardano.Enums;

namespace Cardano.Models
{
    internal class Transaction
    {
        public string TransactionUti { get; set; }
        public string Isin { get; set; }
        public int Notional { get; set; }
        public CurrencyEnum NotionalCurrency { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public float Rate { get; set; }
        public string Lei { get; set; }
        public string LegalName { get; set; }
        public string Bic { get; set; }
        public double? Cost { get; set; }
        public CountryEnum? Country { get; set; }
    }
}
