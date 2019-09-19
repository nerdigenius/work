using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("VendorCreditAdd")]
    public class VendorCreditAdd
    {
        [Key]
        public int Id { get; set; }
        public int UserMasId { get; set; }
        public decimal CreditAmount { get; set; }

        public virtual UserMas UserMas { get; set; }
    }
}