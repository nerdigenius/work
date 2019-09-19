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
    public class CompanyBanksController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: CompanyBanks
        public ActionResult Index()
        {
            var companyBank = db.CompanyBank.Include(c => c.Bank).Include(c => c.Company);
            return View(companyBank.ToList());
        }

        // GET: CompanyBanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBank companyBank = db.CompanyBank.Find(id);
            if (companyBank == null)
            {
                return HttpNotFound();
            }
            return View(companyBank);
        }

        // GET: CompanyBanks/Create
        public ActionResult Create()
        {
            ViewBag.BankId = new SelectList(db.Bank, "Id", "Name");
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
            return View();
        }

        // POST: CompanyBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyId,BankId,AccountName,AccountNo,Branch,SwiftCode,RoutingNo,BranchCode,Address,InitialBalance")] CompanyBank companyBank)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBank.Add(companyBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankId = new SelectList(db.Bank, "Id", "Name", companyBank.BankId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Address", companyBank.CompanyId);
            return View(companyBank);
        }

        // GET: CompanyBanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBank companyBank = db.CompanyBank.Find(id);
            if (companyBank == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankId = new SelectList(db.Bank, "Id", "Name", companyBank.BankId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Address", companyBank.CompanyId);
            return View(companyBank);
        }

        // POST: CompanyBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyId,BankId,AccountName,AccountNo,Branch,SwiftCode,RoutingNo,BranchCode,Address,InitialBalance")] CompanyBank companyBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankId = new SelectList(db.Bank, "Id", "Name", companyBank.BankId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Address", companyBank.CompanyId);
            return View(companyBank);
        }

        // GET: CompanyBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBank companyBank = db.CompanyBank.Find(id);
            if (companyBank == null)
            {
                return HttpNotFound();
            }
            return View(companyBank);
        }

        // POST: CompanyBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyBank companyBank = db.CompanyBank.Find(id);
            db.CompanyBank.Remove(companyBank);
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
