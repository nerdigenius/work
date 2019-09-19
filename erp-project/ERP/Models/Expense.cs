using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("Expense")]
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ExpenseItemId { get; set; }


        [Display(Name = "Expense Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpenseDate { get; set; }                
        public decimal Cost { get; set; }

        public virtual Company Company { get; set; }
        public virtual ExpenseItem ExpenseItem { get; set; }
    }
}