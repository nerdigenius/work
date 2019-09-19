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
    public class ProductCategoriesController : Controller
    {
        private ModelERP db = new ModelERP();
        public JsonResult GetProductCat()
        {
            var prodCatList = db.ProductCategory.OrderBy(x => x.Name).Select(y => new { Name = y.Name, Id = y.Id }).ToList();

            var prodCatWiseItemList = db.Item.OrderBy(x => x.Name).Select(y => new { Name = y.Name, Id = y.Id, ProductCategoryId = y.ProductCategoryId }).ToList();

            var locationList = db.Location.OrderBy(x => x.Name).Select(y => new { Name = y.Name, Id = y.Id }).ToList();
            //return Json(data, JsonRequestBehavior.AllowGet);

            var result = new
            {
                prodCatList = prodCatList,
                prodCatWiseItemList = prodCatWiseItemList,
                locationList = locationList
            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetItemsUnderCategory(int Id)
        {
            var prodCatItem = db.Item.Where(x => x.ProductCategoryId == Id).ToList();
            return Json(prodCatItem, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetLocation()
        {
            var data = db.Location.OrderBy(x => x.Name).Select(y => new { Name = y.Name, Id = y.Id }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        // GET: ProductCategories
        public ActionResult Index()
        {
            var productCategory = db.ProductCategory.Include(p => p.Unit);
            return View(productCategory.ToList());
        }

        // GET: ProductCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // GET: ProductCategories/Create
        public ActionResult Create()
        {
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name");
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UnitId,Name")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductCategory.Add(productCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", productCategory.UnitId);
            return View(productCategory);
        }

        // GET: ProductCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", productCategory.UnitId);
            return View(productCategory);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UnitId,Name")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitId = new SelectList(db.Unit, "Id", "Name", productCategory.UnitId);
            return View(productCategory);
        }

        // GET: ProductCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productCategory = db.ProductCategory.Find(id);
            db.ProductCategory.Remove(productCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        //[HttpGet]
        public JsonResult GetItemsUnderCat(int ProductCategoryId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();


            var items = db.Item.Where(x => x.ProductCategoryId == ProductCategoryId).ToList();


            foreach (var x in items)
            {
                itemList.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            }


            return Json(itemList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetUnitsUnderCat(int ProductCategoryId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();


            var items = db.ProductCategory.Where(x => x.Id == ProductCategoryId).ToList();


            foreach (var x in items)
            {
                itemList.Add(new SelectListItem { Text = x.Unit.Name, Value = x.UnitId.ToString() });
            }


            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProductCategory(int prodId)
        {

            //var checkitemInPurchase = db.PurchaseOrder.Where(x => x.Item.ProductCategoryId == prodId).ToList();
            //var checkitemInSales = db.SalesOrder.Where(x => x.Item.ProductCategoryId == prodId).ToList();
            //var checkitemInTransportOrder = db.TransportOrder.Where(x => x.Item.ProductCategoryId == prodId).ToList();
            //var checkUserDet = db.UserDet.Where(x => x.Item.ProductCategoryId == prodId).ToList();

            var checkItem = (from prodCat in db.ProductCategory
                             join purchase in db.PurchaseOrder on prodCat.Id equals purchase.Item.ProductCategoryId into ct
                             from purchaseLeft in ct.DefaultIfEmpty()

                             join sales in db.SalesOrder on prodCat.Id equals sales.Item.ProductCategoryId into ps
                             from salesLeft in ps.DefaultIfEmpty()

                             join transport in db.TransportOrder on prodCat.Id equals transport.Item.ProductCategoryId into ts
                             from transportLeft in ts.DefaultIfEmpty()

                             join userDet in db.UserDet on prodCat.Id equals userDet.Item.ProductCategoryId into ud
                             from userDetLeft in ts.DefaultIfEmpty()

                             where userDetLeft.Id == prodId || salesLeft.Item.ProductCategoryId == prodId || transportLeft.Item.ProductCategoryId == prodId
                             select purchaseLeft
                            ).ToList();
            if (checkItem.Count == 0)
            {
                bool flag = false;
                try
                {
                    var item = db.ProductCategory.Where(x => x.Id == prodId);
                    var prodCat = db.Item.Where(x => x.ProductCategoryId == prodId);
                   
                    db.ProductCategory.RemoveRange(item);
                    db.Item.RemoveRange(prodCat);

                    flag = db.SaveChanges() > 0;
                }
                catch
                {

                }

                if (flag)
                {
                    var result = new
                    {
                        flag = true,
                        message = "Product Category deletion successful."
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    var result = new
                    {
                        flag = false,
                        message = "Product Category deletion failed!\nError Occured."
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                var result = new
                {
                    flag = false,
                    message = "Product Category deletion failed!\nThis Category has been used!"
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
