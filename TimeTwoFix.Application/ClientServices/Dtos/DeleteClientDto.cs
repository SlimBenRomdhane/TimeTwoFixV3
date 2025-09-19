namespace TimeTwoFix.Application.ClientServices.Dtos
{
    public class DeleteClientDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
    }
}