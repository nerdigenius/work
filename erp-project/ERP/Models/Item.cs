using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only Alphabets and Space allowed.")]
        [StringLength(50)]
        public string Name { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

    }
}