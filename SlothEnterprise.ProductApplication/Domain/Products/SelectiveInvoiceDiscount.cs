using MediatR;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Commands;

namespace SlothEnterprise.ProductApplication.Products
{
    public class SelectiveInvoiceDiscount : IProduct
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Proposed networth of the Invoice
        /// </summary>
        public decimal InvoiceAmount { get; set; }
        /// <summary>
        /// Percentage of the networth agreed and advanced to seller
        /// </summary>
        public decimal AdvancePercentage { get; set; } = 0.80M;
        
        public IRequest<IApplicationSubmitResult> ToSubmitCommand(ISellerCompanyData companyData)
        {
            return new SubmitApplicationCommand<SelectiveInvoiceDiscount>(companyData, this);
        }
    }
}