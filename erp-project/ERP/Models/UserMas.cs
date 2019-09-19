using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("UserMas")]
    public class UserMas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only Alphabets and Space allowed.")]
        public string Name { get; set; }
        [Required]
        public int Phone { get; set; }
        public string Email { get; set; }
        public int? UserType { get; set; }
        public decimal InitialBalance { get; set; }

        public string ContactPerson { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


    }
}