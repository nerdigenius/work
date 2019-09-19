using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("Commission")]
    public class Commission
    {
        //vendor/ retailer
        //date
        // category optional
        // item 
        //qty
        // unit
        // commission price per unit
        //total qty x comm

        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int UserMasId { get; set; }
        public virtual UserMas UserMas { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int OrderQuantity { get; set; }
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public decimal CommissionPerUnit { get; set; }
        public int UserType { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CommissionDate { get; set; }


    }
}