using Cardano.Helpers;
using Cardano.Models;

namespace Cardano.Services
{
    internal static class EnrichmentService
    {
        public static void EnrichTransactions(IEnumerable<Transaction> transactions, IEnumerable<GleifResponseLeiRecord> leiRecords)
        {
            foreach (var transaction in transactions)
            {
                var leiRecord = leiRecords.FirstOrDefault(x => x.id == transaction.Lei);
                transaction.LegalName = leiRecord?.attributes?.entity?.legalName?.name;
                // Bic is sometimes null
                if (leiRecord.attributes.bic != null)
                {
                    transaction.Bic =string.Join(", ", leiRecord.attributes.bic);
                }
                transaction.Country = leiRecord?.attributes?.entity?.legalAddress?.country;
                transaction.CalculateCost();
            }
        }
    }
}
