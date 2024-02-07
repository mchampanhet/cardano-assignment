using Cardano.Helpers;
using Cardano.Models;

namespace Cardano.Service
{
    internal class EnrichmentService
    {
        public void EnrichTransactions(IEnumerable<Transaction> transactions, IEnumerable<GleifResponseLeiRecord> leiRecords)
        {
            foreach (var transaction in transactions)
            {
                var leiRecord = leiRecords.FirstOrDefault(x => x.id == transaction.Lei);
                transaction.LegalName = leiRecord.attributes.entity.legalName.name;
                transaction.Bic = string.Join(", ", leiRecord.attributes.bic ?? new List<string>());
                transaction.Country = leiRecord.attributes.entity.legalAddress.country;
                transaction.CalculateCost();
            }
        }
    }
}
