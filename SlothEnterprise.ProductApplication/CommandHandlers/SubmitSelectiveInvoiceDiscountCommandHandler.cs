using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Commands;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.CommandHandlers
{
    public class SubmitSelectiveInvoiceDiscountCommandHandler: 
        IRequestHandler<SubmitApplicationCommand<SelectiveInvoiceDiscount>, IApplicationSubmitResult>
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        
        public SubmitSelectiveInvoiceDiscountCommandHandler(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService;
        }

        public Task<IApplicationSubmitResult> Handle(SubmitApplicationCommand<SelectiveInvoiceDiscount> request, CancellationToken cancellationToken)
        {
            var product = request.Product;
            //Assumption that SubmitApplicationFor returns applicationId
            var applicationId = _selectInvoiceService.SubmitApplicationFor(
                request.CompanyData.Number.ToString(), product.InvoiceAmount, product.AdvancePercentage);

            IApplicationSubmitResult submitResult = new ApplicationSubmitResult(true, applicationId);
            return Task.FromResult(submitResult);
        }
    }
}