using MediatR;
using SlothEnterprise.ProductApplication.Commands;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Applications
{
    public interface ISellerApplication
    {
        IProduct Product { get; set; }
        ISellerCompanyData CompanyData { get; set; }
        
        IRequest<IApplicationSubmitResult> ToSubmitCommand();
    }

    public class SellerApplication : ISellerApplication
    {
        public IProduct Product { get; set; }
        public ISellerCompanyData CompanyData { get; set; }
        public IRequest<IApplicationSubmitResult> ToSubmitCommand()
        {
            return Product.ToSubmitCommand(CompanyData);
        }
    }
}