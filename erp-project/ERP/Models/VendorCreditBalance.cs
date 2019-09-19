using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("VendorCreditBalance")]
    public class VendorCreditBalance
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int UserMasId { get; set; }
        public decimal CreditBalance { get; set; }


        public virtual Company Company { get; set; }
        public virtual UserMas UserMas { get; set; }
    }
}