using AutoFixture;
using NUnit.Framework;
using SlothEnterprise.ProductApplication.Tests.Stubs;
using SlothEnterprise.ProductApplication.Utils;

namespace SlothEnterprise.ProductApplication.Tests.Utils
{
    public class ApplicationResultExtensionsTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }
        
        [Test]
        public void ToApplicationSubmitResult_Null_DoesNotThrow()
        {
            var result = ((ApplicationResultStub)null).ToApplicationSubmitResult();

            Assert.IsNull(result);
        }
        
        [Test]
        public void ToApplicationSubmitResult_ApplicationResult_AllFieldsMapped()
        {
            var applicationResult = _fixture.Create<ApplicationResultStub>();

            var result = applicationResult.ToApplicationSubmitResult();

            Assert.AreEqual(applicationResult.ApplicationId, result.ApplicationId);
            Assert.AreEqual(applicationResult.Success, result.IsSuccess);
        }
    }
}