using MediatR;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly IMediator _mediator;

        public ProductApplicationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            var command = application.ToSubmitCommand();
            var result = _mediator.Send(command).GetAwaiter().GetResult();
            
            return (result.IsSuccess) ? result.ApplicationId ?? -1 : -1;
        }
    }
}
