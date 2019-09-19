using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("Transport")]
    public class Transport
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(50)]
        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only Alphabets and Space allowed.")]
        public string Name { get; set; }
        [Required]
        public string PlateNo { get; set; }
    }
}