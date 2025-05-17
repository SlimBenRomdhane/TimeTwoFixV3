namespace TimeTwoFix.Application.UserServices.Dtos.Users
{
    public class UserFilterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateOnly? HireDate { get; set; }

    }
}