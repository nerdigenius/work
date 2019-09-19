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
    public class UnitsController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: Units
        public ActionResult Index()
        {

            //string[] names = { "Bill", "Steve", "James", "Mohan" };

            //var myLinqQuery = from name in names
            //                  where name.Contains('a')
            //                  select name;

            //foreach (var name in myLinqQuery)
            //    Console.Write(name + " ");
            var units = db.Unit.Where(x => x.isDeleted == 0).ToList();

            return View(units);
        }

        // GET: Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Unit.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: Units/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                unit.isDeleted = 0;
                db.Unit.Add(unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(unit);
        }

        // GET: Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Unit.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unit);
        }

        // GET: Units/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Unit unit = db.Unit.Find(id);
        //    if (unit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(unit);
        //}

        //// POST: Units/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Unit unit = db.Unit.Find(id);
        //    db.Unit.Remove(unit);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}



        public List<Unit> getDeletedUnits()
        {
            var data = db.Unit.Where(x => x.isDeleted == 0).ToList();
            return data;
        }

        public JsonResult DeleteUnits(int unitId)
        {

            //var UnitsCount = (from prodCategory in db.ProductCategory
            //                  join purchaseOrder in db.PurchaseOrder on prodCategory.UnitId equals purchaseOrder.UnitId
            //                 // join salesOrder in db.SalesOrder on purchaseOrder.UnitId equals salesOrder.UnitId
            //                  //join salesOrder in db.SalesOrder on prodCategory.UnitId equals salesOrder.UnitId into fs
            //                  //from salesOrderDelete in fs.DefaultIfEmpty()

            //                  where purchaseOrder.UnitId == unitId 
            //                  //|| salesOrderDelete.UnitId == unitId 
            //                  //&& purchaseOrder.isDeleted == -1
            //                  select purchaseOrder).Distinct().Count();


            var itemFind = db.Unit.Find(unitId);
            itemFind.isDeleted = 1;
            db.Entry(itemFind).State = EntityState.Modified;
            db.SaveChanges();

            var result = new
            {
                flag = true,
                message = "Unit is deleted successfully."
            };
            return Json(result, JsonRequestBehavior.AllowGet);


            //if (UnitsCount == 0)
            //{
            //    bool flag = false;
            //    try
            //    {
            //        var itemsToDeleteCategory = db.ProductCategory.Where(x => x.UnitId == unitId);
            //        db.ProductCategory.RemoveRange(itemsToDeleteCategory);
            //        db.SaveChanges();

            //        var itemsToDeleteUnit = db.Unit.Where(x => x.Id == unitId);
            //        db.Unit.RemoveRange(itemsToDeleteUnit);
            //        //db.SaveChanges();


            //        flag = db.SaveChanges() > 0;

            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //    if (flag)
            //    {
            //        var result = new
            //        {
            //            flag = true,
            //            message = "Unit is deleted successfully."
            //        };
            //        return Json(result, JsonRequestBehavior.AllowGet);

            //    }
            //    else
            //    {
            //        var result = new
            //        {
            //            flag = false,
            //            message = "Unit deletion failed!\nError Occured."
            //        };
            //        return Json(result, JsonRequestBehavior.AllowGet);
            //    }


            //}
            //else
            //{
            //    var result = new
            //    {
            //        flag = false,
            //        message = "Unit deletion failed!\nDelete Purchase Order and Sales Order first."
            //    };
            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}

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
