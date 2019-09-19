using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("UserDet")]
    public class UserDet
    {
        [Key]
        public int Id { get; set; }
        //public int VendorRetailerItemsId { get; set; }
        public int LocationId { get; set; }
        public int ItemId { get; set; }
        public int UnitPrice { get; set; }
        //public virtual VendorRetailerItems VendorRetailerItems { get; set; }
        public int UserMasId { get; set; }
        public decimal CarryingCost { get; set; }
        public virtual UserMas UserMas { get; set; }
        public virtual Location Location { get; set; }
        public virtual Item Item { get; set; }
    }
}