using System.Collections.Generic;
using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication.Tests.Stubs
{
    public class ApplicationResultStub: IApplicationResult
    {
        public int? ApplicationId { get; set; }
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }

        public ApplicationResultStub()
        {
        }

        public ApplicationResultStub(int? applicationId, bool success)
        {
            ApplicationId = applicationId;
            Success = success;
        }
    }
}