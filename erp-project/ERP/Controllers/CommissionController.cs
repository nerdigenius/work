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
    public class CommissionController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: Commission
        public ActionResult Index()
        {
            var commission = db.Commission.Include(c => c.Item).Include(c => c.Unit).Include(c => c.UserMas).Where(x=>x.UserType == 0);
            return View(commission.ToList());
        }

        // GET: Commission/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commission.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // GET: Commission/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name");
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name");
            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x => x.UserType == 0), "Id", "Name");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");

            return View();
        }

        // POST: Commission/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Commission commission)
        {
            if (ModelState.IsValid)
            {
                commission.UserType = 0;
                db.Commission.Add(commission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Address", commission.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", commission.ItemId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", commission.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", commission.UserMasId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", commission.Item.ProductCategoryId);
          
            return View(commission);
        }

        // GET: Commission/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commission.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Address", commission.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", commission.ItemId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", commission.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", commission.UserMasId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");
            ViewBag.totalCarryingCost = commission.OrderQuantity * commission.CommissionPerUnit;

            return View(commission);
        }

        // POST: Commission/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Commission commission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", commission.ItemId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", commission.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", commission.UserMasId);
            return View(commission);
        }

        // GET: Commission/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commission.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // POST: Commission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commission commission = db.Commission.Find(id);
            db.Commission.Remove(commission);
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
