using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }    
        
        [Required]
        public int UserMasId { get; set; }        
        public int TransactionModeId { get; set; }
        //public int VendorBankAccountId { get; set; }
        public int? VendorBankId { get; set; }
        public int TransactionType { get; set; }
        public decimal Amount { get; set; }
        public int CompanyBankId { get; set; }
        [StringLength(200)]
        public string CheckNo { get; set; }

        public int Status{ get; set; }
        public int IsDeleted { get; set; }

        [Display(Name = "Check Issue Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CheckIssueDate { get; set; }


        [Display(Name = "Check Passing Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CheckPassingDate { get; set; }

        public string CompanyAccountNo { get; set; }
        public string VendorAccountNo { get; set; }
        public DateTime CreatedOn { get; set; }

        //public virtual CompanyBank CompanyBank { get; set; }
        public virtual Company Company { get; set; }
        public virtual UserMas UserMas { get; set; }
        //public virtual TransactionMode TransactionMode { get; set; }
        public virtual VendorBank VendorBank { get; set; }
    }   
}