using System.Collections.Generic;
using NUnit.Framework;
using SlothEnterprise.ProductApplication.Commands;
using SlothEnterprise.ProductApplication.Tests.Stubs;

namespace SlothEnterprise.ProductApplication.Tests.CommandHandlers
{
    public abstract class SubmitApplicationsCommandHandlerTestsBase
    {
        protected const int PositiveApplicationId = 999;

        protected static IEnumerable<TestCaseData> ExternalServiceResponses
        {
            get
            {
                yield return new TestCaseData(new ApplicationResultStub(PositiveApplicationId, true), 
                    new ApplicationSubmitResult(true, PositiveApplicationId));
                yield return new TestCaseData(new ApplicationResultStub(PositiveApplicationId, false),
                    new ApplicationSubmitResult(false, PositiveApplicationId));
                yield return new TestCaseData(new ApplicationResultStub(null, true), 
                    new ApplicationSubmitResult(true, null));
                yield return new TestCaseData(new ApplicationResultStub(null, false), 
                    new ApplicationSubmitResult(false, null));
            }
        }
    }
}