namespace TimeTwoFix.Application.UserServices.Dtos.Roles
{
    public class CreateRoleDto
    {
        public required string RoleName { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public CreateRoleDto()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
