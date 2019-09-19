﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DhaliProcurement.ViewModel
{
    public class VMPurchaseOrderEdit
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal POQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Size { get; set; }
        public int ProcPurchaseMasterId { get; set; }
        public int PurchaseOrderDetId { get; set; }
        public decimal ReqQty { get; set; }
        //public int TenderMasId { get; set; }
        public int Checkflag { get; set; }
    }

    public class VMUpdatePO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal POQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Size { get; set; }
        public int ProcPurchaseMasterId { get; set; }
        public int ProcPurchaseDetId { get; set; }
        
    }

}