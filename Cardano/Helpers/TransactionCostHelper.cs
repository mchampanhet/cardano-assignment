using Cardano.Enums;
using Cardano.Models;

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
                    transaction.Cost = null;
                    break;
            }
        }
    }
}
