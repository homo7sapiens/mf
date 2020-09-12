using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Commands;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Utils;

namespace SlothEnterprise.ProductApplication.CommandHandlers
{
    /// <summary> Submits ConfidentialInvoiceDiscount to confidential invoice microservice </summary>
    public class SubmitConfidentialInvoiceDiscountCommandHandler: 
        IRequestHandler<SubmitApplicationCommand<ConfidentialInvoiceDiscount>, IApplicationSubmitResult>
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceService;
        
        public SubmitConfidentialInvoiceDiscountCommandHandler(IConfidentialInvoiceService confidentialInvoiceService)
        {
            _confidentialInvoiceService = confidentialInvoiceService;
        }

        public Task<IApplicationSubmitResult> Handle(SubmitApplicationCommand<ConfidentialInvoiceDiscount> request, CancellationToken cancellationToken)
        {
            var companyData = request.CompanyData;
            var product = request.Product;

            var result = _confidentialInvoiceService.SubmitApplicationFor(
                companyData.ToCompanyDataRequest(), 
                product.TotalLedgerNetworth, 
                product.AdvancePercentage,
                product.VatRate);

            return Task.FromResult(result.ToApplicationSubmitResult());
        }
    }
}