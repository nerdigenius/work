using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("Unit")]
    public class Unit
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only Alphabets allowed.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public int? isDeleted { get; set; }
    }
}