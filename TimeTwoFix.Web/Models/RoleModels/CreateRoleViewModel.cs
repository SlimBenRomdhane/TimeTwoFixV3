namespace TimeTwoFix.Web.Models.RoleModels
{
    public class CreateRoleViewModel
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}