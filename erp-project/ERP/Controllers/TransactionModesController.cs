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
    public class TransactionModesController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: TransactionModes
        public ActionResult Index()
        {
            return View(db.TransactionMode.ToList());
        }

        // GET: TransactionModes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionMode transactionMode = db.TransactionMode.Find(id);
            if (transactionMode == null)
            {
                return HttpNotFound();
            }
            return View(transactionMode);
        }

        // GET: TransactionModes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionModes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TransactionMode transactionMode)
        {
            if (ModelState.IsValid)
            {
                db.TransactionMode.Add(transactionMode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transactionMode);
        }

        // GET: TransactionModes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionMode transactionMode = db.TransactionMode.Find(id);
            if (transactionMode == null)
            {
                return HttpNotFound();
            }
            return View(transactionMode);
        }

        // POST: TransactionModes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TransactionMode transactionMode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionMode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactionMode);
        }

        // GET: TransactionModes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionMode transactionMode = db.TransactionMode.Find(id);
            if (transactionMode == null)
            {
                return HttpNotFound();
            }
            return View(transactionMode);
        }

        // POST: TransactionModes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionMode transactionMode = db.TransactionMode.Find(id);
            db.TransactionMode.Remove(transactionMode);
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
