using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using NUnit.Framework;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.CommandHandlers;
using SlothEnterprise.ProductApplication.Commands;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Tests.CommandHandlers
{
    public class SubmitSelectiveInvoiceDiscountCommandHandlerTests:SubmitApplicationsCommandHandlerTestsBase
    {
        private Mock<ISelectInvoiceService> _selectInvoiceServiceMock;
        private Fixture _fixture;
        
        private SubmitSelectiveInvoiceDiscountCommandHandler _sut;
        private const int SuccessCode = 200;

        [SetUp]
        public void Setup()
        {
            _selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
            _fixture = new Fixture();

            _sut = new SubmitSelectiveInvoiceDiscountCommandHandler(_selectInvoiceServiceMock.Object);
        }
        
        [Test]
        public async Task Handle_SubmitSelectiveInvoiceDiscountCommand_Success()
        {
            var companyData = _fixture.Create<SellerCompanyData>();
            var product = _fixture.Create<SelectiveInvoiceDiscount>();
            var application = new SellerApplication
            {
                CompanyData = companyData,
                Product = product
            };
            
            _selectInvoiceServiceMock.Setup(x => x.SubmitApplicationFor(
                    companyData.Number.ToString(),
                    product.InvoiceAmount,
                    product.AdvancePercentage))
                .Returns(SuccessCode);
            
            var result = await _sut.Handle((SubmitApplicationCommand<SelectiveInvoiceDiscount>)application.ToSubmitCommand(), CancellationToken.None);

            Assert.AreEqual(true, result.IsSuccess);
            Assert.AreEqual(SuccessCode, result.ApplicationId);
            
            _selectInvoiceServiceMock.Verify(x => x.SubmitApplicationFor(
                    companyData.Number.ToString(),
                    product.InvoiceAmount,
                    product.AdvancePercentage),
                Times.Once);
        }
    }
}