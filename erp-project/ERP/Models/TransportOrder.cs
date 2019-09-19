using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("TransportOrder")]
    public class TransportOrder
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int UserMasId { get; set; }
        public int? AgreementsId { get; set; }
        public int LocationId { get; set; }
        public int ItemId { get; set; }
        public int? UnitId { get; set; }
        public int TransportId { get; set; }

        public string PoNo { get; set; }

        [Display(Name = "Order Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Delivery Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DeliveryDate { get; set; }
        public int OrderQuantity { get; set; }
       
        public decimal CarryCost { get; set; }
        public int Status { get; set; }
        public DateTime ConfirmDate { get; set; }

        public virtual Company Company { get; set; }
        public virtual UserMas UserMas { get; set; }
        public virtual Agreement Agreements { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Location Location { get; set; }
        public virtual Item Item { get; set; }
        public virtual Transport Transport { get; set; }

    }
}