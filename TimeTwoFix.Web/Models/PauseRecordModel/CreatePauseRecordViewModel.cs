using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Web.Models.PauseRecordModel
{
    public class CreatePauseRecordViewModel
    {
        public int InterventionId { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}