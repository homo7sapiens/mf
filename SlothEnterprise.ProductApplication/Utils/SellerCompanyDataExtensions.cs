using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Utils
{
    public static class SellerCompanyDataExtensions
    {
        public static CompanyDataRequest ToCompanyDataRequest(this ISellerCompanyData companyData)
        {
            if (companyData == null)
            {
                return null;
            }

            return new CompanyDataRequest
            {
                CompanyFounded = companyData.Founded,
                CompanyNumber = companyData.Number,
                CompanyName = companyData.Name,
                DirectorName = companyData.DirectorName
            };
        }
    }
}