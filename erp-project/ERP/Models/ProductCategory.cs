using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        public int UnitId { get; set; }

        public virtual Unit Unit { get; set; }
        [Display(Name = "Product Category")]
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only Alphabets and Space allowed.")]
        [StringLength(50)]
        public string Name { get; set; }

        
    }
}