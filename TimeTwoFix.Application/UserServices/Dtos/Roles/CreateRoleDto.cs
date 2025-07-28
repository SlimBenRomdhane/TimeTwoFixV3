namespace TimeTwoFix.Application.UserServices.Dtos.Roles
{
    public class CreateRoleDto
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public CreateRoleDto()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}