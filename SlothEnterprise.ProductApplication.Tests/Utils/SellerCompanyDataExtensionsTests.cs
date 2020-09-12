using AutoFixture;
using NUnit.Framework;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Utils;

namespace SlothEnterprise.ProductApplication.Tests.Utils
{
    public class SellerCompanyDataExtensionsTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }
        
        [Test]
        public void ToCompanyDataRequest_Null_DoesntNotThrow()
        {
            var result = ((SellerCompanyData)null).ToCompanyDataRequest();

            Assert.IsNull(result);
        }
        
        [Test]
        public void ToLoansRequest_SellerCompanyData_AllFieldsMapped()
        {
            var companyData = _fixture.Create<SellerCompanyData>();

            var result = companyData.ToCompanyDataRequest();

            Assert.AreEqual(companyData.Founded, result.CompanyFounded);
            Assert.AreEqual(companyData.Name, result.CompanyName);
            Assert.AreEqual(companyData.Number, result.CompanyNumber);
            Assert.AreEqual(companyData.DirectorName, result.DirectorName);
        }
    }
}