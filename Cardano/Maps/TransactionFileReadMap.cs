using Cardano.Models;
using CsvHelper.Configuration;

namespace Cardano.Maps
{
    internal class TransactionFileReadMap : ClassMap<Transaction>
    {
        public TransactionFileReadMap()
        {
            Map(m => m.TransactionUti).Name("transaction_uti");
            Map(m => m.Isin).Name("isin");
            Map(m => m.Notional).Name("notional");
            Map(m => m.NotionalCurrency).Name("notional_currency");
            Map(m => m.TransactionType).Name("transaction_type");
            Map(m => m.TransactionDateTime).Name("transaction_datetime");
            Map(m => m.Rate).Name("rate");
            Map(m => m.Lei).Name("lei");
            Map(m => m.LegalName).Ignore();
            Map(m => m.Bic).Ignore();
            Map(m => m.Cost).Ignore();
            Map(m => m.Country).Ignore();
        }
    }
}
