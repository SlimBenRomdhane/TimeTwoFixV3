using System.ComponentModel.DataAnnotations;

namespace TimeTwoFix.Core.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string? Notes { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [MaxLength(50)]
        public string? DeletedBy { get; set; }
    }
}