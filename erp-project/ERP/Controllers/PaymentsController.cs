using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.Models;

namespace ERP.Controllers
{
    public class PaymentsController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: Payments
        public ActionResult Index()
        {
            var payment = db.Payment.ToList();
            return View(payment);
        }

        public ActionResult PendingLists()
        {
            //var prevDate = DateTime.Now.AddDays(-3);
            //CreatedOn < 13 && CreatedOn > DateTime.now
            var currentDate = DateTime.Now.Date;


            var transaction = db.Payment.Where(x => x.Status == 0 && x.IsDeleted == 0).ToList();


            ViewBag.PendingCount = transaction.Count;
            return View("PendingChecks", transaction);
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.TransactionModeId = new SelectList(db.TransactionMode, "Id", "Name");
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name");

            var companyBankDD = (from bank in db.Bank
                                 join companyBank in db.CompanyBank on bank.Id equals companyBank.BankId
                                 where bank.Id == companyBank.BankId
                                 select bank
                               ).Distinct().ToList();


            var vendorBankDD = (from bank in db.Bank
                                join vendorBank in db.VendorBank on bank.Id equals vendorBank.BankId
                                where bank.Id == vendorBank.BankId
                                select bank
                               ).ToList();
            ViewBag.CompanyBankId = new SelectList(db.Bank, "Id", "Name");
            ViewBag.VendorBankId = new SelectList(vendorBankDD.Distinct(), "Id", "Name");

            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payment payment)
        {
            PurchaseOrdersController purchaseOrdersController = new PurchaseOrdersController();
            if (ModelState.IsValid)
            {
                var search = db.CompanyBank.SingleOrDefault(x => x.AccountNo == payment.CompanyAccountNo);

                payment.CompanyBankId = search.BankId;
                payment.CompanyAccountNo = search.AccountNo;

                if (payment.VendorAccountNo == "0")
                {
                    payment.VendorAccountNo = null;
                }

                if (payment.VendorBankId == 0)
                {
                    payment.VendorBankId = null;
                }

                if (payment.TransactionModeId == 1)
                {
                    payment.Status = 0;
                }
                else {
                    payment.Status = 1;
                }

                payment.CreatedOn = DateTime.Now;
                db.Payment.Add(payment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Address", payment.CompanyId);
            ViewBag.TransactionModeId = new SelectList(db.TransactionMode, "Id", "Name");
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", payment.UserMasId);
            ViewBag.VendorBankId = new SelectList(db.VendorBank, "Id", "Name");
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }

            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", payment.CompanyId);
            ViewBag.TransactionModeId = new SelectList(db.TransactionMode, "Id", "Name", payment.TransactionModeId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", payment.UserMasId);

            var companyBankDD = (from companyBank in db.CompanyBank
                                 join payments in db.Payment on companyBank.CompanyId equals payments.CompanyBankId
                                 where payments.CompanyId == payment.CompanyId
                                 select companyBank.Bank
                              ).Distinct().ToList();

            var accountsId = db.CompanyBank.Where(x => x.BankId == payment.CompanyBankId).ToList();

            foreach (CompanyBank accounts in accountsId)
            {
                accounts.AccountName = accounts.AccountName + " (" + accounts.AccountNo + ")";
            }

            ViewBag.CompanyAccountNo = new SelectList(accountsId, "Id", "AccountName", payment.CompanyBankId);
            ViewBag.companyBankID = new SelectList(companyBankDD, "Id", "Name", payment.CompanyBankId);

            //Bank vendorBankDD = new Bank();

            if (payment.TransactionType == 1 ||
                (payment.TransactionType == 0 && payment.TransactionModeId == 2))
            {
                var vendorBankDD = (from bank in db.Bank
                                    join vendorBank in db.VendorBank on bank.Id equals vendorBank.BankId
                                    where bank.Id == vendorBank.BankId && vendorBank.UserMasId == payment.UserMasId
                                    select bank
                               ).ToList();
                ViewBag.VendorBankId = new SelectList(vendorBankDD.Distinct(), "Id", "Name");
            }
            else
            {
                var vendorBankDD = db.Bank.Where(x => x.Id == payment.VendorBankId).ToList();
                ViewBag.VendorBankId = new SelectList(vendorBankDD, "Id", "Name", db.Bank.SingleOrDefault(x => x.Id == payment.VendorBankId).Id);
            }

            var vendorAccountList = db.VendorBank.Where(x => x.BankId == payment.VendorBankId).ToList();

            foreach (VendorBank VendorAccounts in vendorAccountList)
            {
                VendorAccounts.AccountName = VendorAccounts.AccountName + " (" + VendorAccounts.AccountNo + ")";
            }

            ViewBag.VendorAccountNo = new SelectList(vendorAccountList, "Id", "AccountName", payment.VendorAccountNo);
            ViewBag.status = payment.Status;
            ViewBag.isCheck = payment.TransactionModeId;
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.CreatedOn = DateTime.Now;
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                PurchaseOrdersController purchaseOrdersController = new PurchaseOrdersController();
                if (payment.TransactionType == 0)
                {
                    purchaseOrdersController.updateBalanceHistory(payment.Id, payment.UserMasId, "payment", true, payment.Amount);
                }
                else
                {
                    purchaseOrdersController.updateBalanceHistory(payment.Id, payment.UserMasId, "collection", true, payment.Amount);
                }
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", payment.CompanyId);
            ViewBag.TransactionModeId = new SelectList(db.TransactionMode, "Id", "Name", payment.TransactionModeId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", payment.UserMasId);
            ViewBag.VendorBankId = new SelectList(db.VendorBank, "Id", "Name", payment.VendorBankId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        public JsonResult confirmTransection(int transectionID)
        {
            Payment payment = db.Payment.SingleOrDefault(x => x.Id == transectionID);
            payment.Status = 1;
            payment.CreatedOn = DateTime.Now;

            db.Entry(payment).State = EntityState.Modified;

            if (db.SaveChanges() > 0) {
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAccountList(int selectedBankID, int selectedCompanyID)
        {
            var accounts = db.CompanyBank.Where(x => x.BankId == selectedBankID && x.CompanyId == selectedCompanyID).ToList();
            return Json(accounts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getBankListByCompanyID(int id)
        {
            var companyBankDD = (from bank in db.Bank
                                 join companyBank in db.CompanyBank on bank.Id equals companyBank.BankId
                                 where bank.Id == companyBank.BankId && companyBank.CompanyId == id
                                 select bank
                               ).Distinct().ToList();
            return Json(companyBankDD, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getVendorRetAccountList(int id)
        {
            var accounts = db.VendorBank.Where(x => x.BankId == id).ToList();
            return Json(accounts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getVendorBankListByVendorID(int id)
        {
            var vendorBankDD = (from bank in db.Bank
                                join vendorBank in db.VendorBank on bank.Id equals vendorBank.BankId
                                where bank.Id == vendorBank.BankId && vendorBank.UserMasId == id
                                select bank
                               ).Distinct().ToList();
            return Json(vendorBankDD, JsonRequestBehavior.AllowGet);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payment.Find(id);
            db.Payment.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
