using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Utils
{
    /// <summary>  Extensions for <see cref="ISellerCompanyData"/> </summary>
    public static class SellerCompanyDataExtensions
    {
        /// <summary> Converts ISellerCompanyData to CompanyDataRequest </summary>
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