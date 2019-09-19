using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("BalanceHistories")]
    public class BalanceHistory
    {
        [Key]
        public int Id { get; set; }
        public int UserMasId { get; set; }
        public DateTime Date { get; set; }
        public decimal initialBalance { get; set; }
        public int? PkRef { get; set; }

        public virtual UserMas UserMas { get; set; }
        
    }
}