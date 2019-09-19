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
    public class VendorCreditAddsController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: VendorCreditAdds
        public ActionResult Index()
        {
            var vendorCreditAdd = db.VendorCreditAdd.Include(v => v.UserMas);
            return View(vendorCreditAdd.ToList());
        }

        // GET: VendorCreditAdds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorCreditAdd vendorCreditAdd = db.VendorCreditAdd.Find(id);
            if (vendorCreditAdd == null)
            {
                return HttpNotFound();
            }
            return View(vendorCreditAdd);
        }

        // GET: VendorCreditAdds/Create
        public ActionResult Create()
        {
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name");
            return View();
        }

        // POST: VendorCreditAdds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserMasId,CreditAmount")] VendorCreditAdd vendorCreditAdd)
        {
            if (ModelState.IsValid)
            {
                db.VendorCreditAdd.Add(vendorCreditAdd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", vendorCreditAdd.UserMasId);
            return View(vendorCreditAdd);
        }

        // GET: VendorCreditAdds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorCreditAdd vendorCreditAdd = db.VendorCreditAdd.Find(id);
            if (vendorCreditAdd == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", vendorCreditAdd.UserMasId);
            return View(vendorCreditAdd);
        }

        // POST: VendorCreditAdds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserMasId,CreditAmount")] VendorCreditAdd vendorCreditAdd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendorCreditAdd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", vendorCreditAdd.UserMasId);
            return View(vendorCreditAdd);
        }

        // GET: VendorCreditAdds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorCreditAdd vendorCreditAdd = db.VendorCreditAdd.Find(id);
            if (vendorCreditAdd == null)
            {
                return HttpNotFound();
            }
            return View(vendorCreditAdd);
        }

        // POST: VendorCreditAdds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VendorCreditAdd vendorCreditAdd = db.VendorCreditAdd.Find(id);
            db.VendorCreditAdd.Remove(vendorCreditAdd);
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
