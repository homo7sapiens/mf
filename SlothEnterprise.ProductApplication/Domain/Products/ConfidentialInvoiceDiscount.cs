using MediatR;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Commands;

namespace SlothEnterprise.ProductApplication.Products
{
    public class ConfidentialInvoiceDiscount : IProduct
    {
        public int Id { get; set; }
        public decimal TotalLedgerNetworth { get; set; }
        public decimal AdvancePercentage { get; set; }
        public decimal VatRate { get; set; } = VatRates.UkVatRate;
        
        public IRequest<IApplicationSubmitResult> ToSubmitCommand(ISellerCompanyData companyData)
        {
            return new SubmitApplicationCommand<ConfidentialInvoiceDiscount>(companyData, this);
        }
    }
}