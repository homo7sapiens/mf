using System.Collections.Generic;
using AutoFixture;
using Moq;
using NUnit.Framework;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Tests.Stubs;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationServiceTests
    {
        private Mock<ISelectInvoiceService> _selectInvoiceServiceMock;
        private Mock<IConfidentialInvoiceService> _confidentialInvoiceWebServiceMock;
        private Mock<IBusinessLoansService> _businessLoansServiceMock;
        private Fixture _fixture;
        
        private ProductApplicationService _sut;
        
        private const int SuccessCode = 200;
        private const int PositiveApplicationId = 999;
        private const int FailCode = -1;

        [SetUp]
        public void Setup()
        {
            _selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
            _confidentialInvoiceWebServiceMock = new Mock<IConfidentialInvoiceService>();
            _businessLoansServiceMock = new Mock<IBusinessLoansService>();

            _fixture = new Fixture();
            
            _sut = new ProductApplicationService(_selectInvoiceServiceMock.Object,
                _confidentialInvoiceWebServiceMock.Object,
                _businessLoansServiceMock.Object);
        }

        [Test]
        public void SubmitApplicationFor_SelectiveInvoiceDiscount_Success()
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

            var result = _sut.SubmitApplicationFor(application);

            Assert.AreEqual(SuccessCode, result);
            
            _selectInvoiceServiceMock.Verify(x => x.SubmitApplicationFor(
                    companyData.Number.ToString(),
                    product.InvoiceAmount,
                    product.AdvancePercentage),
                Times.Once);
            
            _confidentialInvoiceWebServiceMock.Verify(x =>
                    x.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()),
                Times.Never);
            _businessLoansServiceMock.Verify(x =>
                    x.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>()),
                Times.Never);
        }

        [Test, TestCaseSource(nameof(ExternalServiceResponses))]
        public void SubmitApplicationFor_ConfidentialInvoiceDiscount_ExternalSystemCalledOnce(IApplicationResult applicationResult, int expectedResult)
        {
            var companyData = _fixture.Create<SellerCompanyData>();
            var product = _fixture.Create<ConfidentialInvoiceDiscount>();
            var application = new SellerApplication
            {
                CompanyData = companyData,
                Product = product
            };

            _confidentialInvoiceWebServiceMock.Setup(x => x.SubmitApplicationFor(
                It.IsAny<CompanyDataRequest>(),
                product.TotalLedgerNetworth,
                product.AdvancePercentage,
                product.VatRate))
            .Returns(applicationResult);

            var result = _sut.SubmitApplicationFor(application);

            Assert.AreEqual(expectedResult, result);

            _confidentialInvoiceWebServiceMock.Verify(x =>
                    x.SubmitApplicationFor(
                        It.IsAny<CompanyDataRequest>(), 
                        product.TotalLedgerNetworth,
                        product.AdvancePercentage,
                        product.VatRate),
                Times.Once);

            _selectInvoiceServiceMock.Verify(x => x.SubmitApplicationFor(
                    It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()),
                Times.Never);
            _businessLoansServiceMock.Verify(x =>
                    x.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>()),
                Times.Never);
        }
        

        
        [Test, TestCaseSource(nameof(ExternalServiceResponses))]
        public void SubmitApplicationFor_BusinessLoans_ExternalSystemCalledOnce(IApplicationResult applicationResult, int expectedResult)
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

            var result = _sut.SubmitApplicationFor(application);

            Assert.AreEqual(expectedResult, result);
            
            _businessLoansServiceMock.Verify(x =>
                    x.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>()),
                Times.Once);
            
            _selectInvoiceServiceMock.Verify(x => x.SubmitApplicationFor(
                    It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()),
                Times.Never);
            _confidentialInvoiceWebServiceMock.Verify(x =>
                    x.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()),
                Times.Never);
        }
        
        private static IEnumerable<TestCaseData> ExternalServiceResponses
        {
            get
            {
                yield return new TestCaseData(new ApplicationResultStub(PositiveApplicationId, true), PositiveApplicationId);
                yield return new TestCaseData(new ApplicationResultStub(PositiveApplicationId, false), FailCode);
                yield return new TestCaseData(new ApplicationResultStub(null, true), FailCode);
                yield return new TestCaseData(new ApplicationResultStub(null, false), FailCode);
            }
        }
    }
}