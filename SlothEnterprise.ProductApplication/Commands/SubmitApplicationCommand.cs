using MediatR;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Commands
{
    /// <summary> Command to submit application with certain product </summary>
    public class SubmitApplicationCommand<TProduct> : IRequest<IApplicationSubmitResult>
        where TProduct : IProduct
    {
        public ISellerCompanyData CompanyData { get; }
        public TProduct Product { get; }
        
        public SubmitApplicationCommand(ISellerCompanyData companyData, TProduct product)
        {
            CompanyData = companyData;
            Product = product;
        }
    }
}