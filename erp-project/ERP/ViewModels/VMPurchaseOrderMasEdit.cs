using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.ViewModels
{
    public class VMPurchaseOrderMasEdit
    {

        public int Id { get; set; }

        public int UserRoleId { get; set; }
      
        public int AgreementsId { get; set; }
       
      
        public DateTime? OrderDate { get; set; }
       
        public string VendorRef { get; set; }
        public int Status { get; set; }
        public int? Type { get; set; }
    }
}