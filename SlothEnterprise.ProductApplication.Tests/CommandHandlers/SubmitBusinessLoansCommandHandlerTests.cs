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
    public class SubmitBusinessLoansCommandHandlerTests:SubmitApplicationsCommandHandlerTestsBase
    {
        private Mock<IBusinessLoansService> _businessLoansServiceMock;
        private Fixture _fixture;
        
        private SubmitBusinessLoansCommandHandler _sut;

        [SetUp]
        public void Setup()
        {
            _businessLoansServiceMock = new Mock<IBusinessLoansService>();
            _fixture = new Fixture();

            _sut = new SubmitBusinessLoansCommandHandler(_businessLoansServiceMock.Object);
        }
        
        [Test, TestCaseSource(nameof(ExternalServiceResponses))]
        public async Task Handle_SubmitBusinessLoansCommand_Success(
            IApplicationResult applicationResult, IApplicationSubmitResult expectedResult)
        {
            var companyData = _fixture.Create<SellerCompanyData>();
            var product = _fixture.Create<BusinessLoans>();
            var application = new SellerApplication
            {
                CompanyData = companyData,
                Product = product
            };

            _businessLoansServiceMock.Setup(x => x.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequest>(),
                    It.IsAny<LoansRequest>()))
                .Returns(applicationResult);

            var result = await _sut.Handle((SubmitApplicationCommand<BusinessLoans>)application.ToSubmitCommand(), CancellationToken.None);

            Assert.AreEqual(expectedResult.IsSuccess, result.IsSuccess);
            Assert.AreEqual(expectedResult.ApplicationId, result.ApplicationId);
            
            _businessLoansServiceMock.Verify(x =>
                    x.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>()),
                Times.Once);
        }
    }
}