using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("BankTransfer")]
    public class BankTransfer
    {
        [Key]
        public int Id { get; set; }
        public int FromCompanyId { get; set; }
        public int FromCompanyBankId { get; set; }
        public int FromBankAccount { get; set; }
        public decimal amount { get; set; }

        public int ToCompanyId{ get; set; }
        public int ToCompanyBankId { get; set; }
        public int ToBankAccount { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        //public virtual Company Company { get; set; }
    }
}