namespace TimeTwoFix.Web.Models.ErrorModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string UserFriendlyMessage { get; set; }
        public string ErrorCode { get; set; }
        public string Title { get; set; }
        public bool ShowHomeLink { get; set; } = true;
        public string ReturnUrl { get; set; }
        public string ReturnLinkText { get; set; }
        public Dictionary<string, string[]> ValidationErrors { get; set; }
        
        public ErrorViewModel()
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        public static ErrorViewModel Create404(string entityName = null)
        {
            return new ErrorViewModel
            {
                Title = "Not Found",
                ErrorCode = "404",
                UserFriendlyMessage = entityName != null 
                    ? $"The requested {entityName.ToLower()} could not be found."
                    : "The requested resource could not be found.",
                ShowHomeLink = true
            };
        }

        public static ErrorViewModel Create500()
        {
            return new ErrorViewModel
            {
                Title = "Error",
                ErrorCode = "500",
                UserFriendlyMessage = "An unexpected error occurred. Our technical team has been notified.",
                ShowHomeLink = true
            };
        }

        public static ErrorViewModel Create403()
        {
            return new ErrorViewModel
            {
                Title = "Access Denied",
                ErrorCode = "403",
                UserFriendlyMessage = "You don't have permission to access this resource.",
                ShowHomeLink = true
            };
        }

        public static ErrorViewModel CreateValidationError(Dictionary<string, string[]> validationErrors)
        {
            return new ErrorViewModel
            {
                Title = "Validation Error",
                ErrorCode = "400",
                UserFriendlyMessage = "Please correct the following errors:",
                ValidationErrors = validationErrors,
                ShowHomeLink = false
            };
        }

        public static ErrorViewModel CreateBusinessError(string message, string returnUrl = null, string returnLinkText = null)
        {
            return new ErrorViewModel
            {
                Title = "Cannot Complete Operation",
                ErrorCode = "BR400",
                UserFriendlyMessage = message,
                ShowHomeLink = string.IsNullOrEmpty(returnUrl),
                ReturnUrl = returnUrl,
                ReturnLinkText = returnLinkText ?? "Go Back"
            };
        }
    }
}
