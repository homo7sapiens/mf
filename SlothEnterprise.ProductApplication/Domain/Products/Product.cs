using MediatR;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Commands;

namespace SlothEnterprise.ProductApplication.Products
{
    public interface IProduct
    {
        int Id { get; }

        IRequest<IApplicationSubmitResult> ToSubmitCommand(ISellerCompanyData companyData);
    }
}
