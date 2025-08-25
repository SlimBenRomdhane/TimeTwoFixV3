﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTwoFix.Core.Common;
using TimeTwoFix.Core.Entities.WorkOrderManagement;

namespace TimeTwoFix.Core.Entities.ServiceManagement
{
    public class ProvidedService : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public int EstimatedTime { get; set; }

        [Required]
        public decimal PricePerHour { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<Intervention> Interventions { get; set; }
    }
}