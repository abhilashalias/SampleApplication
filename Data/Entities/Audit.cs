using System;
using System.ComponentModel.DataAnnotations;
namespace SampleApplication.Data.Entities
{
    public class Audit
    {
        [Key]
        public int AuditId{get;set;}

        [Required]
        [StringLength(256)]
        public string Particulars{get; set;}

        [Required]
        [StringLength(256)]
        public string Accountability{get; set;}

        [Required]
        public string Capex{get; set;}

        [Required]
        public string Priority{get; set;}

        [StringLength(1000)]
        public string Comments{get; set;}

        [Required]
        public string Status{get; set;}
    }
}