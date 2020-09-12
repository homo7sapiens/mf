using AutoFixture;
using NUnit.Framework;
using SlothEnterprise.ProductApplication.Commands;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Utils;

namespace SlothEnterprise.ProductApplication.Tests.Utils
{
    public class BusinessLoansExtensionsTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }
        
        [Test]
        public void ToLoansRequest_Null_DoesntNotThrow()
        {
            var result = ((BusinessLoans)null).ToLoansRequest();

            Assert.IsNull(result);
        }
        
        [Test]
        public void ToLoansRequest_BusinessLoan_AllFieldsMapped()
        {
            var businessLoan = _fixture.Create<BusinessLoans>();

            var result = businessLoan.ToLoansRequest();

            Assert.AreEqual(businessLoan.LoanAmount, result.LoanAmount);
            Assert.AreEqual(businessLoan.InterestRatePerAnnum, result.InterestRatePerAnnum);
        }
    }
}