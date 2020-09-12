using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Commands;

namespace SlothEnterprise.ProductApplication.Utils
{
    public static class ApplicationResultExtensions
    {
        public static IApplicationSubmitResult ToApplicationSubmitResult(this IApplicationResult result)
        {
            if (result == null)
            {
                return null;
            }

            return new ApplicationSubmitResult(result.Success, result.ApplicationId);
        }
    }
}