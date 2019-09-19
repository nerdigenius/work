//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using ERP.Models;
//using ERP.ViewModels;

//namespace ERP.Controllers
//{
//    public class PurchaseMasController : AlertController
//    {
//        private ModelERP db = new ModelERP();

//        // GET: PurchaseMas
//        public ActionResult Index()
//        {
//            var purchaseMas = db.PurchaseMas.Include(p => p.Agreements).Include(p => p.UserRole);
//            return View(purchaseMas.ToList());
//        }

//        // GET: PurchaseMas/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            PurchaseMas purchaseMas = db.PurchaseMas.Find(id);
//            if (purchaseMas == null)
//            {
//                return HttpNotFound();
//            }
//            return View(purchaseMas);
//        }

//        // GET: PurchaseMas/Create
//        public ActionResult Create()
//        {
//            ViewBag.AgreementsId = new SelectList(db.Agreements, "Id", "AgreementBox");
//            ViewBag.UserRoleId = new SelectList(db.UserRoles, "Id", "Name");
//            return View();
//        }

//        // POST: PurchaseMas/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public ActionResult Create([Bind(Include = "Id,UserRoleId,AgreementsId,OrderDate,VendorRef,Status,Type")] PurchaseMas purchaseMas)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        db.PurchaseMas.Add(purchaseMas);
//        //        db.SaveChanges();
//        //        return RedirectToAction("Index");
//        //    }

//        //    ViewBag.AgreementsId = new SelectList(db.Agreements, "Id", "AgreementBox", purchaseMas.AgreementsId);
//        //    ViewBag.UserRoleId = new SelectList(db.UserRoles, "Id", "Name", purchaseMas.UserRoleId);
//        //    return View(purchaseMas);
//        //}




//        public JsonResult SavePurchaseOrder(IEnumerable<VMPuchaseOrderDetEdit> OrderDetails, VMPurchaseOrderMasEdit OrderMas)
//        {
//            var result = new
//            {
//                flag = false,
//                message = "Error occured. !",
//                Id = 0
//            };

//            try
//            {
//                 using (var dbContextTransaction = db.Database.BeginTransaction())
//                {
//                    try
//                    {

//                        var OrderM = new PurchaseMas()
//                        {
//                            Id = 0,
//                            UserRoleId = OrderMas.UserRoleId,
//                            AgreementsId = OrderMas.AgreementsId,
//                            OrderDate = OrderMas.OrderDate,
//                            VendorRef = OrderMas.VendorRef,
//                            Status = 0
//                            //Type = OrderMas.ProdDepartmentId,
                          
//                        };

//                        db.PurchaseMas.Add(OrderM);
//                        db.SaveChanges();

//                        Dictionary<int, int> dictionary =
//                                new Dictionary<int, int>();


//                        if (OrderDetails != null)
//                        {
//                            foreach (var item in OrderDetails)
//                            {
//                                var OrderD = new PurchaseDet()
//                                {
//                                    Id = 0,
//                                    PurchaseMasId = OrderM.Id,
//                                    ProductCategoryId = item.ProductCategoryId,
//                                    ItemId = item.ItemId,
//                                    LocationId = item.LocationId,
//                                    TransportId = item.TransportId,
//                                    ScheduleDate = item.ScheduleDate,
//                                    Quantity = item.Quantity,
//                                    Tax = item.Tax
                                  
//                                };

//                                db.PurchaseDet.Add(OrderD);
//                                db.SaveChanges();

//                                dictionary.Add(item.TempOrderDetId, OrderD.Id);

//                            }
//                        }
                   

//                        dbContextTransaction.Commit();

//                        result = new
//                        {
//                            flag = true,
//                            message = "Saving successful!!",
//                            Id = OrderM.Id
//                        };

//                        Success("Record saved successfully.", true);


//                    }
//                    catch (Exception ex)
//                    {
//                        dbContextTransaction.Rollback();

//                        result = new
//                        {
//                            flag = false,
//                            message = ex.Message,
//                            Id = 0
//                        };
//                    }
//                }

//            }
//            catch (Exception ex)
//            {

//                result = new
//                {
//                    flag = false,
//                    message = ex.Message,
//                    Id = 0
//                };
//            }


//            return Json(result, JsonRequestBehavior.AllowGet);
//        }





//        // GET: PurchaseMas/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            PurchaseMas purchaseMas = db.PurchaseMas.Find(id);
//            if (purchaseMas == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.AgreementsId = new SelectList(db.Agreements, "Id", "AgreementBox", purchaseMas.AgreementsId);
//            ViewBag.UserRoleId = new SelectList(db.UserRoles, "Id", "Name", purchaseMas.UserRoleId);
//            return View(purchaseMas);
//        }

//        // POST: PurchaseMas/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,UserRoleId,AgreementsId,OrderDate,VendorRef,Status,Type")] PurchaseMas purchaseMas)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(purchaseMas).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.AgreementsId = new SelectList(db.Agreements, "Id", "AgreementBox", purchaseMas.AgreementsId);
//            ViewBag.UserRoleId = new SelectList(db.UserRoles, "Id", "Name", purchaseMas.UserRoleId);
//            return View(purchaseMas);
//        }

//        // GET: PurchaseMas/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            PurchaseMas purchaseMas = db.PurchaseMas.Find(id);
//            if (purchaseMas == null)
//            {
//                return HttpNotFound();
//            }
//            return View(purchaseMas);
//        }

//        // POST: PurchaseMas/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            PurchaseMas purchaseMas = db.PurchaseMas.Find(id);
//            db.PurchaseMas.Remove(purchaseMas);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
