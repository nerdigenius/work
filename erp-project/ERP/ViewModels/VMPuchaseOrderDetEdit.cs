using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.ViewModels
{
    public class VMPuchaseOrderDetEdit
    {

        public int TempOrderDetId { get; set; }

        public int Id { get; set; }

        public int PurchaseMasId { get; set; }

        public int ProductCategoryId { get; set; }
        
        public int ItemId { get; set; }
      
        public int LocationId { get; set; }
       
        public int TransportId { get; set; }
      
        public DateTime? ScheduleDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Tax { get; set; }
    }
}