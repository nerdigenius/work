using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Helpers;
using ERP.Models;
using ERP.ReportDataset;
using ERP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class ReportController : Controller
    {
        private readonly ModelERP db;
        public ReportController()
        {
            db = new ModelERP();
        }
        //comment
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        #region API
        public JsonResult GetTransportByVendorRetailer(int UserMasId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();


            var items = db.TransportOrder.Where(x => x.UserMasId == UserMasId).ToList();


            foreach (var x in items)
            {
                itemList.Add(new SelectListItem { Text = x.Transport.PlateNo, Value = x.TransportId.ToString() });
            }


            return Json(itemList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetExpenseItemByCompany(int CompanyId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();


            var items = db.Expense.Where(x => x.CompanyId == CompanyId).ToList();


            foreach (var x in items)
            {
                itemList.Add(new SelectListItem { Text = x.ExpenseItem.Name, Value = x.ExpenseItemId.ToString() });
            }


            return Json(itemList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCategoryByUser(int UserMasId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();


            var items = db.PurchaseOrder.Where(x => x.UserMasId == UserMasId).ToList();


            foreach (var x in items)
            {
                itemList.Add(new SelectListItem { Text = x.Item.ProductCategory.Name, Value = x.Item.ProductCategoryId.ToString() });
            }


            return Json(itemList, JsonRequestBehavior.AllowGet);
        }


        public List<VMVendorLedgerReport> getPurchaseOrders(int cid, int uid, DateTime dateFrom, DateTime dateTo, int type = 1)
        {
            List<VMVendorLedgerReport> orders = new List<VMVendorLedgerReport>();

            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();

            if (type == 1)
            {
                var myquery = from purchase in db.PurchaseOrder
                              join company in db.Company on purchase.CompanyId equals company.Id
                              where purchase.UserMasId == uid && purchase.CompanyId == cid && purchase.ConfirmDate >= dateFrom && purchase.ConfirmDate <= dateTo
                              select purchase;

                purchaseOrders = myquery.ToList();
            }

            if (type == 2)
            {
                var myquery = from purchase in db.PurchaseOrder
                              join company in db.Company on purchase.CompanyId equals company.Id
                              where purchase.UserMasId == uid && purchase.CompanyId == cid && purchase.ConfirmDate < dateFrom
                              select purchase;

                purchaseOrders = myquery.ToList();
            }

            foreach (var item in purchaseOrders)
            {
                VMVendorLedgerReport objcvm = new VMVendorLedgerReport();
                objcvm.Description = "Purchase Order";
                objcvm.Date = item.ConfirmDate;
                objcvm.TransactionTypeName = "Purchase Order";
                objcvm.Quantity = item.OrderQuantity;
                objcvm.Rate = item.UnitPrice;
                objcvm.DrCr = item.OrderQuantity * item.UnitPrice;
                objcvm.Balance = 0;
                objcvm.TransactionType = 1;
                objcvm.TransportBill = item.CarryCost;

                orders.Add(objcvm);
            }

            return orders;
        }

        public List<VMVendorLedgerReport> getSalesOrders(int cid, int uid, DateTime dateFrom, DateTime dateTo, int type = 1)
        {
            List<VMVendorLedgerReport> orders = new List<VMVendorLedgerReport>();

            List<SalesOrder> salesOrders = new List<SalesOrder>();

            if (type == 1)
            {
                var myquery = from sales in db.SalesOrder
                              join company in db.Company on sales.CompanyId equals company.Id
                              where sales.UserMasId == uid && sales.CompanyId == cid && sales.ConfirmDate >= dateFrom && sales.ConfirmDate <= dateTo
                              select sales;

                salesOrders = myquery.ToList();
            }

            if (type == 2)
            {
                var myquery = from sales in db.SalesOrder
                              join company in db.Company on sales.CompanyId equals company.Id
                              where sales.UserMasId == uid && sales.CompanyId == cid && sales.ConfirmDate < dateFrom
                              select sales;

                salesOrders = myquery.ToList();
            }

            foreach (var item in salesOrders)
            {
                VMVendorLedgerReport objcvm = new VMVendorLedgerReport();
                objcvm.Description = "Sales Order";
                objcvm.Date = item.ConfirmDate;
                objcvm.TransactionTypeName = "Sales Order";
                objcvm.Quantity = item.OrderQuantity;
                objcvm.Rate = item.UnitPrice;
                objcvm.DrCr = item.OrderQuantity * item.UnitPrice;
                objcvm.Balance = 0;
                objcvm.TransactionType = 1;
                objcvm.TransportBill = item.CarryCost;

                orders.Add(objcvm);
            }

            return orders;
        }

        public List<VMVendorLedgerReport> getPayaments(int cid, int uid, DateTime dateFrom, DateTime dateTo, int type = 1)
        {
            List<VMVendorLedgerReport> orders = new List<VMVendorLedgerReport>();
            List<Payment> payments = new List<Payment>();

            if (type == 1)
            {
                var myquery = from payment in db.Payment
                              join company in db.Company on payment.CompanyId equals company.Id
                              where payment.UserMasId == uid && payment.CompanyId == cid && payment.CreatedOn >= dateFrom && payment.CreatedOn <= dateTo && payment.TransactionType == 0
                              && payment.Status == 1 && payment.IsDeleted == 0
                              select payment;

                payments = myquery.ToList();
            }

            if (type == 2)
            {
                var myquery = from payment in db.Payment
                              join company in db.Company on payment.CompanyId equals company.Id
                              where payment.CompanyId == cid && payment.UserMasId == uid && payment.CreatedOn < dateFrom && payment.TransactionType == 0
                              && payment.Status == 1 && payment.IsDeleted == 0
                              select payment;

                payments = myquery.ToList();
            }

            foreach (var item in payments)
            {
                VMVendorLedgerReport objcvm = new VMVendorLedgerReport();
                objcvm.Description = "Payment";
                objcvm.Date = item.CreatedOn;
                objcvm.TransactionTypeName = "Payment";
                objcvm.Quantity = 0;
                objcvm.Rate = 0;
                objcvm.DrCr = item.Amount;
                objcvm.Balance = 0;
                objcvm.TransactionType = 2;

                orders.Add(objcvm);
            }

            return orders;
        }

        public List<VMVendorLedgerReport> getColletion(int cid, int uid, DateTime dateFrom, DateTime dateTo, int type = 1)
        {
            List<VMVendorLedgerReport> orders = new List<VMVendorLedgerReport>();
            List<Payment> sales = new List<Payment>();

            if (type == 1)
            {
                var myquery = from payment in db.Payment
                              join company in db.Company on payment.CompanyId equals company.Id
                              where payment.UserMasId == uid && payment.CompanyId == cid && payment.CreatedOn
                              >= dateFrom && payment.CreatedOn <= dateTo && payment.TransactionType == 1
                              && payment.Status == 1 && payment.IsDeleted == 0
                              select payment;

                sales = myquery.ToList();
            }

            if (type == 2)
            {
                var myquery = from payment in db.Payment
                              join company in db.Company on payment.CompanyId equals company.Id
                              where payment.CompanyId == cid && payment.UserMasId == uid && payment.CreatedOn < dateFrom && payment.TransactionType == 1
                              && payment.Status == 1 && payment.IsDeleted == 0
                              select payment;

                sales = myquery.ToList();
            }

            foreach (var item in sales)
            {
                VMVendorLedgerReport objcvm = new VMVendorLedgerReport();
                objcvm.Description = "Collection";
                objcvm.Date = item.CreatedOn;
                objcvm.TransactionTypeName = "Collection";
                objcvm.Quantity = 0;
                objcvm.Rate = 0;
                objcvm.DrCr = item.Amount;
                objcvm.Balance = 0;
                objcvm.TransactionType = 2;

                orders.Add(objcvm);
            }

            return orders;
        }

        public List<VMVendorLedgerReport> getPaymentAndCollection(int cid, int cbid, string accNo, DateTime dateFrom, DateTime dateTo, int type = 1)
        {
            List<VMVendorLedgerReport> orders = new List<VMVendorLedgerReport>();
            List<Payment> transections = new List<Payment>();

            var description = "";
            var transectionTypeName = "";
            //var transectionType = "";

            if (type == 1)
            {
                var myquery = from payment in db.Payment
                              join company in db.Company on payment.CompanyId equals company.Id
                              where payment.CompanyBankId == cbid && payment.CompanyId == cid && payment.CompanyAccountNo == accNo
                              && payment.CreatedOn >= dateFrom && payment.CreatedOn <= dateTo
                              && payment.Status == 1 && payment.IsDeleted == 0
                              select payment;

                transections = myquery.ToList();
            }

            if (type == 2)
            {
                var myquery = from payment in db.Payment
                              join company in db.Company on payment.CompanyId equals company.Id
                              where payment.CompanyId == cid && payment.CompanyBankId == cbid && payment.CompanyAccountNo == accNo
                              && payment.CreatedOn < dateFrom
                              && payment.Status == 1 && payment.IsDeleted == 0
                              select payment;

                transections = myquery.ToList();
            }

            foreach (var item in transections)
            {
                if (item.TransactionType == 0)
                {
                    description = "Withdrawal";
                    transectionTypeName = "Payment";
                }

                if (item.TransactionType == 1)
                {
                    description = "Deposit";
                    transectionTypeName = "Collection";
                }

                VMVendorLedgerReport objcvm = new VMVendorLedgerReport();
                objcvm.Description = description;
                objcvm.Date = item.CreatedOn;
                objcvm.TransactionTypeName = transectionTypeName;
                objcvm.Quantity = 0;
                objcvm.Rate = 0;
                objcvm.DrCr = item.Amount;
                objcvm.Balance = 0;
                objcvm.TransactionType = item.TransactionType;

                orders.Add(objcvm);
            }

            return orders;
        }

        public List<VMVendorLedgerReport> getCommission(int cid, int uid, DateTime dateFrom, DateTime dateTo, int type = 1)
        {
            List<VMVendorLedgerReport> commissions = new List<VMVendorLedgerReport>();
            List<Commission> commisionsList = new List<Commission>();

            if (type == 1)
            {
                var myquery = from commision in db.Commission
                              join company in db.Company on commision.CompanyId equals company.Id
                              where commision.UserMasId == uid && commision.CompanyId == cid
                              && commision.CommissionDate >= dateFrom
                              && commision.CommissionDate <= dateTo
                              select commision;

                commisionsList = myquery.ToList();
            }

            if (type == 2)
            {
                var myquery = from commision in db.Commission
                              join company in db.Company on commision.CompanyId equals company.Id
                              where commision.UserMasId == uid && commision.CompanyId == cid && commision.CommissionDate < dateFrom
                              select commision;

                commisionsList = myquery.ToList();
            }

            foreach (var item in commisionsList)
            {
                VMVendorLedgerReport objcvm = new VMVendorLedgerReport();
                objcvm.Description = "Commission";
                objcvm.Date = item.CommissionDate;
                objcvm.TransactionTypeName = "Commission";
                objcvm.Quantity = item.OrderQuantity;
                objcvm.Rate = item.CommissionPerUnit;
                objcvm.DrCr = item.CommissionPerUnit * item.OrderQuantity;
                objcvm.Balance = 0;
                objcvm.TransactionType = 5; //commission vendor
                objcvm.TransportBill = 0;

                commissions.Add(objcvm);
            }

            return commissions;
        }

        public decimal getInitialBalance(int CompanyId, int UserMasId, DateTime DateFrom, DateTime DateTo)
        {
            decimal initialBalance = db.UserMas.FirstOrDefault(x => x.Id == UserMasId).InitialBalance;
            decimal currenInitialBalance = initialBalance;

            var purchaseList = getPurchaseOrders(CompanyId, UserMasId, DateFrom, DateTo, 2);
            var paymentList = getPayaments(CompanyId, UserMasId, DateFrom, DateTo, 2);
            var commissionList = getCommission(CompanyId, UserMasId, DateFrom, DateTo, 2);

            var ReportList = purchaseList.Concat(paymentList).ToList();
            ReportList = ReportList.Concat(commissionList).ToList();

            ReportList = ReportList.AsQueryable().OrderBy(x => x.Date).ToList();

            decimal totalTransportBill = 0;

            foreach (var list in ReportList)
            {
                if (list.TransactionType == 1)
                {
                    list.Balance = currenInitialBalance - list.DrCr;
                    currenInitialBalance = list.Balance;
                }

                if (list.TransactionType == 2 || list.TransactionType == 5)
                {
                    list.Balance = currenInitialBalance + list.DrCr;
                    currenInitialBalance = list.Balance;
                }

                totalTransportBill = totalTransportBill + list.TransportBill;
            }

            currenInitialBalance = currenInitialBalance + totalTransportBill;

            return currenInitialBalance;
        }

        public decimal getInitialBalanceSales(int CompanyId, int UserMasId, DateTime DateFrom, DateTime DateTo)
        {

            decimal initialBalance = db.UserMas.FirstOrDefault(x => x.Id == UserMasId).InitialBalance;
            decimal currenInitialBalance = initialBalance;

            var salesList = getSalesOrders(CompanyId, UserMasId, DateFrom, DateTo, 2);
            var collectionList = getColletion(CompanyId, UserMasId, DateFrom, DateTo, 2);
            var commissionList = getCommission(CompanyId, UserMasId, DateFrom, DateTo, 2);

            var ReportList = salesList.Concat(collectionList).ToList();
            ReportList = ReportList.Concat(commissionList).ToList();

            ReportList = ReportList.AsQueryable().OrderBy(x => x.Date).ToList();

            decimal totalTransportBill = 0;

            foreach (var list in ReportList)
            {
                if (list.TransactionType == 2 || list.TransactionType == 5)
                {
                    list.Balance = currenInitialBalance - list.DrCr;
                    currenInitialBalance = list.Balance;
                }

                if (list.TransactionType == 1)
                {
                    list.Balance = currenInitialBalance + list.DrCr;
                    currenInitialBalance = list.Balance;
                }

                totalTransportBill = totalTransportBill + list.TransportBill;
            }

            currenInitialBalance = currenInitialBalance - totalTransportBill;

            return currenInitialBalance;
        }

        public decimal getInitialBalanceBank(int CompanyId, int bankId, string accountId, DateTime DateFrom, DateTime DateTo)
        {

            decimal initialBalance = db.CompanyBank.SingleOrDefault(x => x.CompanyId == CompanyId
                                                                    && x.BankId == bankId
                                                                    && x.AccountNo == accountId).InitialBalance;
            decimal currenInitialBalance = initialBalance;

            var paymentsAndCollections = getPaymentAndCollection(CompanyId, bankId, accountId, DateFrom, DateTo);
            var BankToBankTranferList = getBankToBankTranfer(CompanyId, bankId, accountId, DateFrom, DateTo);

            var ReportList = paymentsAndCollections.Concat(BankToBankTranferList).ToList();
            ReportList = ReportList.AsQueryable().OrderBy(x => x.Date).ToList();

            foreach (var item in ReportList)
            {
                if (item.TransactionType == 3)
                {
                    item.Balance = -1 * (currenInitialBalance - item.DrCr);
                    currenInitialBalance = item.Balance;
                }

                if (item.TransactionType == 4)
                {
                    item.Balance = currenInitialBalance + item.DrCr;
                    currenInitialBalance = item.Balance;
                }
            }


            return currenInitialBalance;
        }


        public List<VMVendorLedgerReport> getBankToBankTranfer(int FromCompanyId, int FromCompanyBankId, string FromBankAccount, DateTime DateFrom, DateTime DateTo, int type = 1)
        {
            List<VMVendorLedgerReport> orders = new List<VMVendorLedgerReport>();
            List<BankTransfer> transections = new List<BankTransfer>();

            var transectionType = 0;

            if (type == 1)
            {
                var myquery = (from bankTransfer in db.BankTransfer
                               where
                               (
                                   bankTransfer.FromCompanyId == FromCompanyId
                                   && bankTransfer.FromCompanyBankId == FromCompanyBankId
                                   && bankTransfer.FromBankAccount.ToString() == FromBankAccount
                               )
                               ||
                               (
                                   bankTransfer.ToCompanyId == FromCompanyId
                                   && bankTransfer.ToCompanyBankId == FromCompanyBankId
                                   && bankTransfer.ToBankAccount.ToString() == FromBankAccount
                               )
                               && bankTransfer.Date >= DateFrom && bankTransfer.Date <= DateTo
                               select bankTransfer);

                transections = myquery.ToList();
            }

            if (type == 2)
            {
                var myquery = from bankTransfer in db.BankTransfer
                              where
                              (
                                  bankTransfer.FromCompanyId == FromCompanyId
                                  && bankTransfer.FromCompanyBankId == FromCompanyBankId
                                  && bankTransfer.FromBankAccount.ToString() == FromBankAccount
                              )
                              ||
                              (
                                  bankTransfer.ToCompanyId == FromCompanyId
                                  && bankTransfer.ToCompanyBankId == FromCompanyBankId
                                  && bankTransfer.ToBankAccount.ToString() == FromBankAccount
                              )
                              && bankTransfer.Date < bankTransfer.Date
                              select bankTransfer;

                transections = myquery.ToList();
            }

            foreach (var item in transections)
            {
                if (item.FromBankAccount.ToString() == FromBankAccount)
                {
                    transectionType = 3; //credit (-)
                }

                if (item.ToBankAccount.ToString() == FromBankAccount)
                {
                    transectionType = 4; //debit (+)
                }

                VMVendorLedgerReport objcvm = new VMVendorLedgerReport();
                objcvm.Description = "Withdrawl";
                objcvm.Date = item.Date;
                objcvm.TransactionTypeName = "Withdrawl";
                objcvm.Quantity = 0;
                objcvm.Rate = 0;
                objcvm.DrCr = item.amount;
                objcvm.Balance = 0;
                objcvm.TransactionType = transectionType;

                orders.Add(objcvm);
            }

            return orders;
        }

        #endregion

        #region Vendor Retailer Ledger
        [HttpGet]
        public ActionResult VendorLedgerReport()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");

            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x => x.UserType == 0).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult VendorLedgerReport(int CompanyId, int UserMasId, DateTime DateFrom, DateTime DateTo)
        {
            VendorLedgerDS vendorLedger = new VendorLedgerDS();

            var initialBalance = getInitialBalance(CompanyId, UserMasId, DateFrom, DateTo);
            var vendorInfo = db.UserMas.FirstOrDefault(x => x.Id == UserMasId);
            var companyInfo = db.Company.FirstOrDefault(x => x.Id == CompanyId);

            vendorLedger.VendorInfo.AddVendorInfoRow(vendorInfo.Name,
                                                           vendorInfo.Phone,
                                                           vendorInfo.Email,
                                                           companyInfo.Name,
                                                           initialBalance);
            var purchaseList = getPurchaseOrders(CompanyId, UserMasId, DateFrom, DateTo);
            var paymentList = getPayaments(CompanyId, UserMasId, DateFrom, DateTo);
            var commissionList = getCommission(CompanyId, UserMasId, DateFrom, DateTo);

            var ReportList = purchaseList.Concat(paymentList).ToList();
            ReportList = ReportList.Concat(commissionList).ToList();

            ReportList = ReportList.AsQueryable().OrderBy(x => x.Date).ToList();

            var dateConvert = Convert.ToDateTime(DateFrom.Date).Date;

            var currentBalance = initialBalance;
            decimal totalTransportBill = 0;

            foreach (var item in ReportList)
            {

                if (item.TransactionType == 1)
                {
                    item.Balance = currentBalance - item.DrCr;
                    currentBalance = item.Balance;
                }

                if (item.TransactionType == 2 || item.TransactionType == 5)
                {
                    item.Balance = currentBalance + item.DrCr;
                    currentBalance = item.Balance;
                }

                totalTransportBill = totalTransportBill + item.TransportBill;
            }


            List<VMVendorLedgerReport> transport = new List<VMVendorLedgerReport> {
                new VMVendorLedgerReport { Date = DateTime.Now, TransactionTypeName = "Transport Bill", Description = "Transport Bill",  Quantity = 0, Rate = 0, DrCr = totalTransportBill, TransactionType = 3, Balance = totalTransportBill + currentBalance}
            };

            ReportList.AddRange(transport);

            foreach (var item in ReportList)
            {
                if (item.TransactionType == 1)
                {
                    item.DrCr = (-1 * (item.DrCr));
                    vendorLedger.VendorLedger.AddVendorLedgerRow(item.Date,
                                                             item.TransactionType,
                                                             item.Description,
                                                             item.Quantity,
                                                             item.Rate,
                                                             item.DrCr,
                                                             item.Balance);

                }
                else
                {
                    vendorLedger.VendorLedger.AddVendorLedgerRow(item.Date,
                                                             item.TransactionType,
                                                             item.Description,
                                                             item.Quantity,
                                                             item.Rate,
                                                             item.DrCr,
                                                             item.Balance);

                }

            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "VendorLedgerReport.rpt"));

            rd.SetDataSource(vendorLedger);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Byte[] fileBuffer = ms.ToArray();

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", fileBuffer.Length.ToString());
            Response.BinaryWrite(fileBuffer);
            return null;
        }

        [HttpGet]
        public ActionResult RetailerLedgerReport()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");

            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x => x.UserType == 1).ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult RetailerLedgerReport(int CompanyId, int UserMasId, DateTime DateFrom, DateTime DateTo)
        {

            VendorLedgerDS retailerLedger = new VendorLedgerDS();

            var initialBalance = getInitialBalanceSales(CompanyId, UserMasId, DateFrom, DateTo);

            var retailerInfo = db.UserMas.FirstOrDefault(x => x.Id == UserMasId);
            var companyInfo = db.Company.FirstOrDefault(x => x.Id == CompanyId);
            var commissionList = getCommission(CompanyId, UserMasId, DateFrom, DateTo);

            retailerLedger.VendorInfo.AddVendorInfoRow(retailerInfo.Name,
                                                           retailerInfo.Phone,
                                                           retailerInfo.Email,
                                                           companyInfo.Name,
                                                           initialBalance);

            var salesList = getSalesOrders(CompanyId, UserMasId, DateFrom, DateTo);
            var collectionList = getColletion(CompanyId, UserMasId, DateFrom, DateTo);

            var ReportList = salesList.Concat(collectionList).ToList();
            ReportList = ReportList.Concat(commissionList).ToList();

            ReportList = ReportList.AsQueryable().OrderBy(x => x.Date).ToList();

            var dateConvert = Convert.ToDateTime(DateFrom.Date).Date;

            var currentBalance = initialBalance;
            decimal totalTransportBill = 0;

            foreach (var item in ReportList)
            {
                if (item.TransactionType == 2 || item.TransactionType == 5)
                {
                    item.Balance = currentBalance - item.DrCr;
                    currentBalance = item.Balance;
                }

                if (item.TransactionType == 1)
                {
                    item.Balance = currentBalance + item.DrCr;
                    currentBalance = item.Balance;
                }

                totalTransportBill = totalTransportBill + item.TransportBill;
            }


            List<VMVendorLedgerReport> transport = new List<VMVendorLedgerReport> {
                new VMVendorLedgerReport { Date = DateTime.Now, TransactionTypeName = "Transport Bill", Description = "Transport Bill",  Quantity = 0, Rate = 0, DrCr = totalTransportBill, TransactionType = 3, Balance = totalTransportBill + currentBalance}
            };

            ReportList.AddRange(transport);

            foreach (var item in ReportList)
            {
                if (item.TransactionType == 1)
                {
                    item.DrCr = (item.DrCr);
                    retailerLedger.VendorLedger.AddVendorLedgerRow(item.Date,
                                                             item.TransactionType,
                                                             item.Description,
                                                             item.Quantity,
                                                             item.Rate,
                                                             item.DrCr,
                                                             item.Balance);

                }
                else
                {
                    item.DrCr = -1 * (item.DrCr);
                    retailerLedger.VendorLedger.AddVendorLedgerRow(item.Date,
                                                             item.TransactionType,
                                                             item.Description,
                                                             item.Quantity,
                                                             item.Rate,
                                                             item.DrCr,
                                                             item.Balance);
                }

            }

            var vendorInfo = db.UserMas.FirstOrDefault(x => x.Id == UserMasId);
            var companyName = db.Company.FirstOrDefault(x => x.Id == CompanyId).Name;
            retailerLedger.VendorInfo.AddVendorInfoRow(vendorInfo.Name,
                                                     vendorInfo.Phone,
                                                     vendorInfo.Email,
                                                     companyName,
                                                     currentBalance);

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "RetailerLedgerReport.rpt"));

            rd.SetDataSource(retailerLedger);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Byte[] fileBuffer = ms.ToArray();

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", fileBuffer.Length.ToString());
            Response.BinaryWrite(fileBuffer);
            return null;
        }

        #endregion

        #region Purchase Sales
        [HttpGet]
        public ActionResult PurchaseOrderReport()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");

            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x => x.UserType == 0).ToList(), "Id", "Name");

            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name");

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult PurchaseOrderReport(int? CompanyId, int? UserMasId, int ProductCategoryId, int? ItemId, DateTime DateFrom, DateTime DateTo)
        {

            var companyInfo = db.Company.FirstOrDefault();


            PurchaseOrderDS dsPurchase = new PurchaseOrderDS();

            if (companyInfo != null)
            {
                dsPurchase.CompanyInfo.AddCompanyInfoRow(companyInfo.Phone,
                                                             companyInfo.Address,
                                                             companyInfo.Website,
                                                             companyInfo.Email,
                                                             companyInfo.Name);
            }
            else
            {

                dsPurchase.CompanyInfo.AddCompanyInfoRow(0,
                                                             "",
                                                             "",
                                                             "",
                                                             ""
                                                             );

            }


            var purchaseList = (from purchase in db.PurchaseOrder
                                join company in db.Company on purchase.CompanyId equals company.Id
                                where purchase.Item.ProductCategoryId == ProductCategoryId && purchase.ConfirmDate >= DateFrom && purchase.ConfirmDate <= DateTo
                                select new { purchase }).ToList();


            if (CompanyId != null && ItemId != null && UserMasId != null)
            {
                purchaseList = purchaseList.Where(x => x.purchase.ItemId == ItemId && x.purchase.CompanyId == CompanyId && x.purchase.UserMasId == UserMasId).ToList();
            }
            else if (ItemId != null)
            {
                purchaseList = purchaseList.Where(x => x.purchase.ItemId == ItemId).ToList();
            }

            else if (CompanyId != null)
            {
                purchaseList = purchaseList.Where(x => x.purchase.CompanyId == CompanyId).ToList();
            }

            dsPurchase.DateInput.AddDateInputRow(NullHelpers.DateToString(DateFrom),
                                                 NullHelpers.DateToString(DateTo),
                                                 NullHelpers.DateToString(DateTime.Now));
            foreach (var item in purchaseList)
            {
                dsPurchase.PurchaseOrders.AddPurchaseOrdersRow(
                    item.purchase.UserMasId,
                    item.purchase.UserMas.Name,
                    item.purchase.ItemId,
                    item.purchase.Item.Name,
                    NullHelpers.DateToString(item.purchase.OrderDate),
                    item.purchase.OrderQuantity,
                    item.purchase.UnitPrice,
                    item.purchase.UnitPrice * item.purchase.OrderQuantity
                    );
            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "PurchaseOrderReport.rpt"));

            rd.SetDataSource(dsPurchase);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Byte[] fileBuffer = ms.ToArray();

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", fileBuffer.Length.ToString());
            Response.BinaryWrite(fileBuffer);
            return null;
        }



        [HttpGet]
        public ActionResult SalesOrderReport()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");

            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x => x.UserType == 1).ToList(), "Id", "Name");

            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name");

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");

            return View();
        }


        [HttpPost]
        public ActionResult SalesOrderReport(int? CompanyId, int? UserMasId, int ProductCategoryId, int? ItemId, DateTime DateFrom, DateTime DateTo)
        {

            var companyInfo = db.Company.FirstOrDefault(x => x.Id == CompanyId);


            PurchaseOrderDS dsPurchase = new PurchaseOrderDS();

            if (companyInfo != null)
            {
                dsPurchase.CompanyInfo.AddCompanyInfoRow(companyInfo.Phone,
                                                             companyInfo.Address,
                                                             companyInfo.Website,
                                                             companyInfo.Email,
                                                             companyInfo.Name);
            }
            else
            {

                dsPurchase.CompanyInfo.AddCompanyInfoRow(0,
                                                             "",
                                                             "",
                                                             "",
                                                             ""
                                                             );

            }


            var salesList = (from sales in db.SalesOrder
                             join company in db.Company on sales.CompanyId equals company.Id
                             where sales.Item.ProductCategoryId == ProductCategoryId && sales.ConfirmDate >= DateFrom && sales.ConfirmDate <= DateTo
                             select new { sales }).ToList();


            if (CompanyId != null && ItemId != null && UserMasId != null)
            {
                salesList = salesList.Where(x => x.sales.ItemId == ItemId && x.sales.CompanyId == CompanyId && x.sales.UserMasId == UserMasId).ToList();
            }
            else if (ItemId != null)
            {
                salesList = salesList.Where(x => x.sales.ItemId == ItemId).ToList();
            }

            else if (CompanyId != null)
            {
                salesList = salesList.Where(x => x.sales.CompanyId == CompanyId).ToList();
            }

            dsPurchase.DateInput.AddDateInputRow(NullHelpers.DateToString(DateFrom),
                                                 NullHelpers.DateToString(DateTo),
                                                 NullHelpers.DateToString(DateTime.Now));
            foreach (var item in salesList)
            {
                dsPurchase.PurchaseOrders.AddPurchaseOrdersRow(
                    item.sales.UserMasId,
                    item.sales.UserMas.Name,
                    item.sales.ItemId,
                    item.sales.Item.Name,
                    NullHelpers.DateToString(item.sales.OrderDate),
                    item.sales.OrderQuantity,
                    item.sales.UnitPrice,
                    item.sales.UnitPrice * item.sales.OrderQuantity
                    );
            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "SalesOrderReport.rpt"));

            rd.SetDataSource(dsPurchase);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Byte[] fileBuffer = ms.ToArray();

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", fileBuffer.Length.ToString());
            Response.BinaryWrite(fileBuffer);
            return null;
        }

        #endregion

        #region Commission Vendor Retailer
        [HttpGet]

        public ActionResult VendorCommissionReport()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");

            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x => x.UserType == 0).ToList(), "Id", "Name");

            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name");

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult VendorCommissionReport(int? CompanyId, int? UserMasId, int ProductCategoryId, int? ItemId, DateTime DateFrom, DateTime DateTo)
        {

            var companyInfo = db.Company.FirstOrDefault();


            CommissionDS dsCommission = new CommissionDS();

            if (companyInfo != null)
            {
                dsCommission.CompanyInfo.AddCompanyInfoRow(companyInfo.Phone,
                                                             companyInfo.Address,
                                                             companyInfo.Website,
                                                             companyInfo.Email,
                                                             companyInfo.Name);
            }
            else
            {

                dsCommission.CompanyInfo.AddCompanyInfoRow(0,
                                                             "",
                                                             "",
                                                             "",
                                                             ""
                                                             );

            }


            var commissionList = (from commission in db.Commission
                                  join company in db.Company on commission.CompanyId equals company.Id
                                  where commission.CommissionDate >= DateFrom && commission.CommissionDate <= DateTo
                                  && commission.UserType == 0
                                  select new { commission }).ToList();


            if (CompanyId != null && ItemId != null && UserMasId != null)
            {
                commissionList = commissionList.Where(x => x.commission.ItemId == ItemId && x.commission.CompanyId == CompanyId && x.commission.UserMasId == UserMasId).ToList();
            }
            else if (ItemId != null)
            {
                commissionList = commissionList.Where(x => x.commission.ItemId == ItemId).ToList();
            }

            else if (CompanyId != null)
            {
                commissionList = commissionList.Where(x => x.commission.CompanyId == CompanyId).ToList();
            }

            dsCommission.DateInput.AddDateInputRow(NullHelpers.DateToString(DateFrom),
                                                 NullHelpers.DateToString(DateTo),
                                                 NullHelpers.DateToString(DateTime.Now));
            foreach (var item in commissionList)
            {
                dsCommission.UserCommission.AddUserCommissionRow(
                    item.commission.UserMas.Name,

                    item.commission.Item.Name,
                    item.commission.OrderQuantity,
                    item.commission.Unit.Name,
                    item.commission.CommissionPerUnit,
                    item.commission.OrderQuantity * item.commission.CommissionPerUnit,
                    NullHelpers.DateToString(item.commission.CommissionDate)

                    );
            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "VendorComissionReport.rpt"));

            rd.SetDataSource(dsCommission);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Byte[] fileBuffer = ms.ToArray();

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", fileBuffer.Length.ToString());
            Response.BinaryWrite(fileBuffer);
            return null;
        }



        [HttpGet]
        public ActionResult RetailerCommissionReport()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");

            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x => x.UserType == 1).ToList(), "Id", "Name");

            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name");

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult RetailerCommissionReport(int? CompanyId, int? UserMasId, int ProductCategoryId, int? ItemId, DateTime DateFrom, DateTime DateTo)
        {

            var companyInfo = db.Company.FirstOrDefault();


            CommissionDS dsCommission = new CommissionDS();

            if (companyInfo != null)
            {
                dsCommission.CompanyInfo.AddCompanyInfoRow(companyInfo.Phone,
                                                             companyInfo.Address,
                                                             companyInfo.Website,
                                                             companyInfo.Email,
                                                             companyInfo.Name);
            }
            else
            {

                dsCommission.CompanyInfo.AddCompanyInfoRow(0,
                                                             "",
                                                             "",
                                                             "",
                                                             ""
                                                             );

            }


            var commissionList = (from commission in db.Commission
                                  join company in db.Company on commission.CompanyId equals company.Id
                                  where commission.CommissionDate >= DateFrom && commission.CommissionDate <= DateTo
                                  && commission.UserType == 1
                                  select new { commission }).ToList();


            if (CompanyId != null && ItemId != null && UserMasId != null)
            {
                commissionList = commissionList.Where(x => x.commission.ItemId == ItemId && x.commission.CompanyId == CompanyId && x.commission.UserMasId == UserMasId).ToList();
            }
            else if (ItemId != null)
            {
                commissionList = commissionList.Where(x => x.commission.ItemId == ItemId).ToList();
            }

            else if (CompanyId != null)
            {
                commissionList = commissionList.Where(x => x.commission.CompanyId == CompanyId).ToList();
            }

            dsCommission.DateInput.AddDateInputRow(NullHelpers.DateToString(DateFrom),
                                                 NullHelpers.DateToString(DateTo),
                                                 NullHelpers.DateToString(DateTime.Now));
            foreach (var item in commissionList)
            {
                dsCommission.UserCommission.AddUserCommissionRow(
                    item.commission.UserMas.Name,

                    item.commission.Item.Name,
                    item.commission.OrderQuantity,
                    item.commission.Unit.Name,
                    item.commission.CommissionPerUnit,
                    item.commission.OrderQuantity * item.commission.CommissionPerUnit,
                    NullHelpers.DateToString(item.commission.CommissionDate)

                    );
            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "RetailerComissionReport.rpt"));

            rd.SetDataSource(dsCommission);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Byte[] fileBuffer = ms.ToArray();

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", fileBuffer.Length.ToString());
            Response.BinaryWrite(fileBuffer);
            return null;
        }

        #endregion

        [HttpGet]
        public ActionResult TransportationReport()
        {
            ViewBag.TransportNo = new SelectList(db.Transport, "Id", "PlateNo");

            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x => x.UserType == 0).ToList(), "Id", "Name");
            return View();
        }



        [HttpPost]
        public ActionResult TransportationReport(int? UserMasId, int? TransportNo, DateTime DateFrom, DateTime DateTo)
        {

            var companyInfo = db.Company.FirstOrDefault();


            TransportDS dsTransport = new TransportDS();

            if (companyInfo != null)
            {
                dsTransport.CompanyInfo.AddCompanyInfoRow(companyInfo.Phone,
                                                             companyInfo.Address,
                                                             companyInfo.Website,
                                                             companyInfo.Email,
                                                             companyInfo.Name);
            }
            else
            {

                dsTransport.CompanyInfo.AddCompanyInfoRow(0,
                                                             "",
                                                             "",
                                                             "",
                                                             ""
                                                             );

            }


            var transportList = (from transport in db.TransportOrder
                                 where transport.ConfirmDate >= DateFrom && transport.ConfirmDate <= DateTo && transport.Status == 1
                                 select new { transport }).ToList();


            if (UserMasId != null && TransportNo != null)
            {
                transportList = transportList.Where(x => x.transport.UserMasId == UserMasId && x.transport.TransportId == TransportNo).ToList();
            }

            else if (UserMasId != null)
            {
                transportList = transportList.Where(x => x.transport.UserMasId == UserMasId).ToList();
            }


            dsTransport.DateInput.AddDateInputRow(NullHelpers.DateToString(DateFrom),
                                                 NullHelpers.DateToString(DateTo),
                                                 NullHelpers.DateToString(DateTime.Now));
            foreach (var item in transportList)
            {
                dsTransport.Transport.AddTransportRow(
                  NullHelpers.DateToString(item.transport.ConfirmDate),
                    item.transport.Transport.PlateNo,
                    item.transport.UserMas.Name,
                    item.transport.OrderQuantity,
                    item.transport.CarryCost,
                    item.transport.OrderQuantity * item.transport.CarryCost
                    );
            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "TransportationReport.rpt"));

            rd.SetDataSource(dsTransport);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Byte[] fileBuffer = ms.ToArray();

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", fileBuffer.Length.ToString());
            Response.BinaryWrite(fileBuffer);
            return null;
        }

        [HttpGet]

        public ActionResult BankTransferReport()
        {
            ViewBag.FromCompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.FromCompanyBankId = new SelectList(db.Bank, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult BankTransferReport(int FromCompanyId, int FromCompanyBankId, string FromBankAccount, DateTime DateFrom, DateTime DateTo)
        {
            VendorLedgerDS bankToBank = new VendorLedgerDS();

            decimal inittialBalance = getInitialBalanceBank(FromCompanyId, FromCompanyBankId, FromBankAccount, DateFrom, DateTo);
            var currentBalance = inittialBalance;

            var paymentsAndCollections = getPaymentAndCollection(FromCompanyId, FromCompanyBankId, FromBankAccount, DateFrom, DateTo);
            var BankToBankTranferList = getBankToBankTranfer(FromCompanyId, FromCompanyBankId, FromBankAccount, DateFrom, DateTo);

            var ReportList = paymentsAndCollections.Concat(BankToBankTranferList).ToList();
            ReportList = ReportList.AsQueryable().OrderBy(x => x.Date).ToList();

            foreach (var item in ReportList)
            {
                if (item.TransactionType == 3)
                {
                    item.Balance = -1 * (currentBalance - item.DrCr);
                    currentBalance = item.Balance;
                }

                if (item.TransactionType == 4)
                {
                    item.Balance = currentBalance + item.DrCr;
                    currentBalance = item.Balance;
                }

                bankToBank.VendorLedger.AddVendorLedgerRow(item.Date,
                                                             item.TransactionType,
                                                             item.Description,
                                                             item.Quantity,
                                                             item.Rate,
                                                             item.DrCr,
                                                             item.Balance);
            }

            return View();
        }


        [HttpGet]
        public ActionResult ExpenseReport()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.ExpenseItemId = new SelectList(db.ExpenseItem, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult ExpenseReport(int? CompanyId, int? ExpenseItemId, DateTime DateFrom, DateTime DateTo)
        {

            var companyInfo = db.Company.FirstOrDefault();


            ExpenseDS dsExpense = new ExpenseDS();

            if (companyInfo != null)
            {
                dsExpense.CompanyInfo.AddCompanyInfoRow(companyInfo.Phone,
                                                             companyInfo.Address,
                                                             companyInfo.Website,
                                                             companyInfo.Email,
                                                             companyInfo.Name);
            }
            else
            {

                dsExpense.CompanyInfo.AddCompanyInfoRow(0,
                                                             "",
                                                             "",
                                                             "",
                                                             ""
                                                             );

            }


            var expenseList = (from expense in db.Expense
                               join expenseItems in db.ExpenseItem on expense.ExpenseItemId equals expenseItems.Id


                               where expense.ExpenseDate >= DateFrom && expense.ExpenseDate <= DateTo
                               select new { expense }).ToList();


            if (CompanyId != null)
            {
                expenseList = expenseList.Where(x => x.expense.CompanyId == CompanyId).ToList();
            }

            else if (CompanyId != null && ExpenseItemId != null)
            {
                expenseList = expenseList.Where(x => x.expense.CompanyId == CompanyId && x.expense.ExpenseItemId == ExpenseItemId).ToList();
            }

            dsExpense.DateInput.AddDateInputRow(NullHelpers.DateToString(DateFrom),
                                                 NullHelpers.DateToString(DateTo),
                                                 NullHelpers.DateToString(DateTime.Now));
            foreach (var item in expenseList)
            {
                dsExpense.Expense.AddExpenseRow(

                    NullHelpers.DateToString(item.expense.ExpenseDate),
                    item.expense.ExpenseItem.Name,
                    item.expense.Cost
                    );
            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ExpenseReport.rpt"));

            rd.SetDataSource(dsExpense);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            Byte[] fileBuffer = ms.ToArray();

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", fileBuffer.Length.ToString());
            Response.BinaryWrite(fileBuffer);
            return null;
        }

       
    }
}