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
    public class TransportOrdersController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: TransportOrders
        public ActionResult Index()
        {
            var transportOrder = db.TransportOrder.Include(t => t.Agreements).Include(t => t.Company).Include(t => t.Item).Include(t => t.Location).Include(t => t.Transport).Include(t => t.Unit).Include(t => t.UserMas);
            return View(transportOrder.ToList());
        }

        // GET: TransportOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportOrder transportOrder = db.TransportOrder.Find(id);
            if (transportOrder == null)
            {
                return HttpNotFound();
            }
            return View(transportOrder);
        }

        // GET: TransportOrders/Create
        public ActionResult Create()
        {



            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox");
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name");
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name");
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name");
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name");
            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x => x.UserType == 0), "Id", "Name");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");
            return View();
        }

        // POST: TransportOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransportOrder transportOrder)
        {
            if (ModelState.IsValid)
            {
                transportOrder.ConfirmDate = DateTime.Now;
                transportOrder.Status = 0;

                db.TransportOrder.Add(transportOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox", transportOrder.AgreementsId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Address", transportOrder.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", transportOrder.ItemId);
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name", transportOrder.LocationId);
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name", transportOrder.TransportId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", transportOrder.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", transportOrder.UserMasId);
            return View(transportOrder);
        }

        // GET: TransportOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportOrder transportOrder = db.TransportOrder.Find(id);
            if (transportOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox", transportOrder.AgreementsId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Address", transportOrder.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", transportOrder.ItemId);
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name", transportOrder.LocationId);
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name", transportOrder.TransportId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", transportOrder.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", transportOrder.UserMasId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", transportOrder.Item.ProductCategoryId);
            ViewBag.totalCarryingCost = transportOrder.OrderQuantity * transportOrder.CarryCost;
            ViewBag.status = transportOrder.Status;

            return View(transportOrder);
        }

        // POST: TransportOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransportOrder transportOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox", transportOrder.AgreementsId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Address", transportOrder.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", transportOrder.ItemId);
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name", transportOrder.LocationId);
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name", transportOrder.TransportId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", transportOrder.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", transportOrder.UserMasId);
            return View(transportOrder);
        }


        public JsonResult confirmOrder(int orderID, int UserMasId)
        {

            var checkUpdate = 0;
            checkUpdate = 1;
            var check = db.TransportOrder.Find(orderID).Status = 1;
            var confirmDate = db.TransportOrder.Find(orderID).ConfirmDate = DateTime.Now;
            db.SaveChanges();


            return Json(checkUpdate, JsonRequestBehavior.AllowGet);
        }
        // GET: TransportOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportOrder transportOrder = db.TransportOrder.Find(id);
            if (transportOrder == null)
            {
                return HttpNotFound();
            }
            return View(transportOrder);
        }

        // POST: TransportOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransportOrder transportOrder = db.TransportOrder.Find(id);
            db.TransportOrder.Remove(transportOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult DeleteTransport(int id)
        {
            var itemFind = db.TransportOrder.ToList().Find(x => x.Id == id);

            db.TransportOrder.Remove(itemFind);
            db.SaveChanges();

            var result = new
            {
                flag = true,
                message = "Deletion successful."
            };
            return Json(result, JsonRequestBehavior.AllowGet);


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
