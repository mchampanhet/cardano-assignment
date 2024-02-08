using Cardano.Enums;
using Cardano.Models;
using Cardano.Services;

namespace Cardano.Helpers
{
    internal static class TransactionCostHelper
    {
        public static void CalculateCost(this Transaction transaction)
        {
            switch(transaction.Country)
            {
                case CountryEnum.GB:
                    transaction.Cost = transaction.Notional * transaction.Rate - transaction.Notional;
                    break;
                case CountryEnum.NL:
                    transaction.Cost = Math.Abs(transaction.Notional / transaction.Rate - transaction.Notional);
                    break;
                default:
                    // we could throw an error, but it seems like we could just
                    // log a warning, not calculate the cost, and move on with the rest of the process
                    LoggerService.LogWarning($"Cost calculcations not setup for country '{transaction.Country}'");
                    transaction.Cost = null;
                    break;
            }
        }
    }
}
