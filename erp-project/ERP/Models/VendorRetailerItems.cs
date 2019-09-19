using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("VendorRetailerItems")]
    public class VendorRetailerItems
    {
        [Key]
        public int Id { get; set; }
        public int UserMasId { get; set; }
        public int ItemId { get; set; }
        public decimal ItemCost { get; set; }
        public virtual Item Item { get; set; }
        public virtual UserMas UserMas { get; set; }
    }
}