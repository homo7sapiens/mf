using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Utils
{
    /// <summary> Extensions for <see cref="BusinessLoans"/> </summary>
    public static class BusinessLoansExtensions
    {
        /// <summary> Converts BusinessLoans to LoansRequest </summary>
        public static LoansRequest ToLoansRequest(this BusinessLoans businessLoans)
        {
            if (businessLoans == null)
            {
                return null;
            }
            
            return new LoansRequest
            {
                InterestRatePerAnnum = businessLoans.InterestRatePerAnnum,
                LoanAmount = businessLoans.LoanAmount
            };
        }
    }
}