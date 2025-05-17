namespace TimeTwoFix.Application.UserServices.Dtos.Roles
{
    public class UpdateRoleDto
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UpdateRoleDto()
        {
            UpdatedAt = DateTime.UtcNow;
        }

    }
}