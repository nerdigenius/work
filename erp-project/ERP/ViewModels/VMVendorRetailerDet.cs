using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.ViewModels
{
    public class VMVendorRetailerDet
    {
        public int TempOrderDetId { get; set; }

        public int Id { get; set; }

        public int ProductCategoryId { get; set; }

        public int ItemId { get; set; }

  

        public int UnitPrice { get; set; }


        //edit
        public int LocationId { get; set; }

        //end
        public string Location { get; set; }

        public decimal CarryingCost { get; set; }




        //Edit

        //public int LocationId { get; set; }
        public int VendorRetailerId { get; set; }

    }
}


