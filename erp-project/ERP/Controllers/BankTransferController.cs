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
    public class BankTransferController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: BankTransfer
        public ActionResult Index()
        {
            //var companyaDetails = db.BankTransfer.SingleOrDefault(x=>x.FromCompanyId)
            
            return View(db.BankTransfer.ToList());
        }

        // GET: BankTransfer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTransfer bankTransfer = db.BankTransfer.Find(id);
            if (bankTransfer == null)
            {
                return HttpNotFound();
            }
            return View(bankTransfer);
        }

        // GET: BankTransfer/Create
        public ActionResult Create()
        {
            ViewBag.FromCompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.FromCompanyBankId = new SelectList(db.Bank, "Id", "Name");

            ViewBag.ToCompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.ToCompanyBankId = new SelectList(db.Bank, "Id", "Name");
            //var companyBankDD = (from bank in db.Bank
            //                     join companyBank in db.CompanyBank on bank.Id equals companyBank.BankId
            //                     where bank.Id == companyBank.BankId
            //                     select bank
            //                   ).Distinct().ToList();


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankTransfer bankTransfer)
        {
            if (ModelState.IsValid)
            {
                db.BankTransfer.Add(bankTransfer);
                bankTransfer.Date = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bankTransfer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTransfer selectedBankTransfer = db.BankTransfer.Find(id);
            if (selectedBankTransfer == null)
            {
                return HttpNotFound();
            }

            ViewBag.FromCompanyId = new SelectList(db.Company, "Id", "Name", selectedBankTransfer.FromCompanyId);
            ViewBag.ToCompanyId = new SelectList(db.Company, "Id", "Name", selectedBankTransfer.ToCompanyId);

            var FromCompanyBankDD = (from fromCompanyBank in db.CompanyBank
                                     join company in db.Company on fromCompanyBank.CompanyId equals company.Id
                                     where company.Id == selectedBankTransfer.FromCompanyId
                                     select fromCompanyBank.Bank
                            ).Distinct().ToList();

            ViewBag.FromCompanyBankId = new SelectList(FromCompanyBankDD, "Id", "Name", selectedBankTransfer.FromCompanyBankId);

            var FromCompanyAccountDD = (from fromCompanyBank in db.CompanyBank
                                        join bank in db.Bank on fromCompanyBank.BankId equals bank.Id
                                        
                                        where bank.Id == selectedBankTransfer.FromCompanyBankId
                                        select fromCompanyBank
                            ).ToList();

            //foreach (var fromAccounts in FromCompanyAccountDD)
            //{
            //    fromAccounts.AccountName = fromAccounts.AccountName + " (" + fromAccounts.AccountNo + ")";
            //}

            ViewBag.FromBankAccount = new SelectList(FromCompanyAccountDD, "AccountNo", "AccountName", selectedBankTransfer.FromBankAccount);


            //To Bank


            var toCompanyBankDD = (from toCompanyBank in db.CompanyBank
                                     join company in db.Company on toCompanyBank.CompanyId equals company.Id
                                     where company.Id == selectedBankTransfer.ToCompanyId
                                     select toCompanyBank.Bank
                            ).Distinct().ToList();

            ViewBag.ToCompanyBankId = new SelectList(toCompanyBankDD, "Id", "Name", selectedBankTransfer.ToCompanyBankId);

            var ToCompanyAccountDD = (from toCompanyBank in db.CompanyBank
                                        join bank in db.Bank on toCompanyBank.BankId equals bank.Id

                                        where bank.Id == selectedBankTransfer.ToCompanyBankId
                                        select toCompanyBank
                            ).ToList();

            //foreach (var toAccounts in ToCompanyAccountDD)
            //{
            //    toAccounts.AccountName = toAccounts.AccountName + " (" + toAccounts.AccountNo + ")";
            //}

            ViewBag.ToBankAccount = new SelectList(ToCompanyAccountDD, "AccountNo", "AccountName", selectedBankTransfer.ToBankAccount);

            return View(selectedBankTransfer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankTransfer bankTransfer)
        {
            if (ModelState.IsValid)
            {
                bankTransfer.Date = DateTime.Now;
                db.Entry(bankTransfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bankTransfer);
        }

        // GET: BankTransfer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTransfer bankTransfer = db.BankTransfer.Find(id);
            if (bankTransfer == null)
            {
                return HttpNotFound();
            }
            return View(bankTransfer);
        }

        // POST: BankTransfer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankTransfer bankTransfer = db.BankTransfer.Find(id);
            db.BankTransfer.Remove(bankTransfer);
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
