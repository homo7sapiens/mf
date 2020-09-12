using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Commands;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Utils;

namespace SlothEnterprise.ProductApplication.CommandHandlers
{
    public class SubmitBusinessLoansCommandHandler: 
        IRequestHandler<SubmitApplicationCommand<BusinessLoans>, IApplicationSubmitResult>
    {
        private readonly IBusinessLoansService _businessLoansService;
        
        public SubmitBusinessLoansCommandHandler(IBusinessLoansService businessLoansService)
        {
            _businessLoansService = businessLoansService;
        }

        public Task<IApplicationSubmitResult> Handle(SubmitApplicationCommand<BusinessLoans> request, CancellationToken cancellationToken)
        {
            var companyData = request.CompanyData;
            var product = request.Product;
            
            var result = _businessLoansService.SubmitApplicationFor(
                companyData.ToCompanyDataRequest(), 
                product.ToLoansRequest());

            return Task.FromResult(result.ToApplicationSubmitResult());
        }
    }
}