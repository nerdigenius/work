using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("Agreement")]
    public class Agreement
    {
        [Key]
        public int Id { get; set; }
       
        public int? UserMasId { get; set; }


        [StringLength(200)]
        public string AgreementBox { get; set; }


        public virtual UserMas UserMas { get; set; }
    }
}