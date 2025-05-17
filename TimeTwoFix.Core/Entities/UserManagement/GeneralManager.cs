namespace TimeTwoFix.Core.Entities.UserManagement
{
    public class GeneralManager : ApplicationUser
    {
        public string? OfficeNumber { get; set; }
        public int YearsInManagement { get; set; }
    }
}