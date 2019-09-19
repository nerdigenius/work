using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.Models;
using ERP.ViewModels;

namespace ERP.Controllers
{

    //[Authorize]
    public class PurchaseOrdersController : Controller
    {
        private ModelERP db = new ModelERP();

        // GET: PurchaseOrders
        public ActionResult Index()
        {
            var purchaseOrder = db.PurchaseOrder
                .Include(p => p.Agreements)
                .Include(p => p.Company)
                .Include(p => p.Item)
                .Include(p => p.Location)
                .Include(p => p.Transport)
                .Include(p => p.Unit)
                .Include(p => p.UserMas)
                .Where(x => x.UserMas.UserType == 0 && x.isDeleted ==1);
            return View(purchaseOrder.ToList());
        }
        //test
        // GET: PurchaseOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
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

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                purchaseOrder.ConfirmDate = DateTime.Now;
                purchaseOrder.Status = 0;
                purchaseOrder.isDeleted = 1;
                db.PurchaseOrder.Add(purchaseOrder);
                db.SaveChanges();
                Inventory inventory = new Inventory();

                var isItemExistInInventory = db.Inventory.Where(inventoryData => inventoryData.CompanyId == purchaseOrder.CompanyId && inventoryData.ItemId == purchaseOrder.ItemId).ToList();

                if (isItemExistInInventory.Count == 0)
                {

                    inventory.CompanyId = purchaseOrder.CompanyId;
                    inventory.ItemId = purchaseOrder.ItemId;
                    inventory.Quantity = 0;
                    inventory.OrderId = purchaseOrder.Id;
                    db.Inventory.Add(inventory);

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox", purchaseOrder.AgreementsId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", purchaseOrder.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", purchaseOrder.ItemId);
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name", purchaseOrder.LocationId);
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name", purchaseOrder.TransportId);
            ViewBag.UnitId = new SelectList(db.Unit.Where(x=> x.isDeleted == 1), "Id", "Name", purchaseOrder.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", purchaseOrder.UserMasId);

            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox", purchaseOrder.AgreementsId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", purchaseOrder.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", purchaseOrder.ItemId);
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name", purchaseOrder.LocationId);
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name", purchaseOrder.TransportId);
            ViewBag.UnitId = new SelectList(db.Unit.Where(x=>x.isDeleted==1), "Id", "Name", purchaseOrder.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", purchaseOrder.UserMasId);

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", purchaseOrder.Item.ProductCategoryId);
            ViewBag.productSubtotal = purchaseOrder.OrderQuantity * purchaseOrder.UnitPrice;

            var totalCost = purchaseOrder.OrderQuantity * purchaseOrder.UnitPrice;
            ViewBag.taxTotal = totalCost * ((purchaseOrder.Tax) / 100);
            ViewBag.totalCarryingCost = purchaseOrder.OrderQuantity * purchaseOrder.CarryCost;
            ViewBag.status = purchaseOrder.Status;
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                purchaseOrder.ConfirmDate = DateTime.Now;
                db.Entry(purchaseOrder).State = EntityState.Modified;
                db.SaveChanges();

                Inventory inventory = new Inventory();

                var isItemExistInInventory = db.Inventory.Where(inventoryData => inventoryData.CompanyId == purchaseOrder.CompanyId && inventoryData.ItemId == purchaseOrder.ItemId).ToList();

                if (isItemExistInInventory.Count == 0)
                {

                    inventory.CompanyId = purchaseOrder.CompanyId;
                    inventory.ItemId = purchaseOrder.ItemId;
                    inventory.Quantity = 0;
                    inventory.OrderId = purchaseOrder.Id;
                    db.Inventory.Add(inventory);

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.AgreementsId = new SelectList(db.Agreement, "Id", "AgreementBox", purchaseOrder.AgreementsId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", purchaseOrder.CompanyId);
            ViewBag.ItemId = new SelectList(db.Item, "Id", "Name", purchaseOrder.ItemId);
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Name", purchaseOrder.LocationId);
            ViewBag.TransportId = new SelectList(db.Transport, "Id", "Name", purchaseOrder.TransportId);
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", purchaseOrder.UnitId);
            ViewBag.UserMasId = new SelectList(db.UserMas, "Id", "Name", purchaseOrder.UserMasId);
            return View(purchaseOrder);
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
            else
            {
                inventoryItem.Quantity = inventoryItem.Quantity + quantity;
                db.Entry(inventoryItem).State = EntityState.Modified;
                flag = db.SaveChanges() > 0;
            }

            //db.Inventory.Add(inventory);

            var checkUpdate = 0;

            if (flag)
            {
                checkUpdate = 1;
                var check = db.PurchaseOrder.Find(orderID).Status = 1;
                var confirmDate = db.PurchaseOrder.Find(orderID).ConfirmDate = DateTime.Now;
                db.SaveChanges();
            }

            //updateBalanceHistory(orderID, UserMasId, "purchase");

            return Json(checkUpdate, JsonRequestBehavior.AllowGet);
        }

        public bool updateBalanceHistory(int orderID, int vendorID, string Type, bool edit = false, decimal amount = 0)
        {
            BalanceHistory balanceHistory = new BalanceHistory();
            VMMixData mixData = new VMMixData();

            decimal vendorCurrentInitialBalance = 0;
            var isDateExist = db.BalanceHistory.Where(x => x.Date.Day == DateTime.Now.Day
                                                        && x.Date.Month == DateTime.Now.Month
                                                        && x.Date.Year == DateTime.Now.Year
                                                        && x.UserMasId == vendorID).ToList();

            var userHistory = db.BalanceHistory.Where(x => x.UserMasId == vendorID).ToList();
            BalanceHistory lastHistory = new BalanceHistory();
            PurchaseOrder PurchaseData = new PurchaseOrder();
            SalesOrder SalesData = new SalesOrder();
            Payment PaymentOrCollectionData = new Payment();

            if (Type == "purchase")
            {
                PurchaseData = db.PurchaseOrder.SingleOrDefault(x => x.Id == orderID);
                mixData.quantity = PurchaseData.OrderQuantity;
                mixData.unitPrice = PurchaseData.UnitPrice;
            }

            if (Type == "sales")
            {
                SalesData = db.SalesOrder.SingleOrDefault(x => x.Id == orderID);
                mixData.quantity = SalesData.OrderQuantity;
                mixData.unitPrice = SalesData.UnitPrice;
            }

            if ((Type == "payment" || Type == "collection") && edit == false)
            {
                PaymentOrCollectionData = db.Payment.SingleOrDefault(x => x.Id == orderID);
                mixData.amount = PaymentOrCollectionData.Amount;
            }
            else if ((Type == "payment" || Type == "collection") && edit == true)
            {
                PaymentOrCollectionData = db.Payment.SingleOrDefault(x => x.Id == orderID);
                mixData.amount = amount;
            }

            var update = false;
            if (userHistory.Count > 0)
            {
                if (isDateExist.Count > 0)
                {
                    update = true;
                    lastHistory = isDateExist[0];
                    balanceHistory = lastHistory;

                    vendorCurrentInitialBalance = currentBalance(Type, lastHistory.initialBalance, mixData);
                }
                else
                {
                    lastHistory = userHistory[userHistory.Count - 1];
                    vendorCurrentInitialBalance = currentBalance(Type, lastHistory.initialBalance, mixData);
                }
            }
            else
            {
                vendorCurrentInitialBalance = db.UserMas.SingleOrDefault(userMas => userMas.Id == vendorID).InitialBalance;
                vendorCurrentInitialBalance = currentBalance(Type, lastHistory.initialBalance, mixData);
            }

            balanceHistory.UserMasId = vendorID;
            balanceHistory.initialBalance = vendorCurrentInitialBalance;
            balanceHistory.Date = DateTime.Now;

            if (update)
            {
                db.Entry(balanceHistory).State = EntityState.Modified;
                db.SaveChanges();
            }

            else
            {
                db.BalanceHistory.Add(balanceHistory);
                db.SaveChanges();
            }

            return true;
        }

        public decimal currentBalance(string Type, decimal balance, VMMixData data)
        {

            if (Type == "purchase")
            {
                return balance - (data.unitPrice * data.quantity);
            }

            if (Type == "sales")
            {
                return balance + (data.unitPrice * data.quantity); ;
            }

            if (Type == "payment")
            {
                return balance + data.amount;
            }

            if (Type == "collection")
            {
                return balance - data.amount;
            }

            return 0;
        }

        public JsonResult getRelatedData(int id)
        {

            var locationId = db.UserDet.Where(x => x.UserMasId == id)
                                       .OrderBy(x => x.Location.Name)
                                       .Select(y => new { Name = y.Location.Name, Id = y.LocationId }).ToList();
            return Json(locationId, JsonRequestBehavior.AllowGet);

        }


        // GET: PurchaseOrders/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
        //    if (purchaseOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(purchaseOrder);
        //}

        //// POST: PurchaseOrders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    PurchaseOrder purchaseOrder = db.PurchaseOrder.Find(id);
        //    db.PurchaseOrder.Remove(purchaseOrder);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        //public JsonResult DeletePurchaseOrders(int purchaseOrderId, int companyId, int itemId, int orderQuantity, int userMasId)
        //{
        //    var itemsDeletedByOrderId = (from purchaseOrders in db.PurchaseOrder
        //                                     //join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
        //                                 join inventoryItems in db.Inventory on purchaseOrders.Id equals inventoryItems.OrderId
        //                                 //  join balanceHistory in db.BalanceHistory on purchaseOrders.UserMasId equals balanceHistory.UserMasId
        //                                 where inventoryItems.OrderId == purchaseOrderId

        //                                 select new { inventoryItems, purchaseOrders }).FirstOrDefault();


        //    bool flag = false;
        //    if (itemsDeletedByOrderId.purchaseOrders.Status == 0)
        //    {
        //        //checking pending
        //        var itemsToDeleteTask = db.PurchaseOrder.Where(x => x.Id == purchaseOrderId);
        //        db.PurchaseOrder.RemoveRange(itemsToDeleteTask);
        //        db.SaveChanges();



        //        // flag = db.SaveChanges() > 0;
        //    }
        //    else
        //    {
        //        //checking approved

        //        Inventory inventories = new Inventory();
        //        inventories.Quantity = itemsDeletedByOrderId.inventoryItems.Quantity - itemsDeletedByOrderId.purchaseOrders.OrderQuantity;
        //        inventories.ItemId = itemsDeletedByOrderId.inventoryItems.ItemId;
        //        inventories.OrderId = itemsDeletedByOrderId.inventoryItems.OrderId;


        //        var balanceHistoriesByUserMas = db.BalanceHistory.SingleOrDefault(x => x.UserMasId == itemsDeletedByOrderId.purchaseOrders.UserMasId && x.Date <= itemsDeletedByOrderId.purchaseOrders.ConfirmDate);

        //        var initialBalanceUpdated = balanceHistoriesByUserMas.initialBalance - (itemsDeletedByOrderId.purchaseOrders.OrderQuantity - itemsDeletedByOrderId.purchaseOrders.UnitPrice);

        //        var updateFieldsInBalanceHistory = db.BalanceHistory.Find(item)
        //        bHistory.initialBalance = initialBalanceUpdated;

        //        db.Entry(inventories).State = EntityState.Modified;
        //        db.SaveChanges();

        //    }






        //    if (PurchaseOrdersCount == 0)
        //    {
        //        bool flag = false;
        //        try
        //        {
        //            var itemsToDeleteTask = db.Inventory.Where(x => x.OrderId == purchaseOrderId);
        //            db.Inventory.RemoveRange(itemsToDeleteTask);
        //            db.SaveChanges();



        //            flag = db.SaveChanges() > 0;

        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //        if (flag)
        //        {
        //            var result = new
        //            {
        //                flag = true,
        //                message = "Purchase Orders deletion successful."
        //            };
        //            return Json(result, JsonRequestBehavior.AllowGet);

        //        }
        //        else
        //        {
        //            var result = new
        //            {
        //                flag = false,
        //                message = "Purchase Orders deletion failed!\nError Occured."
        //            };
        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }


        //    }
        //    else
        //    {
        //        var result = new
        //        {
        //            flag = false,
        //            message = "Purchase Orders deletion failed!\nDelete requisition first."
        //        };
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }

        //}

        [HttpPost]
        public JsonResult GetLocationbyUserMas(int UserMasId)
        {
            List<SelectListItem> locationList = new List<SelectListItem>();

            List<SelectListItem> prodCatList = new List<SelectListItem>();

            var items = db.UserDet.Where(x => x.UserMasId == UserMasId).ToList();



            foreach (var x in items)
            {
                locationList.Add(new SelectListItem { Text = x.Location.Name, Value = x.Location.Id.ToString() });
            }

            //foreach (var x in items)
            //{
            var catList = (from userDet in db.UserDet
                           join item in db.Item on userDet.ItemId equals item.Id
                           where userDet.ItemId == item.Id
                           select item.ProductCategory).Distinct().ToList();
            //  prodCatList.Add(new SelectListItem { Text = x..Name, Value = x.Location.Id.ToString() });
            //}

            var result = new
            {
                locationList = locationList,
                catList = catList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
            //return Json(locationList, JsonRequestBehavior.AllowGet);
        }


        
        public JsonResult DeletePurchaseOrders(int purchaseOrderId)
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



            var itemFind = db.PurchaseOrder.ToList().Find(x => x.Id == purchaseOrderId);
            
            if (itemFind.Status==0)
            {
                itemFind.isDeleted = 0;
                db.Entry(itemFind).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var inventory = db.Inventory.FirstOrDefault(x => x.CompanyId == itemFind.CompanyId && x.ItemId == itemFind.ItemId);
                inventory.Quantity = inventory.Quantity - itemFind.OrderQuantity;
                var findItem = db.Inventory.Find(inventory.Id);
                findItem.Quantity = inventory.Quantity;
                db.Entry(findItem).State = EntityState.Modified;

                //isDeleted 1 means not deleted 0 means deleted
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
        }
    }
