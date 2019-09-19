using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("VendorBankAccount")]
    public class VendorBankAccount
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int BankId { get; set; }
        [StringLength(500)]
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual Bank Bank{ get; set; }
    }
}