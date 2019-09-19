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
    public class VendorBanksController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: VendorBanks
        public ActionResult Index()
        {
            var vendorBank = db.VendorBank.Include(v => v.Bank).Include(v => v.UserMas);
            return View(vendorBank.ToList());
        }

        // GET: VendorBanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorBank vendorBank = db.VendorBank.Find(id);
            if (vendorBank == null)
            {
                return HttpNotFound();
            }
            return View(vendorBank);
        }

        // GET: VendorBanks/Create
        public ActionResult Create()
        {
            ViewBag.BankId = new SelectList(db.Bank, "Id", "Name");
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name");
            return View();
        }

        // POST: VendorBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankId,UserMasId,AccountName,AccountNo,Branch,SwiftCode,RoutingNo,BranchCode,Address")] VendorBank vendorBank)
        {
            if (ModelState.IsValid)
            {
                db.VendorBank.Add(vendorBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankId = new SelectList(db.Bank, "Id", "Name", vendorBank.BankId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", vendorBank.UserMasId);
            return View(vendorBank);
        }

        // GET: VendorBanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorBank vendorBank = db.VendorBank.Find(id);
            if (vendorBank == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankId = new SelectList(db.Bank, "Id", "Name", vendorBank.BankId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", vendorBank.UserMasId);
            return View(vendorBank);
        }

        // POST: VendorBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankId,UserMasId,AccountName,AccountNo,Branch,SwiftCode,RoutingNo,BranchCode,Address")] VendorBank vendorBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendorBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankId = new SelectList(db.Bank, "Id", "Name", vendorBank.BankId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", vendorBank.UserMasId);
            return View(vendorBank);
        }

        // GET: VendorBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorBank vendorBank = db.VendorBank.Find(id);
            if (vendorBank == null)
            {
                return HttpNotFound();
            }
            return View(vendorBank);
        }

        // POST: VendorBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VendorBank vendorBank = db.VendorBank.Find(id);
            db.VendorBank.Remove(vendorBank);
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
