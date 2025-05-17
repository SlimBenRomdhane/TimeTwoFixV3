using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Core.Entities.UserManagement
{
    public class FrontDeskAssistant : ApplicationUser
    {
        public string? WorkStationNumber { get; set; }
        public string? PhoneExtension { get; set; }

        [MaxLength(50)]
        public string? SpokenLanguage { get; set; }

        public bool BusinessKnowledge { get; set; }
    }
}