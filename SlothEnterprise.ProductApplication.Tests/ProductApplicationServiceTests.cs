using System.Collections.Generic;
using System.Threading;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Commands;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Tests.Stubs;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationServiceTests
    {
        private Mock<IMediator> _mediatorMock;
        private Fixture _fixture;
        
        private ProductApplicationService _sut;
        
        private const int PositiveApplicationId = 999;
        private const int FailCode = -1;
        
        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            
            _fixture = new Fixture();
            
            _sut = new ProductApplicationService(_mediatorMock.Object);
        }

        [Test, TestCaseSource(nameof(ExternalServiceResponses))]
        public void SubmitApplicationFor_Application_CorrectServiceResult(IApplicationSubmitResult applicationResult, int expectedResult)
        {
            var companyData = _fixture.Create<SellerCompanyData>();
            var product = _fixture.Create<ConfidentialInvoiceDiscount>();
            var application = new SellerApplication
            {
                CompanyData = companyData,
                Product = product
            };
        
            _mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<IApplicationSubmitResult>>(), CancellationToken.None))
            .ReturnsAsync(applicationResult);
        
            var result = _sut.SubmitApplicationFor(application);
        
            Assert.AreEqual(expectedResult, result);

            _mediatorMock.Verify(x =>
                    x.Send(It.IsAny<IRequest<IApplicationSubmitResult>>(), CancellationToken.None),
                    Times.Once);
        }

        private static IEnumerable<TestCaseData> ExternalServiceResponses
        {
            get
            {
                yield return new TestCaseData(new ApplicationSubmitResult(true, PositiveApplicationId), PositiveApplicationId);
                yield return new TestCaseData(new ApplicationSubmitResult(false, PositiveApplicationId), FailCode);
                yield return new TestCaseData(new ApplicationSubmitResult(true, null), FailCode);
                yield return new TestCaseData(new ApplicationSubmitResult(false, null), FailCode);
            }
        }
    }
}