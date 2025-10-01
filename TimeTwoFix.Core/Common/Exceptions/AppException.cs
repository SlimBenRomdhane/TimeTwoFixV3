namespace TimeTwoFix.Core.Common.Exceptions
{
    public class AppException : Exception
    {
        public string? ErrorCode { get; set; }
        public string UserFriendlyMessage { get; set; }
        public IDictionary<string, object>? AdditionalData { get; set; }

        public AppException(string message, string userFriendlyMessage, string? errorCode = null, IDictionary<string, object>? additionalData = null) 
            : base(message)
        {
            ErrorCode = errorCode;
            UserFriendlyMessage = userFriendlyMessage;
            AdditionalData = additionalData;
        }

        public AppException(string message, Exception innerException, string userFriendlyMessage, string? errorCode = null) 
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            UserFriendlyMessage = userFriendlyMessage;
        }
    }

    public class EntityNotFoundException : AppException
    {
        public EntityNotFoundException(string entityName, object id)
            : base(
                  $"{entityName} with id {id} was not found",
                  $"The requested {entityName.ToLower()} could not be found",
                  "NOT_FOUND")
        {
        }
    }

    public class ValidationException : AppException
    {
        public ValidationException(string message)
            : base(message, message, "VALIDATION_ERROR")
        {
        }
    }

    public class BusinessRuleException : AppException
    {
        public BusinessRuleException(string message, string userFriendlyMessage)
            : base(message, userFriendlyMessage, "BUSINESS_RULE_VIOLATION")
        {
        }
    }

    public class ConcurrencyException : AppException
    {
        public ConcurrencyException(string entityName)
            : base(
                  $"A concurrency error occurred while updating {entityName}",
                  "The record has been modified by another user. Please refresh and try again.",
                  "CONCURRENCY_ERROR")
        {
        }
    }
}
