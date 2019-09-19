using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("Inventory")]
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public int CompanyId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Company Company { get; set; }
    }
}