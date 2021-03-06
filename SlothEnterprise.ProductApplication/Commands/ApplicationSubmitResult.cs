﻿namespace SlothEnterprise.ProductApplication.Commands
{
    /// <summary> Application submit result </summary>
    public interface IApplicationSubmitResult
    {
        bool IsSuccess { get; }
        int? ApplicationId { get; }
    }
    
    public class ApplicationSubmitResult : IApplicationSubmitResult
    {
        public bool IsSuccess { get; }
        public int? ApplicationId { get; }

        public ApplicationSubmitResult(bool isSuccess, int? applicationId = null)
        {
            IsSuccess = isSuccess;
            ApplicationId = applicationId;
        }
    }
}