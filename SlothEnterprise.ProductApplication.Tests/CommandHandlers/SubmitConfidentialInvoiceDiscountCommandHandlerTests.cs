using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using NUnit.Framework;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.CommandHandlers;
using SlothEnterprise.ProductApplication.Commands;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Tests.CommandHandlers
{
    public class SubmitConfidentialInvoiceDiscountCommandHandlerTests:SubmitApplicationsCommandHandlerTestsBase
    {
        private Mock<IConfidentialInvoiceService> _confidentialInvoiceServiceMock;
        private Fixture _fixture;
        
        private SubmitConfidentialInvoiceDiscountCommandHandler _sut;

        [SetUp]
        public void Setup()
        {
            _confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();
            _fixture = new Fixture();

            _sut = new SubmitConfidentialInvoiceDiscountCommandHandler(_confidentialInvoiceServiceMock.Object);
        }
        
        [Test, TestCaseSource(nameof(ExternalServiceResponses))]
        public async Task Handle_SubmitConfidentialInvoiceDiscountCommand_Success(
            IApplicationResult applicationResult, IApplicationSubmitResult expectedResult)
        {
            var companyData = _fixture.Create<SellerCompanyData>();
            var product = _fixture.Create<ConfidentialInvoiceDiscount>();
            var application = new SellerApplication
            {
                CompanyData = companyData,
                Product = product
            };
            
            _confidentialInvoiceServiceMock.Setup(x => x.SubmitApplicationFor(
                It.IsAny<CompanyDataRequest>(),
                product.TotalLedgerNetworth,
                product.AdvancePercentage,
                product.VatRate))
            .Returns(applicationResult);
            
            var result = await _sut.Handle((SubmitApplicationCommand<ConfidentialInvoiceDiscount>)application.ToSubmitCommand(), CancellationToken.None);

            Assert.AreEqual(expectedResult.IsSuccess, result.IsSuccess);
            Assert.AreEqual(expectedResult.ApplicationId, result.ApplicationId);
            
            _confidentialInvoiceServiceMock.Verify(x =>
                    x.SubmitApplicationFor(
                        It.IsAny<CompanyDataRequest>(), 
                        product.TotalLedgerNetworth,
                        product.AdvancePercentage,
                        product.VatRate),
                Times.Once);
        }
    }
}