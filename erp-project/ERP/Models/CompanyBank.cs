using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("CompanyBank")]
    public class CompanyBank
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int BankId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only Alphabets and Space allowed.")]
        [Display(Name = "Account Name")]
        [StringLength(500)]
        public string AccountName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Account No")]
        [StringLength(500)]
        public string AccountNo { get; set; }
        [StringLength(200)]
        public string Branch { get; set; }
        [StringLength(400)]
        public string SwiftCode { get; set; }
        [StringLength(200)]
        public string RoutingNo { get; set; }
        [StringLength(200)]
        public string BranchCode { get; set; }
        [StringLength(1000)]
        public string Address { get; set; }
        public decimal InitialBalance { get; set; }

        public virtual Company Company { get; set; }
        public virtual Bank Bank { get; set; }

    }
}