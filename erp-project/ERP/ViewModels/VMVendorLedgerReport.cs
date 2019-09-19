using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.ViewModels
{
    public class VMVendorLedgerReport
    {
        public DateTime Date { get; set; }
        public string TransactionTypeName { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }

        public decimal Rate { get; set; }
        public decimal DrCr { get; set; }
        public decimal Balance { get; set; }
        public int TransactionType { get; set; }
        public decimal TransportBill { get; set; }

        public decimal initialBalance { get; set; }

    }
}