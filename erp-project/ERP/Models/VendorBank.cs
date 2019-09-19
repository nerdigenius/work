using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("VendorBank")]
    public class VendorBank
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BankId { get; set; }
        [Required]
        public int UserMasId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only Alphabets and Space allowed.")]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public string SwiftCode { get; set; }
        public string RoutingNo { get; set; }
        public string BranchCode { get; set; }
        public string Address { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual UserMas UserMas { get; set; }
    }
}