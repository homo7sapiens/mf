using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Commands;

namespace SlothEnterprise.ProductApplication.Utils
{
    /// <summary> Extensions for <see cref="IApplicationResult"/> </summary>
    public static class ApplicationResultExtensions
    {
        /// <summary> Convert IApplicationResult to IApplicationSubmitResult  </summary>
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