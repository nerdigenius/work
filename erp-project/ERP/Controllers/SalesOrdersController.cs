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
    [Authorize]
    public class SalesOrdersController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: PurchaseOrders
        public ActionResult Index()
        {
            var salesOrder = db.SalesOrder.Where(x=>x.UserMas.UserType==1 && x.isDeleted == 1).ToList();
            return View(salesOrder);
        }

        // GET: PurchaseOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.SalesOrder.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            return View(salesOrder);
        }

        // GET: salesOrders/Create
        public ActionResult Create()
        {
            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox");
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name");
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name");
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name");
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name");
            ViewBag.UserMasId = new SelectList(db.UserMas.Where(x=>x.UserType==1), "Id", "Name");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");
            return View();
        }

        // POST: salesOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                salesOrder.ConfirmDate = DateTime.Now;
                salesOrder.Status = 0;
                salesOrder.isDeleted = 1;

                db.SalesOrder.Add(salesOrder);
                db.SaveChanges();
                Inventory inventory = new Inventory();

                var isItemExistInInventory = db.Inventory.Where(inventoryData => inventoryData.CompanyId == salesOrder.CompanyId && inventoryData.ItemId == salesOrder.ItemId).ToList();

                if (isItemExistInInventory.Count == 0)
                {

                    inventory.CompanyId = salesOrder.CompanyId;
                    inventory.ItemId = salesOrder.ItemId;
                    inventory.Quantity = 0;
                    inventory.OrderId = salesOrder.Id;
                    db.Inventory.Add(inventory);


                    db.SaveChanges();
                }
             

                return RedirectToAction("Index");
            }

            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox", salesOrder.AgreementsId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", salesOrder.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", salesOrder.ItemId);
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name", salesOrder.LocationId);
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name", salesOrder.TransportId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", salesOrder.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", salesOrder.UserMasId);

            return View(salesOrder);
        }

        // GET: salesOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.SalesOrder.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox", salesOrder.AgreementsId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", salesOrder.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", salesOrder.ItemId);
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name", salesOrder.LocationId);
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name", salesOrder.TransportId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", salesOrder.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", salesOrder.UserMasId);

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", salesOrder.Item.ProductCategoryId);
            ViewBag.productSubtotal = salesOrder.OrderQuantity * salesOrder.UnitPrice;

            var totalCost = salesOrder.OrderQuantity * salesOrder.UnitPrice;
            ViewBag.taxTotal = totalCost * ((salesOrder.Tax) / 100);
            ViewBag.totalCarryingCost = salesOrder.OrderQuantity * salesOrder.CarryCost;
            ViewBag.status = salesOrder.Status;
            return View(salesOrder);
        }

        // POST: salesOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                salesOrder.ConfirmDate = DateTime.Now;
                db.Entry(salesOrder).State = EntityState.Modified;
                db.SaveChanges();

                Inventory inventory = new Inventory();

                var isItemExistInInventory = db.Inventory.Where(inventoryData => inventoryData.CompanyId == salesOrder.CompanyId && inventoryData.ItemId == salesOrder.ItemId).ToList();

                if (isItemExistInInventory.Count == 0)
                {

                    inventory.CompanyId = salesOrder.CompanyId;
                    inventory.ItemId = salesOrder.ItemId;
                    inventory.Quantity = 0;
                    inventory.OrderId = salesOrder.Id;
                    db.Inventory.Add(inventory);


                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox", salesOrder.AgreementsId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", salesOrder.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", salesOrder.ItemId);
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name", salesOrder.LocationId);
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name", salesOrder.TransportId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", salesOrder.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", salesOrder.UserMasId);
            return View(salesOrder);
        }

        public JsonResult confirmOrder(int companyID, int SelecteditemID, int quantity, int orderID, int UserMasId)
        {

            bool flag = false;
            Inventory inventory = new Inventory();
            Inventory inventoryItem = db.Inventory.SingleOrDefault(x => x.CompanyId == companyID && x.ItemId == SelecteditemID);
            if (inventoryItem == null)
            {
                inventory.CompanyId = companyID;
                inventory.OrderId = orderID;
                inventory.Quantity = quantity;
                inventory.ItemId = SelecteditemID;
                db.Inventory.Add(inventory);
                flag = db.SaveChanges() > 0;
            }
            else {
                inventoryItem.Quantity = inventoryItem.Quantity - quantity;
                db.Entry(inventoryItem).State = EntityState.Modified;
                flag = db.SaveChanges() > 0;
            }



            //db.Inventory.Add(inventory);

            

            var checkUpdate = 0;

            if (flag)
            {
                checkUpdate = 1;
                var check = db.SalesOrder.Find(orderID).Status = 1;
                var confirmDate = db.SalesOrder.Find(orderID).ConfirmDate= DateTime.Now;
                db.SaveChanges();
            }

            PurchaseOrdersController purchaseOrdersController = new PurchaseOrdersController();
            purchaseOrdersController.updateBalanceHistory(orderID, UserMasId, "sales");
   
            return Json(checkUpdate, JsonRequestBehavior.AllowGet);
        }        

        public JsonResult getRelatedData(int id)
        {

            var locationId = db.UserDet.Where(x=>x.UserMasId == id)
                                       .OrderBy(x => x.Location.Name)
                                       .Select(y => new { Name = y.Location.Name, Id = y.LocationId }).ToList();

            //var items = db.UserDet.Where(x => x.UserMasId == id)
            //                           .OrderBy(x => x.Item.Name)
            //                           .Select(y => new { Name = y.Item.Name, Id = y.ItemId }).ToList();


            //var categories = db.UserDet.Where(x => x.UserMasId == id)
            //                          .OrderBy(x => x.Item.ProductCategory.Name)
            //                          .Select(y => new { Name = y.Item.ProductCategory.Name, Id = y.Item.ProductCategoryId }).ToList();




          

            //var result = new
            //{
            //    prodCatList = 0,
            //    prodCatWiseItemList = 0,
            //    locationList = 0
            //};
            return Json(locationId, JsonRequestBehavior.AllowGet);

        }


        // GET: PurchaseOrders/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SalesOrder salesOrder = db.SalesOrder.Find(id);
        //    if (salesOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(salesOrder);
        //}

        //// POST: PurchaseOrders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SalesOrder purchaseOrder = db.SalesOrder.Find(id);
        //    db.SalesOrder.Remove(purchaseOrder);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        public JsonResult DeleteSalesOrders(int salesOrderId)
        {

          


            var itemFind = db.SalesOrder.ToList().Find(x => x.Id == salesOrderId);

            if (itemFind.Status == 0)
            {
                itemFind.isDeleted = 0;
                db.Entry(itemFind).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var inventory = db.Inventory.FirstOrDefault(x => x.CompanyId == itemFind.CompanyId && x.ItemId == itemFind.ItemId);
                inventory.Quantity = inventory.Quantity + itemFind.OrderQuantity;
                var findItem = db.Inventory.Find(inventory.Id);
                findItem.Quantity = inventory.Quantity;
                db.Entry(findItem).State = EntityState.Modified;


                itemFind.isDeleted = 0;
                db.Entry(itemFind).State = EntityState.Modified;

                db.SaveChanges();


            }



            var result = new
            {
                flag = true,
                message = "Unit is deleted successfully."
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
