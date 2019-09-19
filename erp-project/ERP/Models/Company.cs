using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("Company")]
    public class Company
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only Alphabets and Space allowed.")]
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }

        public string Email { get; set; }



    }
}