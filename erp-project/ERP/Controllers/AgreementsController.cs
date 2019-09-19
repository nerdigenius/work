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
    public class AgreementsController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: Agreements
        public ActionResult Index()
        {
            var agreement = db.Agreement.Include(a => a.UserMas);
            return View(agreement.ToList());
        }

        // GET: Agreements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = db.Agreement.Find(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            return View(agreement);
        }

        // GET: Agreements/Create
        public ActionResult Create()
        {
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name");
            return View();
        }

        // POST: Agreements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserMasId,AgreementBox")] Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                db.Agreement.Add(agreement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", agreement.UserMasId);
            return View(agreement);
        }

        // GET: Agreements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = db.Agreement.Find(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", agreement.UserMasId);
            return View(agreement);
        }

        // POST: Agreements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserMasId,AgreementBox")] Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agreement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", agreement.UserMasId);
            return View(agreement);
        }

        // GET: Agreements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = db.Agreement.Find(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            return View(agreement);
        }

        // POST: Agreements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agreement agreement = db.Agreement.Find(id);
            db.Agreement.Remove(agreement);
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
