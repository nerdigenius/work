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
    public class RetailerController : AlertController
    {
        private ModelERP db = new ModelERP();

        // GET: VendorRetailer
        public ActionResult Index()
        {
            var userMas = db.UserMas.Include(u => u.Employee).Where(x => x.UserType == 1);
            return View(userMas.ToList());
        }

        // GET: VendorRetailer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMas userMas = db.UserMas.Find(id);
            if (userMas == null)
            {
                return HttpNotFound();
            }
            return View(userMas);
        }

        // GET: VendorRetailer/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        //// POST: VendorRetailer/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Phone,Email,UserType,ContactPerson,EmployeeId")] UserMas userMas)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.UserMas.Add(userMas);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", userMas.EmployeeId);
        //    return View(userMas);
        //}



        public JsonResult SaveVendorRetailerOrder(IEnumerable<VMVendorRetailerDet> OrderDetails, VMVendorRetailerMas OrderMas)
        {
            var result = new
            {
                flag = false,
                message = "Error occured. !",
                Id = 0
            };

            try
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        var OrderM = new UserMas()
                        {
                            Id = 0,
                            Name = OrderMas.Name,
                            Phone = OrderMas.Phone,
                            Email = OrderMas.Email,
                            UserType = 1,
                            ContactPerson = OrderMas.ContactPerson,
                            EmployeeId = OrderMas.EmployeeId,
                            InitialBalance = OrderMas.InitialBalance
                            //Type = OrderMas.ProdDepartmentId,

                        };

                        db.UserMas.Add(OrderM);
                        db.SaveChanges();

                        Dictionary<int, int> dictionary =
                                new Dictionary<int, int>();






                        if (OrderDetails != null)
                        {
                            foreach (var item in OrderDetails)
                            {


                                //var OrderLocation = new Location()
                                //{
                                //    Id = 0,
                                //    Name = item.Location,
                                //    Cost = item.CarryingCost

                                //};

                                //db.Location.Add(OrderLocation);
                                //db.SaveChanges();

                                //var OrderRetailerItems = new VendorRetailerItems()
                                //{
                                //    Id = 0,
                                //    UserMasId = OrderM.Id,
                                //    ItemId = item.ItemId,
                                //    ItemCost = item.UnitPrice


                                //};

                                //db.VendorRetailerItems.Add(OrderRetailerItems);
                                //db.SaveChanges();


                                var OrderD = new UserDet()
                                {
                                    Id = 0,
                                    UserMasId = OrderM.Id,
                                    ItemId = item.ItemId,
                                    LocationId = item.LocationId,
                                    UnitPrice = item.UnitPrice,
                                    CarryingCost = item.CarryingCost


                                };

                                db.UserDet.Add(OrderD);
                                db.SaveChanges();

                                dictionary.Add(item.TempOrderDetId, OrderD.Id);

                            }
                        }


                        dbContextTransaction.Commit();

                        result = new
                        {
                            flag = true,
                            message = "Saving successful!!",
                            Id = OrderM.Id
                        };

                        Success("Record saved successfully.", true);


                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();

                        result = new
                        {
                            flag = false,
                            message = ex.Message,
                            Id = 0
                        };
                    }
                }

            }
            catch (Exception ex)
            {

                result = new
                {
                    flag = false,
                    message = ex.Message,
                    Id = 0
                };
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }






        public JsonResult UpdateVendorRetailerOrder(IEnumerable<VMVendorRetailerDet> OrderDetails, VMVendorRetailerMas OrderMas, int[] DelItems)
        {
            var result = new
            {
                flag = false,
                message = "Error occured. !"
            };

            //return Json(result, JsonRequestBehavior.AllowGet);

            try
            {
                var OpDate = DateTime.Now;
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        var OrderM = db.UserMas.Find(OrderMas.Id);

                        if (OrderM == null)
                        {
                            result = new
                            {
                                flag = false,
                                message = "Invalid Order Id. Saving failed !"
                            };

                            return Json(result, JsonRequestBehavior.AllowGet);
                        }

                        OrderM.Name = OrderMas.Name;
                        OrderM.Phone = OrderMas.Phone;
                        OrderM.Email = OrderMas.Email;
                        OrderM.ContactPerson = OrderMas.ContactPerson;
                        OrderM.EmployeeId = OrderMas.EmployeeId;

                        db.Entry(OrderM).State = EntityState.Modified;
                        db.SaveChanges();

                        Dictionary<int, int> dictionary =
                                new Dictionary<int, int>();
                        if (OrderDetails != null)
                        {
                            foreach (var item in OrderDetails)
                            {



                                //var detailIds = (from userDet in db.UserDet
                                //                  join vendorRetailer in db.VendorRetailerItems on userDet.VendorRetailerItemsId equals vendorRetailer.Id
                                //                  where vendorRetailer.UserMasId == OrderM.Id
                                //                  select userDet).SingleOrDefault().LocationId;

                                //var vendorRetId = db.VendorRetailerItems.FirstOrDefault(x => x.UserMasId == OrderM.Id).Id;

                                //var locId = db.UserDet.FirstOrDefault(x => x.VendorRetailerItemsId == vendorRetId).LocationId;

                                //var OrderLocation = new Location()
                                //{
                                //    Id = item.LocationId,
                                //    Name = db.Location.SingleOrDefault(x=>x.Id == item.LocationId).Name,
                                //    Cost = item.CarryingCost,


                                //};


                                //Location locations = new Location();
                                //var locationName = db.Location.Find(item.LocationId).Name;
                                //locations.Id = item.LocationId;
                                //locations.Name = item.Location;
                                //locations.Cost = item.CarryingCost;

                                //db.Entry(locations).State = locations.Id == 0 ?
                                //                           EntityState.Added :
                                //                           EntityState.Modified;



                                //var OrderRetailerItems = new VendorRetailerItems()
                                //{
                                //    Id = item.VendorRetailerId,
                                //    UserMasId = OrderM.Id,
                                //    ItemId = item.ItemId,
                                //    ItemCost = item.UnitPrice


                                //};
                                //db.Entry(OrderRetailerItems).State = OrderRetailerItems.Id == 0 ?
                                //                         EntityState.Added :
                                //                         EntityState.Modified;


                                var OrderD = new UserDet()
                                {

                                    Id = item.Id,
                                    //VendorRetailerItemsId = OrderRetailerItems.Id,
                                    UserMasId = OrderM.Id,
                                    ItemId = item.ItemId,
                                    LocationId = item.LocationId,
                                    UnitPrice = item.UnitPrice,
                                    CarryingCost = item.CarryingCost


                                };



                                db.Entry(OrderD).State = OrderD.Id == 0 ?
                                                            EntityState.Added :
                                                            EntityState.Modified;

                                //db.BuyerOrderDets.Add(OrderD);
                                db.SaveChanges();

                                dictionary.Add(item.TempOrderDetId, OrderD.Id);

                            }
                        }



                        //if (DelItems != null)
                        //{
                        //    foreach (var item in DelItems)
                        //    {

                        //        var userDetDel = db.UserDet.SingleOrDefault(x => x.UserMasId == item);
                        //        db.UserDet.Remove(userDetDel);
                        //        db.SaveChanges();
                        //    }
                        //}


                        dbContextTransaction.Commit();

                        result = new
                        {
                            flag = true,
                            message = "Update successful !!"
                        };

                        Success("Updated successfully.", true);


                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();

                        result = new
                        {
                            flag = false,
                            message = ex.Message
                        };
                    }
                }

            }
            catch (Exception ex)
            {

                result = new
                {
                    flag = false,
                    message = ex.Message
                };
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: VendorRetailer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMas userMas = db.UserMas.Find(id);
            if (userMas == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", userMas.EmployeeId);
            return View(userMas);
        }

        // POST: VendorRetailer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Email,UserType,ContactPerson,EmployeeId")] UserMas userMas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userMas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", userMas.EmployeeId);
            return View(userMas);
        }

        // GET: VendorRetailer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMas userMas = db.UserMas.Find(id);
            if (userMas == null)
            {
                return HttpNotFound();
            }
            return View(userMas);
        }

        // POST: VendorRetailer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserMas userMas = db.UserMas.Find(id);
            db.UserMas.Remove(userMas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public JsonResult GetDetailData(int Id)
        {
            var list = (from userMasVendor in db.UserMas
                            //join vendorRetailerItems in db.VendorRetailerItems on userMasVendor.Id equals vendorRetailerItems.UserMasId
                        join userDetVendor in db.UserDet on userMasVendor.Id equals userDetVendor.UserMasId
                        where userDetVendor.UserMasId == Id
                        select new { userDetVendor, userMasVendor }).AsEnumerable()
                       .Select(x => new
                       {
                           Id = x.userDetVendor.Id,
                           ProductCategoryId = x.userDetVendor.Item.ProductCategoryId,
                           ProductCategoryName = x.userDetVendor.Item.ProductCategory.Name,
                           ItemId = x.userDetVendor.ItemId,
                           ItemName = x.userDetVendor.Item.Name,
                           UnitPrice = x.userDetVendor.UnitPrice,
                           LocationId = x.userDetVendor.LocationId,
                           LocationName = x.userDetVendor.Location.Name,
                           CarryingCost = x.userDetVendor.CarryingCost,
                           VendorRetailerId = x.userDetVendor.Id
                       });


            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductCategoryNames(int Id)
        {

            var userMasIdWiseProductCat = db.UserDet.Where(x => x.UserMasId == Id);
            var data = userMasIdWiseProductCat.Select(y => new { Name = y.Item.ProductCategory.Name, Id = y.Item.ProductCategoryId }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public JsonResult DeleteRetailer(int id)
        {
            bool flag = false;
            try
            {

                var findRetailer = db.SalesOrder.Where(x => x.UserMasId == id).ToList();
                var itemToDeleteMas = db.UserMas.Find(id);
                var itemsToDeleteDet = db.UserDet.Where(x => x.UserMasId == id);


                if (findRetailer.Count == 0)
                {
                    foreach (var item in itemsToDeleteDet)
                    {
                        var itemsToDelete = db.UserDet.Where(x => x.UserMasId == item.UserMasId);
                        db.UserDet.RemoveRange(itemsToDelete);
                    }

                    db.UserMas.Remove(itemToDeleteMas);

                    flag = db.SaveChanges() > 0;

                }
            }
            catch
            {

            }

            if (flag)
            {
                var result = new
                {
                    flag = false,
                    message = "Deletion successful."
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new
                {
                    flag = false,
                    message = "Retailer is used in Sales Order."
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
