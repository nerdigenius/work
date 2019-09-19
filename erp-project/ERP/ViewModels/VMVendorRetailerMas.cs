using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.ViewModels
{
    public class VMVendorRetailerMas
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public int? UserType { get; set; }
        //public int AgreementsId { get; set; }
        public string ContactPerson { get; set; }

        public int? EmployeeId { get; set; }
        public decimal InitialBalance { get; set; }

    }
}


