using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Utils
{
    public static class BusinessLoansExtensions
    {
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