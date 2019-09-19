namespace ERP.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ERP.Models;

    public partial class ModelERP : DbContext
    {
        public ModelERP()
            : base("name=ModelERP")
        {
            Database.SetInitializer<ModelERP>(null); //historical moment
        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UsersRole> UsersRole { get; set; }

        public virtual DbSet<Agreement> Agreement { get; set; }
        public virtual DbSet<UserMas> UserMas { get; set; }

        public virtual DbSet<Location> Location { get; set; }

       // public virtual DbSet<VendorRetailerItems> VendorRetailerItems { get; set; }

        public virtual DbSet<UserDet> UserDet { get; set; }

        public virtual DbSet<VendorBank> VendorBank { get; set; }

        public virtual DbSet<Item> Item { get; set; }

        public virtual DbSet<Company> Company { get; set; }

        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<SalesOrder> SalesOrder { get; set; }

        public virtual DbSet<TransportOrder> TransportOrder { get; set; }

        public virtual DbSet<Unit> Unit { get; set; }

        public virtual DbSet<Transport> Transport { get; set; }

        public virtual DbSet<ProductCategory> ProductCategory { get; set; }

        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<CompanyBank> CompanyBank { get; set; }

        public virtual DbSet<Commission> Commission { get; set; }     

        public virtual DbSet<BankTransfer> BankTransfer { get; set; }
        
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<ExpenseItem> ExpenseItem { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<BalanceHistory> BalanceHistory { get; set; }
        public virtual DbSet<TransactionMode> TransactionMode { get; set; }
        public virtual DbSet<VendorBankAccount> VendorBankAccount { get; set; }
        public virtual DbSet<VendorCreditAdd> VendorCreditAdd { get; set; }
        public virtual DbSet<VendorCreditBalance> VendorCreditBalance { get; set; }

        public System.Data.Entity.DbSet<ERP.Models.Employee> Employees { get; set; }

       // public System.Data.Entity.DbSet<ERP.Models.BankTransfer> BankTransfer { get; set; }
    }
}
