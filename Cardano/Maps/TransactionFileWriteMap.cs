using Cardano.Models;
using CsvHelper.Configuration;

namespace Cardano.Maps
{
    internal class TransactionFileWriteMap : ClassMap<Transaction>
    {
        public TransactionFileWriteMap()
        {
            Map(m => m.TransactionUti).Name("transaction_uti").Index(0);
            Map(m => m.Isin).Name("isin").Index(1);
            Map(m => m.Notional).Name("notional").Index(2);
            Map(m => m.NotionalCurrency).Name("notional_currency").Index(3);
            Map(m => m.TransactionType).Name("transaction_type").Index(4);
            Map(m => m.TransactionDateTime).Name("transaction_datetime").Index(5);
            Map(m => m.Rate).Name("rate").Index(6);
            Map(m => m.Lei).Name("lei").Index(7);
            Map(m => m.LegalName).Name("legal_name").Index(8);
            Map(m => m.Bic).Name("bic").Index(9);
            Map(m => m.Cost).Name("transaction_costs").Index(10);
            Map(m => m.Country).Ignore();
        }
    }
}
