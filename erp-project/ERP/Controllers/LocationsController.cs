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
    public class LocationsController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.Location.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Location.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Location.Find(id);
            db.Location.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public JsonResult DeleteLocations(int locationId)
        {
            //string result = "";

            bool flag = false;
            try
            {

                var itemToDeleteMas = db.Location.Find(locationId);

                if (itemToDeleteMas == null)
                {
                    var result = new
                    {
                        flag = false,
                        message = "Deletion failed!\nLocation Not found."
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var checkLocationPurchase = db.PurchaseOrder.Where(x => x.LocationId == locationId).ToList();

                if (checkLocationPurchase.Count > 0)
                {
                    var result = new
                    {
                        flag = false,
                        message = "Deletion failed!\nLocation exists. Delete Purchase order data first."
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                var checkLocationSales = db.SalesOrder.Where(x => x.LocationId == locationId).ToList();

                if (checkLocationSales.Count > 0)
                {
                    var result = new
                    {
                        flag = false,
                        message = "Deletion failed!\nLocation exists. Delete Sales order data first."
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                var checkLocationVendorRetailer = db.UserDet.Where(x => x.LocationId == locationId).ToList();

                if (checkLocationVendorRetailer.Count > 0)
                {
                    var result = new
                    {
                        flag = false,
                        message = "Deletion failed!\nLocation exists. Delete Location from Vendor or Retailer data first."
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);

                }

              

               
                db.Location.Remove(itemToDeleteMas);

                flag = db.SaveChanges() > 0;

            }
            catch (Exception)
            {

            }

            if (flag)
            {
                var result = new
                {
                    flag = true,
                    message = "Deletion successful."
                };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var result = new
                {
                    flag = false,
                    message = "Deletion failed!\nError Occured."
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

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
