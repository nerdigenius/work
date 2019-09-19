﻿using DhaliProcurement.Helper;
using DhaliProcurement.Models;
using DhaliProcurement.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DhaliProcurement.Controllers
{
    [Authorize]
    public class MaterialsEntryController : Controller
    {
        private DCPSContext db = new DCPSContext();
        public ActionResult Index(int? ProjectId, int? SiteId)
        {
            var MarterialsEntry = db.Proc_MaterialEntryMas.ToList();

            var procprojects = db.ProcProject.ToList();
            List<ProjectSite> sites = new List<ProjectSite>();
            foreach (var i in procprojects)
            {
                var site = db.ProjectSite.FirstOrDefault(x => x.Id == i.ProjectSiteId);
                sites.Add(site);
            }

            List<Project> projects = new List<Project>();
            foreach (var i in sites)
            {
                var proj = db.Project.FirstOrDefault(x => x.Id == i.ProjectId);
                projects.Add(proj);
            }

            ViewBag.ProjectId = new SelectList(projects, "Id", "Name");
            ViewBag.SiteId = new SelectList(sites, "Id", "Name");

            if (ProjectId != null && SiteId != null)
            {

                MarterialsEntry = MarterialsEntry.Where(x => x.ProcProject.ProjectSiteId == SiteId && x.ProcProject.ProjectSite.ProjectId == ProjectId).ToList();
                ViewBag.SiteId = new SelectList(db.ProjectSite.Where(y => y.ProjectId == ProjectId), "Id", "Name");
                return View(MarterialsEntry);

            }
            else if (ProjectId != null)
            {
                MarterialsEntry = MarterialsEntry.Where(x => x.ProcProject.ProjectSite.ProjectId == ProjectId).ToList();
                ViewBag.SiteId = new SelectList(db.ProjectSite.Where(y => y.ProjectId == ProjectId), "Id", "Name");
                return View(MarterialsEntry);
            }

            else
            {
                return View(MarterialsEntry.ToList());
            }

        }

        public ActionResult Create()
        {
            //ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name");
            //ViewBag.SiteId = new SelectList(db.ProjectSite, "Id", "Name");

            var EntryProject = (from purchaseMas in db.Proc_PurchaseOrderMas
                                    //   join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                                join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                                join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                                join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                                join project in db.Project on site.ProjectId equals project.Id
                                where purchaseMas.Proc_TenderMasId == tenderMas.Id && purchaseMas.VendorId == tenderDet.VendorId && tenderDet.Status == "A"
                                select project).Distinct().ToList();

            var procprojects = db.ProcProject.ToList();
            List<ProjectSite> sites = new List<ProjectSite>();
            foreach (var i in procprojects)
            {
                var site = db.ProjectSite.FirstOrDefault(x => x.Id == i.ProjectSiteId);
                sites.Add(site);
            }

            List<Project> projects = new List<Project>();

            foreach (var i in sites)
            {
                var proj = db.Project.FirstOrDefault(x => x.Id == i.ProjectId);
                projects.Add(proj);
            }

            //var requisitionProjects = from requisitionMas in db.Proc_RequisitionMas
            //                          join procproject in db.ProcProject on requisitionMas.ProcProjectId equals procproject.Id
            //                          //join site in db.ProjectSite on procproject.ProjectSiteId equals site.Id
            //                          //join project in db.Project on site.ProjectId equals project.Id
            //                          where requisitionMas.ProcProjectId == procproject.Id
            //                          select procproject;

            ViewBag.ProjectId = new SelectList(EntryProject.Distinct(), "Id", "Name");
            //ViewBag.ProjectId = new SelectList(projects.Distinct(), "Id", "Name");
            ViewBag.SiteId = new SelectList(sites, "Id", "Name");

            List<SelectListItem> ItemList = new List<SelectListItem>();
            var items = (from requisitionDet in db.Proc_RequisitionDet
                         join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                         join procproject in db.ProcProject on requisitionMas.ProcProjectId equals procproject.Id
                         join procProjectItem in db.ProcProjectItem on procproject.Id equals procProjectItem.ProcProjectId
                         join site in db.ProjectSite on procproject.ProjectSiteId equals site.Id
                         join project in db.Project on site.ProjectId equals project.Id
                         where requisitionDet.ItemId == procProjectItem.ItemId
                         select procProjectItem).ToList();

            foreach (var x in items)
            {
                var itemName = db.Item.SingleOrDefault(m => m.Id == x.ItemId);
                ItemList.Add(new SelectListItem { Text = itemName.Name, Value = x.ItemId.ToString() });
            }
            //var items = from requisitionDet in db.Proc_RequisitionDet

            ViewBag.ItemName = ItemList;

            //ViewBag.ItemName = new SelectList(itemName, "Id", "Name");
            ViewBag.ReqNo = new SelectList(db.Proc_RequisitionMas, "Id", "RCode");
            ViewBag.PONo = new SelectList(db.Proc_PurchaseOrderMas, "Id", "PONo");
            //ViewBag.VendorId = new SelectList(db.Vendor, "Id", "Name");
            return View();
        }

        public ActionResult Details(int MaterialEntryMasId)
        {
            ViewBag.MaterialEntryMasId = MaterialEntryMasId;
            var id = (from entryMas in db.Proc_MaterialEntryMas
                      join procProject in db.ProcProject on entryMas.ProcProjectId equals procProject.Id
                      join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                      join project in db.Project on site.ProjectId equals project.Id
                      where entryMas.Id == MaterialEntryMasId
                      select new { site, project }).FirstOrDefault();

            ViewBag.ProjectId = id.project.Id;
            ViewBag.ProjectName = id.project.Name;

            ViewBag.SiteId = id.site.Id;
            ViewBag.SiteName = id.site.Name;

            VMMaterialsEntryMasterDetail vm = new VMMaterialsEntryMasterDetail();
            vm.proc_MaterialEntryMas = db.Proc_MaterialEntryMas.SingleOrDefault(x => x.Id == MaterialEntryMasId);
            List<Proc_MaterialEntryDet> tenderList = new List<Proc_MaterialEntryDet>();
            var TenderDetails = db.Proc_MaterialEntryDet.Where(x => x.Proc_MaterialEntryMasId == MaterialEntryMasId).ToList();
            foreach (var item in TenderDetails)
            {
                tenderList.Add(item);
            }
            vm.proc_MaterialEntryDet = tenderList;
            return View(vm);
        }


        public ActionResult Edit(int MaterialEntryMasId)
        {
            ViewBag.MaterialEntryMasId = MaterialEntryMasId;
            var id = (from entryMas in db.Proc_MaterialEntryMas
                      join procProject in db.ProcProject on entryMas.ProcProjectId equals procProject.Id
                      join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                      join project in db.Project on site.ProjectId equals project.Id
                      where entryMas.Id == MaterialEntryMasId
                      select new { site, project }).FirstOrDefault();

            ViewBag.ProjectId = id.project.Id;
            ViewBag.ProjectName = id.project.Name;

            ViewBag.SiteId = id.site.Id;
            ViewBag.SiteName = id.site.Name;

            List<SelectListItem> ItemList = new List<SelectListItem>();

            //var items = (from procProjectItem in db.ProcProjectItem
            //             join procproject in db.ProcProject on procProjectItem.ProcProjectId equals procproject.Id
            //             join site in db.ProjectSite on procproject.ProjectSiteId equals site.Id
            //             join project in db.Project on site.ProjectId equals project.Id
            //             where project.Id == id.project.Id && site.Id == id.site.Id
            //             select procProjectItem).ToList();

            var items = (from purchaseMas in db.Proc_PurchaseOrderMas
                         join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                         join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                         join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                         join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                         join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                         join procproject in db.ProcProject on requisitionMas.ProcProjectId equals procproject.Id
                         join procProjectItem in db.ProcProjectItem on procproject.Id equals procProjectItem.ProcProjectId
                         join site in db.ProjectSite on procproject.ProjectSiteId equals site.Id
                         join project in db.Project on site.ProjectId equals project.Id
                         where project.Id == id.project.Id && site.Id == id.site.Id && purchaseDet.ItemId == procProjectItem.ItemId
                         select procProjectItem).Distinct().ToList();

            foreach (var x in items)
            {
                var itemName = db.Item.SingleOrDefault(m => m.Id == x.ItemId);
                ItemList.Add(new SelectListItem { Text = itemName.Name, Value = x.ItemId.ToString() });
            }
            //var items = from requisitionDet in db.Proc_RequisitionDet

            ViewBag.ItemName = ItemList;
            ViewBag.PONo = new SelectList(db.Proc_PurchaseOrderMas, "Id", "PONo");
            ViewBag.ReqNo = new SelectList(db.Proc_RequisitionMas, "Id", "RCode");
            ViewBag.MaterialEntryMasId = MaterialEntryMasId;

            VMMaterialsEntryMasterDetail vm = new VMMaterialsEntryMasterDetail();
            vm.proc_MaterialEntryMas = db.Proc_MaterialEntryMas.SingleOrDefault(x => x.Id == MaterialEntryMasId);
            List<Proc_MaterialEntryDet> tenderList = new List<Proc_MaterialEntryDet>();
            var TenderDetails = db.Proc_MaterialEntryDet.Where(x => x.Proc_MaterialEntryMasId == MaterialEntryMasId).ToList();
            foreach (var item in TenderDetails)
            {
                tenderList.Add(item);
            }
            vm.proc_MaterialEntryDet = tenderList;
            return View(vm);
        }





        [HttpPost]
        public JsonResult GetSites(int ProjectId)
        {
            List<SelectListItem> siteList = new List<SelectListItem>();

            //var projects = db.Project.SingleOrDefault(x => x.Id == ProjectId);

            var projects = (from purchaseMas in db.Proc_PurchaseOrderMas
                            join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                            join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                            join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                            join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                            join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                            join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                            //join project in db.Project on site.ProjectId equals project.Id
                            where tenderMas.isApproved == "A" && site.ProjectId == ProjectId
                            select site).Distinct().ToList();

            //var sites = db.ProjectSite.Where(x => x.ProjectId == ProjectId).ToList();
            var projectResources = db.ProjectResource.SingleOrDefault(x => x.ProjectId == ProjectId);
            var projectManager = projectResources.CompanyResource.Name;


            foreach (var x in projects)
            {
                siteList.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            }

            var result = new
            {
                manager = projectManager,
                Sites = siteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetSitesForReport(int ProjectId)
        {
            List<SelectListItem> siteList = new List<SelectListItem>();

            //var projects = db.Project.SingleOrDefault(x => x.Id == ProjectId);

            var projects = (from materialEntry in db.Proc_MaterialEntryDet
                            join purchaseDet in db.Proc_PurchaseOrderDet on materialEntry.Proc_PurchaseOrderDetId equals purchaseDet.Id
                            join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                            join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                            join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                            join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                            join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                            join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                            join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                            where tenderMas.isApproved == "A" && site.ProjectId == ProjectId
                            select site).Distinct().ToList();

            //var sites = db.ProjectSite.Where(x => x.ProjectId == ProjectId).ToList();
            var projectResources = db.ProjectResource.SingleOrDefault(x => x.ProjectId == ProjectId);
            var projectManager = projectResources.CompanyResource.Name;


            foreach (var x in projects)
            {
                siteList.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            }

            var result = new
            {
                manager = projectManager,
                Sites = siteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetItemList(int SiteId, int ProjectId)
        {
            List<SelectListItem> ItemList = new List<SelectListItem>();

            var items = (from purchaseMas in db.Proc_PurchaseOrderMas
                         join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                         join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                         join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                         join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                         join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                         join procproject in db.ProcProject on requisitionMas.ProcProjectId equals procproject.Id
                         join procProjectItem in db.ProcProjectItem on procproject.Id equals procProjectItem.ProcProjectId
                         join site in db.ProjectSite on procproject.ProjectSiteId equals site.Id
                         join project in db.Project on site.ProjectId equals project.Id
                         where project.Id == ProjectId && site.Id == SiteId && purchaseDet.ItemId == procProjectItem.ItemId
                         select procProjectItem).Distinct().ToList();

            //var items = (from procProjectItem in db.ProcProjectItem
            //             join procproject in db.ProcProject on procProjectItem.ProcProjectId equals procproject.Id
            //             join site in db.ProjectSite on procproject.ProjectSiteId equals site.Id
            //             join project in db.Project on site.ProjectId equals project.Id
            //             where project.Id == ProjectId && site.Id == SiteId
            //             select procProjectItem).ToList();


            foreach (var x in items)
            {
                var itemName = db.Item.SingleOrDefault(m => m.Id == x.ItemId);
                ItemList.Add(new SelectListItem { Text = itemName.Name, Value = x.ItemId.ToString() });
            }


            var result = new
            {
                Items = ItemList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUnit(int itemId, int projectId, int siteId)
        {
            //var unit = (from procProjectItem in db.ProcProjectItem
            //            join units in db.Unit on procProjectItem.UnitId equals units.Id
            //            where procProjectItem.ItemId == itemId
            //            select units).FirstOrDefault();
            var unit = (from procProject in db.ProcProject
                        join ProcProjectItem in db.ProcProjectItem on procProject.Id equals ProcProjectItem.ProcProjectId
                        join units in db.Unit on ProcProjectItem.UnitId equals units.Id
                        where ProcProjectItem.ItemId == itemId && procProject.ProjectSiteId == siteId
                        select units).SingleOrDefault();

            var totalRequired = (from procProjectItem in db.ProcProjectItem
                                 join procproject in db.ProcProject on procProjectItem.ProcProjectId equals procproject.Id
                                 join site in db.ProjectSite on procproject.ProjectSiteId equals site.Id
                                 join project in db.Project on site.ProjectId equals project.Id
                                 where project.Id == projectId && site.Id == siteId && procProjectItem.ItemId == itemId
                                 select procProjectItem).FirstOrDefault();
            if (totalRequired == null)
            {
                totalRequired.PQuantity = 0;
            }

            List<SelectListItem> ReqList = new List<SelectListItem>();
            //var requisition = from requisitionDet in db.Proc_RequisitionDet
            //                  join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
            //                  join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
            //                  join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
            //                  where requisitionDet.ItemId == itemId && site.Id== siteId
            //                  select requisitionMas;

            var requisition = (from purchaseMas in db.Proc_PurchaseOrderMas
                               join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                               join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                               join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                               join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                               join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                               join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                               join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                               where requisitionDet.ItemId == itemId && site.Id == siteId && tenderDet.Proc_RequisitionDetId == requisitionDet.Id && tenderDet.Status == "A"
                               select requisitionMas).Distinct();

            foreach (var x in requisition)
            {
                ReqList.Add(new SelectListItem { Text = x.Rcode, Value = x.Id.ToString() });
            }

            var result = new
            {
                flag = true,
                UnitName = unit.Name,
                UnitId = unit.Id,
                ReqList = ReqList,
                TotalRequired = totalRequired.PQuantity
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetVendor(int itemId, int projectId, int siteId, int ReqNo, int PONo)
        {


            var vendors = (from purchaseDet in db.Proc_PurchaseOrderDet
                           join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                           //join entryDet in db.Proc_MaterialEntryDet on purchaseDet.Id equals entryDet.Proc_PurchaseOrderDetId
                           join vendor in db.Vendor on purchaseMas.VendorId equals vendor.Id
                           where purchaseMas.Id == PONo
                           select vendor).Distinct().FirstOrDefault();

            var POQty = db.Proc_PurchaseOrderDet.SingleOrDefault(x => x.ItemId == itemId && x.Proc_PurchaseOrderMas.Id == PONo);

            decimal total = 0;
            //var totalReceived = (from entryDet in db.Proc_MaterialEntryDet
            //                     join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
            //                     join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
            //                     join tenderDet in db.Proc_TenderDet on purchaseMas.Proc_TenderMasId equals tenderDet.Proc_TenderMasId
            //                     join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
            //                     join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
            //                     join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
            //                     join procProjectItem in db.ProcProjectItem on procProject.Id equals procProjectItem.ProcProjectId
            //                     //where purchaseDet.ItemId == item.ItemId
            //                     where entryDet.Proc_PurchaseOrderDet.ItemId == itemId && procProject.ProjectSiteId == siteId
            //                     select entryDet).Distinct().ToList();

            var totalReceived = (from entryDet in db.Proc_MaterialEntryDet
                                 join entryMas in db.Proc_MaterialEntryMas on entryDet.Proc_MaterialEntryMasId equals entryMas.Id
                                 join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                                 join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                                 join procProject in db.ProcProject on entryMas.ProcProjectId equals procProject.Id
                                 join procProjectItem in db.ProcProjectItem on procProject.Id equals procProjectItem.ProcProjectId
                                 where entryDet.Proc_PurchaseOrderDet.ItemId == itemId && procProject.ProjectSiteId == siteId
                                 select entryDet).Distinct().ToList();


            foreach (var i in totalReceived)
            {
                total = total + i.EntryQty;
            }



            var result = new
            {
                VendorId = vendors.Id,
                vendorName = db.Vendor.SingleOrDefault(x => x.Id == vendors.Id).Name,
                POQuantity = POQty.POQty,
                //POReceived = POQty.PORcv
                POReceived = total
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPO(int itemId, int projectId, int siteId, int ReqNo)
        {


            var PO = (from purMas in db.Proc_PurchaseOrderMas
                      join purDet in db.Proc_PurchaseOrderDet on purMas.Id equals purDet.Proc_PurchaseOrderMasId
                      join tenderMas in db.Proc_TenderMas on purMas.Proc_TenderMasId equals tenderMas.Id
                      join tenderDet in db.Proc_TenderDet on new { ColA = tenderMas.Id, ColB = purMas.VendorId } equals new { ColA = tenderDet.Proc_TenderMasId, ColB = tenderDet.VendorId } /*&& purMas.VendorId equals tenderDet.VendorId*/
                      join requisitionDet in db.Proc_RequisitionDet on new { ColA = tenderDet.Proc_RequisitionDetId, ColB = purDet.ItemId } equals new { ColA = requisitionDet.Id, ColB = requisitionDet.ItemId } /*&& purDet.ItemId equals requisitionDet.ItemId*/
                      join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                      join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                      where purDet.ItemId == itemId && requisitionMas.Id == ReqNo && procProject.ProjectSiteId == siteId
                      select purMas).Distinct().ToList();



            var totalMaterial = (from total in db.ProcProjectItem
                                 join procProject in db.ProcProject on total.ProcProjectId equals procProject.Id
                                 where procProject.ProjectSiteId == siteId && total.ItemId == itemId
                                 select total).FirstOrDefault();



            var requiredQty = (from requisitionDet in db.Proc_RequisitionDet
                               join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                               where requisitionMas.Id == ReqNo && requisitionDet.ItemId == itemId
                               select requisitionDet).FirstOrDefault();

            var unitPrice = (from tenderDet in db.Proc_TenderDet
                             join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                             join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                             where requisitionMas.Id == ReqNo && requisitionDet.ItemId == itemId
                             select tenderDet).FirstOrDefault();


            //decimal receivedQuantity = 0;
            //var totalReceived = (from entryDet in db.Proc_MaterialEntryDet
            //                     join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
            //                     join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
            //                     join tenderDet in db.Proc_TenderDet on purchaseMas.Proc_TenderMasId equals tenderDet.Proc_TenderMasId
            //                     where purchaseDet.ItemId == itemId
            //                     select entryDet).Distinct().ToList();


            //decimal receivedQuantity = 0;
            //if (totalReceived.Count == 0)
            //{
            //    receivedQuantity = 0;
            //}
            //else
            //{
            //    foreach (var i in totalReceived)
            //    {
            //        receivedQuantity = receivedQuantity + i.EntryQty;
            //    }

               
            //}

            List<SelectListItem> POList = new List<SelectListItem>();

            foreach (var x in PO)
            {
                POList.Add(new SelectListItem { Text = x.PONo, Value = x.Id.ToString() });
            }

            var result = new
            {
                TotalMaterial = totalMaterial.PQuantity,
                RemainingQty = requiredQty.ReqQty,
                UnitPrice = unitPrice.TQPrice,
                POs = POList
                //PreviousReceived = receivedQuantity

            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RebindModal(int ProjectId, int SiteId, int itemId,int RCode)
        {

            List<SelectListItem> ItemList = new List<SelectListItem>();

            var items = (from purchaseMas in db.Proc_PurchaseOrderMas
                         join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                         join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                         join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                         join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                         join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                         join procproject in db.ProcProject on requisitionMas.ProcProjectId equals procproject.Id
                         join procProjectItem in db.ProcProjectItem on procproject.Id equals procProjectItem.ProcProjectId
                         join site in db.ProjectSite on procproject.ProjectSiteId equals site.Id
                         join project in db.Project on site.ProjectId equals project.Id
                         where project.Id == ProjectId && site.Id == SiteId && purchaseDet.ItemId == procProjectItem.ItemId
                         select procProjectItem).Distinct().ToList();

            foreach (var x in items)
            {
                var itemName = db.Item.SingleOrDefault(m => m.Id == x.ItemId);
                ItemList.Add(new SelectListItem { Text = itemName.Name, Value = x.ItemId.ToString() });
            }



            var requisition = (from purchaseMas in db.Proc_PurchaseOrderMas
                               join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                               join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                               join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                               join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                               join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                               join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                               join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                               where requisitionDet.ItemId == itemId && site.Id == SiteId && tenderDet.Proc_RequisitionDetId == requisitionDet.Id && tenderDet.Status == "A"
                               select requisitionMas).Distinct();
            List<SelectListItem> ReqList = new List<SelectListItem>();

            foreach (var x in requisition)
            {
                ReqList.Add(new SelectListItem { Text = x.Rcode, Value = x.Id.ToString() });
            }


            var PO = (from requisitionDet in db.Proc_RequisitionDet
                      join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                      join tenderDet in db.Proc_TenderDet on requisitionDet.Id equals tenderDet.Proc_RequisitionDetId
                      join tenderMas in db.Proc_TenderMas on tenderDet.Proc_TenderMasId equals tenderMas.Id
                      join purchaseMas in db.Proc_PurchaseOrderMas on tenderMas.Id equals purchaseMas.Proc_TenderMasId
                      join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                      join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                      join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                      where requisitionMas.Id== RCode && purchaseDet.ItemId == itemId && site.Id == SiteId
                      select purchaseMas).Distinct().ToList();

            List<SelectListItem> POList = new List<SelectListItem>();

            foreach (var x in PO)
            {
                POList.Add(new SelectListItem { Text = x.PONo, Value = x.Id.ToString() });
            }


            var result = new
            {
                Items = ItemList,
                ReqList = ReqList,
                POs = POList
            };

            return Json(result, JsonRequestBehavior.AllowGet);

        }



        public JsonResult CreateMaterialsEntry(IEnumerable<VMMaterialsEntry> RequisitionItems, int ProjectId, int SiteId, string EDate)
        {
            var result = new
            {
                flag = false,
                message = "Requisition saving error !"
            };
            var flag = false;
            var EntryDate = DateTime.ParseExact(EDate, "dd/mm/yyyy", CultureInfo.CurrentCulture);
            var planList = (from procProject in db.ProcProject
                            join site in db.ProjectSite on procProject.ProjectSiteId equals SiteId
                            join project in db.Project on site.ProjectId equals ProjectId
                            join materialEntry in db.Proc_MaterialEntryMas on procProject.Id equals materialEntry.ProcProjectId
                            where project.Id == ProjectId && site.Id == SiteId && materialEntry.EDate == EntryDate
                            select procProject).ToList();

            if (planList.Count == 0)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        flag = false;
                        Proc_MaterialEntryMas master = new Proc_MaterialEntryMas();

                        var newProj = (from procProject in db.ProcProject
                                       join site in db.ProjectSite on procProject.ProjectSiteId equals SiteId
                                       join project in db.Project on site.ProjectId equals ProjectId
                                       where project.Id == ProjectId
                                       select procProject);

                        foreach (var i in newProj)
                        {
                            master.ProcProjectId = i.Id;
                        }

                        master.EDate = EntryDate;
                        db.Proc_MaterialEntryMas.Add(master);

                        flag = db.SaveChanges() > 0;

                        var EntryMasId = master.Id;


                        foreach (var item in RequisitionItems)
                        {
                            Proc_MaterialEntryDet detail = new Proc_MaterialEntryDet();
                            detail.Proc_MaterialEntryMasId = EntryMasId;
                            var purchaseOrderMasId = db.Proc_PurchaseOrderMas.FirstOrDefault(x => x.Id == item.PONo).Id;
                            detail.Proc_PurchaseOrderDetId = db.Proc_PurchaseOrderDet.FirstOrDefault(x => x.Proc_PurchaseOrderMasId == purchaseOrderMasId && x.ItemId == item.ItemId).Id;

                            var challanCheck = db.Proc_MaterialEntryDet.Where(x => x.ChallanNo.Trim() == item.ChallanNo.Trim()).ToList();
                            if (challanCheck.Count == 0)
                            {
                                detail.ChallanNo = item.ChallanNo;

                            }
                            else
                            {
                                result = new
                                {
                                    flag = false,
                                    message = item.ChallanNo + " -Challano No already exists!"
                                };
                                return Json(result, JsonRequestBehavior.AllowGet);
                            
                            }
                            
                            if (item.ChallanDate == null)
                            {
                                detail.ChallanDate = null;
                            }
                            else
                            {
                                //detail.ChallanDate = DateTime.ParseExact(item.ChallanDate, "dd/mm/yyyy", CultureInfo.CurrentCulture);
                                detail.ChallanDate = DateTime.ParseExact(item.ChallanDate, "dd/mm/yyyy", CultureInfo.CurrentCulture);
                            }

                            detail.EntryQty = item.EntryQty;
                            detail.Status = item.Status;

                            db.Proc_MaterialEntryDet.Add(detail);
                            flag = db.SaveChanges() > 0;
                            if (flag)
                            {
                                var PORcv = db.Proc_PurchaseOrderDet.Find(detail.Proc_PurchaseOrderDetId);
                                PORcv.PORcv = PORcv.PORcv + item.EntryQty;
                                db.Entry(master).State = EntityState.Modified;
                                flag = db.SaveChanges() > 0;
                            }

                        }

                        dbContextTransaction.Commit();

                        if (flag == true)
                        {
                            result = new
                            {
                                flag = true,
                                message = "Save Successful!"
                            };
                        }

                        //if (flag == false)
                        //{
                        //    var mas = db.Proc_MaterialEntryMas.Find(EntryMasId);
                        //    db.Proc_MaterialEntryMas.Remove(mas);
                        //    flag = db.SaveChanges() > 0;
                        //}
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        result = new
                        {

                            flag = false,
                            message = "Saving failed! Error occurred."
                            //message = ex.Message
                        };
                    }
                }
            }
            else
            {
                result = new
                {
                    flag = true,
                    message = "Entry exists agaist this date!"
                };
            }




            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteMaterialEntry(int MaterialEntryMasId)
        {
            //var EntryCount = (from entryMas in db.Proc_MaterialEntryMas
            //                       //join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
            //                   join paymentDet in db.Proc_PurchaseOrderMas on entryMas.Id equals paymentDet.Proc_EntryDetId
            //                   where entryMas.Id == MaterialEntryMasId
            //                  select entryMas).Distinct().Count();
            //if (EntryCount == 0)
            //{
            bool flag = false;

            var counter = 0;
            var entryDet = db.Proc_MaterialEntryDet.Where(x => x.Proc_MaterialEntryMasId == MaterialEntryMasId).ToList();
            foreach (var i in entryDet)
            {
                var check = db.Proc_VendorPaymentDet.Where(x => x.Proc_MaterialEntryDetId == i.Id).ToList();
                if (check.Count > 0)
                {
                    counter++;
                }
            }


            if (counter == 0)
            {
                var itemsToDeleteTask = db.Proc_MaterialEntryMas.Where(x => x.Id == MaterialEntryMasId);
                db.Proc_MaterialEntryMas.RemoveRange(itemsToDeleteTask);

                var itemsToDeletePlan = db.Proc_MaterialEntryDet.Where(x => x.Proc_MaterialEntryMasId == MaterialEntryMasId);
                db.Proc_MaterialEntryDet.RemoveRange(itemsToDeletePlan);

                flag = db.SaveChanges() > 0;
            }


            if (flag)
            {
                var result = new
                {
                    flag = true,
                    message = "Material Entry deletion successful."
                };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var result = new
                {
                    flag = false,
                    message = "Material Entry deletion failed!\nFirst delete Vendor Payment!."
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            //}
            //else
            //{
            //    var result = new
            //    {
            //        flag = false,
            //        message = "Tender deletion failed!\nDelete purchase order first."
            //    };
            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}

        }

        public JsonResult getEditMaterialEntry(int MaterialEntryMasId)
        {
            bool flag = false;
            var id = (from entryMas in db.Proc_MaterialEntryMas
                      join procProject in db.ProcProject on entryMas.ProcProjectId equals procProject.Id
                      join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                      join project in db.Project on site.ProjectId equals project.Id
                      where entryMas.Id == MaterialEntryMasId
                      select new { site, project }).SingleOrDefault();

            var check = db.Proc_MaterialEntryDet.Where(x => x.Proc_MaterialEntryMasId == MaterialEntryMasId).Count();
            if (check > 0)
            {
                flag = true;
                var data = db.Proc_MaterialEntryDet.Where(x => x.Proc_MaterialEntryMasId == MaterialEntryMasId).ToList();

                List<VMEditMaterialsEntryItem> detailsList = new List<VMEditMaterialsEntryItem>();
                foreach (var i in data)
                {
                    VMEditMaterialsEntryItem vm = new VMEditMaterialsEntryItem();

                    vm.Proc_MaterialEntryDetId = i.Id;
                    //var projectId = (from tenderDet in db.Proc_TenderDet
                    //                 join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                    //                 join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //                 join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                    //                 join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                    //                 join project in db.Project on site.ProjectId equals project.Id
                    //                 where tenderDet.Proc_TenderMasId == i.Proc_TenderMasId
                    //                 select project).FirstOrDefault();
                    //vm.ProjectId = projectId.Id;
                    //vm.ProjectName = projectId.Name;

                    //var siteId = (from tenderDet in db.Proc_TenderDet
                    //              join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                    //              join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //              join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                    //              join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                    //              where tenderDet.Proc_TenderMasId == TenderId
                    //              select site).FirstOrDefault();
                    //vm.SiteId = siteId.Id;
                    //vm.SitetName = siteId.Name;

                    vm.ChallanNo = i.ChallanNo;

                    //var date = NullHelper.DateToString(i.ChallanDate);
                    if (i.ChallanDate == null)
                    {
                        vm.ChallanDate = "";
                    }
                    else
                    {
                        vm.ChallanDate = NullHelper.DateToString(i.ChallanDate);
                    }
                    //vm.ChallanDate = i.ChallanDate;
                    vm.EntryQty = i.EntryQty;
                    vm.Status = i.Status;

                    //var ItemId = (from entryDet in db.Proc_MaterialEntryDet
                    //              join entryMas in db.Proc_MaterialEntryMas on entryDet.Proc_MaterialEntryMasId equals entryMas.Id
                    //              join procProject in db.ProcProject on entryMas.ProcProjectId equals procProject.Id
                    //              join procProjectItem in db.ProcProjectItem on procProject.Id equals procProjectItem.ProcProjectId
                    //              join items in db.Item on procProjectItem.ItemId equals items.Id
                    //              where entryDet.Proc_MaterialEntryMasId == MaterialEntryMasId
                    //              select items).FirstOrDefault();
                    var ItemId = (from entryDet in db.Proc_MaterialEntryDet
                                  join entryMas in db.Proc_MaterialEntryMas on entryDet.Proc_MaterialEntryMasId equals entryMas.Id
                                  join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                                  join procProject in db.ProcProject on entryMas.ProcProjectId equals procProject.Id
                                  join procProjectItem in db.ProcProjectItem on procProject.Id equals procProjectItem.ProcProjectId
                                  join items in db.Item on procProjectItem.ItemId equals items.Id
                                  where purchaseDet.Id == i.Proc_PurchaseOrderDetId && purchaseDet.ItemId == procProjectItem.ItemId
                                  select items).FirstOrDefault();

                    vm.ItemId = ItemId.Id;
                    vm.ItemName = ItemId.Name;

                    var requisitionId = (from entryDet in db.Proc_MaterialEntryDet
                                         join entryMas in db.Proc_MaterialEntryMas on entryDet.Proc_MaterialEntryMasId equals entryMas.Id
                                         join procProject in db.ProcProject on entryMas.ProcProjectId equals procProject.Id
                                         join procProjectItem in db.ProcProjectItem on procProject.Id equals procProjectItem.ProcProjectId
                                         join requisitionMas in db.Proc_RequisitionMas on procProject.Id equals requisitionMas.ProcProjectId
                                         where entryDet.Proc_MaterialEntryMasId == MaterialEntryMasId && procProjectItem.ItemId == ItemId.Id
                                         select requisitionMas).FirstOrDefault();

                    vm.RCodeName = requisitionId.Rcode;
                    vm.RCode = db.Proc_RequisitionMas.SingleOrDefault(x => x.Rcode == requisitionId.Rcode).Id;

                    var PONo = (from purchaseOrderMas in db.Proc_PurchaseOrderMas
                                join purchaseOrderDet in db.Proc_PurchaseOrderDet on purchaseOrderMas.Id equals purchaseOrderDet.Proc_PurchaseOrderMasId
                                join entryDet in db.Proc_MaterialEntryDet on purchaseOrderDet.Id equals entryDet.Proc_PurchaseOrderDetId
                                //where entryDet.Proc_MaterialEntryMas.Id == MaterialEntryMasId && purchaseOrderDet.ItemId == ItemId.Id
                                where entryDet.Proc_MaterialEntryMas.Id == MaterialEntryMasId && purchaseOrderDet.Id == i.Proc_PurchaseOrderDetId
                                select purchaseOrderMas).FirstOrDefault();

                    vm.PONo = PONo.Id;
                    vm.PONoName = PONo.PONo;

                    vm.PurchaseOrderDetId = db.Proc_PurchaseOrderDet.FirstOrDefault(x => x.Proc_PurchaseOrderMasId == PONo.Id).Id;


                    var getSites = (from materialEntry in db.Proc_MaterialEntryDet
                                    join purchaseDet in db.Proc_PurchaseOrderDet on materialEntry.Proc_PurchaseOrderDetId equals purchaseDet.Id
                                    join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                                    join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                    join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                    join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                                    join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                    join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                                    join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                                    where materialEntry.Id == i.Id && purchaseMas.Id == PONo.Id && requisitionMas.Id== requisitionId.Id
                                    select site).Distinct().FirstOrDefault();

                    //var unit = db.Unit.FirstOrDefault(x => x.Id == ItemId.Id);
                    //var unit = (from procProjectItem in db.ProcProjectItem
                    //            join units in db.Unit on procProjectItem.UnitId equals units.Id
                    //            where procProjectItem.ItemId == ItemId.Id
                    //            select units).FirstOrDefault();

                    var unit = (from procProject in db.ProcProject
                                join ProcProjectItem in db.ProcProjectItem on procProject.Id equals ProcProjectItem.ProcProjectId
                                join units in db.Unit on ProcProjectItem.UnitId equals units.Id
                                where ProcProjectItem.ItemId == ItemId.Id && procProject.ProjectSiteId == getSites.Id
                                select units).SingleOrDefault();
                    vm.UnitId = unit.Id;
                    vm.UnitName = unit.Name;

                    var vendors = (from purchaseDet in db.Proc_PurchaseOrderDet
                                   join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                                   //join entryDet in db.Proc_MaterialEntryDet on purchaseDet.Id equals entryDet.Proc_PurchaseOrderDetId
                                   join vendor in db.Vendor on purchaseMas.VendorId equals vendor.Id
                                   where purchaseMas.PONo == PONo.PONo
                                   select vendor).Distinct().FirstOrDefault();

                    //var vendor = (from requisitionDet in db.Proc_RequisitionDet
                    //              join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //              join tenderDet in db.Proc_TenderDet on requisitionDet.Id equals tenderDet.Proc_RequisitionDetId
                    //              where requisitionMas.Rcode == requisitionId.Rcode && requisitionDet.ItemId == ItemId.Id
                    //              select tenderDet).FirstOrDefault();

                    vm.VendorId = vendors.Id;
                    vm.VendorName = db.Vendor.SingleOrDefault(x => x.Id == vendors.Id).Name;

                    var totalMaterial = (from total in db.ProcProjectItem
                                         join procProject in db.ProcProject on total.ProcProjectId equals procProject.Id
                                         join requisitionMas in db.Proc_RequisitionMas on procProject.Id equals requisitionMas.ProcProjectId
                                         join requisitionDet in db.Proc_RequisitionDet on requisitionMas.Id equals requisitionDet.Proc_RequisitionMasId
                                         //where total.ItemId == ItemId.Id && procProject.ProjectSiteId == requisitionDet.Proc_RequisitionMas.ProcProject.ProjectSiteId
                                         where total.ItemId == i.Proc_PurchaseOrderDet.ItemId && procProject.ProjectSiteId == i.Proc_MaterialEntryMas.ProcProject.ProjectSiteId
                                         select total).FirstOrDefault();


                    var requiredQty = (from requisitionDet in db.Proc_RequisitionDet
                                       join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                       where requisitionMas.Rcode == requisitionId.Rcode && requisitionDet.ItemId == ItemId.Id
                                       select requisitionDet).FirstOrDefault();

                    //var unitPrice = (from tenderDet in db.Proc_TenderDet
                    //                 join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                    //                 join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //                 where requisitionMas.Rcode == requisitionId.Rcode && requisitionDet.ItemId == ItemId.Id

                    var unitPrice = (from purchaseDet in db.Proc_PurchaseOrderDet
                                     join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                                     join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                     join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                     join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                                     join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                     //where requisitionMas.Rcode == requisitionId.Rcode && purchaseDet.ItemId == ItemId.Id
                                     where purchaseDet.ItemId == ItemId.Id
                                     select tenderDet).FirstOrDefault();

                    vm.TotalMaterial = totalMaterial.PQuantity;


                    //var totalReceived = (from entryDet in db.Proc_MaterialEntryDet
                    //                     join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                    //                     join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                    //                     join tenderDet in db.Proc_TenderDet on purchaseMas.Proc_TenderMasId equals tenderDet.Proc_TenderMasId
                    //                     join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                    //                     join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //                     join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                    //                     join procProjectItem in db.ProcProjectItem on procProject.Id equals procProjectItem.ProcProjectId
                    //                     //where purchaseDet.ItemId == ItemId.Id
                    //                     where purchaseDet.ItemId == ItemId.Id && procProject.ProjectSiteId== id.site.Id
                    //                     select entryDet).Distinct().ToList();

                    var totalReceived = (from entryDet in db.Proc_MaterialEntryDet
                                         join entryMas in db.Proc_MaterialEntryMas on entryDet.Proc_MaterialEntryMasId equals entryMas.Id
                                         join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                                         join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                                         join procProject in db.ProcProject on entryMas.ProcProjectId equals procProject.Id
                                         join procProjectItem in db.ProcProjectItem on procProject.Id equals procProjectItem.ProcProjectId
                                         where entryDet.Proc_PurchaseOrderDet.ItemId == ItemId.Id && procProject.ProjectSiteId == id.site.Id
                                         select entryDet).Distinct().ToList();

                    decimal receivedQuantity = 0;
                    if (totalReceived == null)
                    {
                        receivedQuantity = 0;
                    }
                    else
                    {
                        foreach (var m in totalReceived)
                        {
                            receivedQuantity = receivedQuantity + m.EntryQty;
                        }

                        //receivedQuantity = totalrecv;
                    }


                    vm.PreviousReceivedQty = receivedQuantity;

                    vm.UnitPrice = unitPrice.TQPrice;
                    vm.Status = i.Status;
                    var editCheck = db.Proc_VendorPaymentDet.Where(x => x.Proc_MaterialEntryDetId == i.Id).ToList();

                    if (editCheck.Count == 0)
                    {
                        vm.Checkflag = 0;
                    }
                    else
                    {
                        vm.Checkflag = 1;
                    }

                    detailsList.Add(vm);
                }

                var result = new
                {
                    flag = flag,
                    List = detailsList
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new
                {
                    flag = flag
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult EditEntry(IEnumerable<VMMaterialsEntry> EntryItems, int?[] DeleteItems, int EntryMasId, int ProcProjectId, int ProjectId, string TNo, int SiteId, string EDate)
        {
            var result = new
            {
                flag = false,
                message = "Entry saving error !"
            };
            var flag = false;
            if (DeleteItems != null)
            {
                foreach (var i in DeleteItems)
                {
                    //var delteItem = db.Proc_MaterialEntryDet.SingleOrDefault(x => x.ItemId == i && x.Proc_PurchaseOrderMasId == ProcPurchaseMasterId);
                    var entryDetId = db.Proc_MaterialEntryDet.Find(i);
                    db.Proc_MaterialEntryDet.Remove(entryDetId);
                    db.SaveChanges();

                }
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var master = db.Proc_MaterialEntryMas.Find(EntryMasId);
                    master.EDate = DateTime.ParseExact(EDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                    //master.EDate = EDate;
                    db.Entry(master).State = EntityState.Modified;
                    flag = db.SaveChanges() > 0;
                    //var TenderMasId = master.Id;

                    if (flag)
                    {
                        foreach (var item in EntryItems)
                        {
                            //var check = db.Proc_MaterialEntryDet.FirstOrDefault(x => x.Proc_MaterialEntryMasId == EntryMasId && x.Proc_PurchaseOrderDet.ItemId == item.ItemId && x.ChallanNo == item.ChallanNo);
                            var check = db.Proc_MaterialEntryDet.FirstOrDefault(x => x.Id == item.Proc_MaterialEntryDetId);

                            if (check == null)
                            {
                                Proc_MaterialEntryDet detail = new Proc_MaterialEntryDet();
                                detail.Proc_MaterialEntryMasId = EntryMasId;
                                detail.ChallanNo = item.ChallanNo;
                                if (item.ChallanDate == null)
                                {
                                    detail.ChallanDate = null;
                                }
                                else
                                {
                                    detail.ChallanDate = DateTime.ParseExact(item.ChallanDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                                }
                                //detail.ChallanDate = item.ChallanDate;
                                detail.EntryQty = item.EntryQty;
                                detail.Status = item.Status;

                                var PurchaseOrderMasId = db.Proc_PurchaseOrderMas.FirstOrDefault(x => x.Id == item.PONo);
                                var PurchaseOrderDetId = db.Proc_PurchaseOrderDet.FirstOrDefault(y => y.Proc_PurchaseOrderMasId == PurchaseOrderMasId.Id && y.ItemId == item.ItemId);

                                detail.Proc_PurchaseOrderDetId = PurchaseOrderDetId.Id;
                                db.Entry(detail).State = EntityState.Added;
                                db.SaveChanges();
                            }
                            else
                            {

                                //var Proc_EntryDet_Id = db.Proc_MaterialEntryDet.FirstOrDefault(x => x.Proc_MaterialEntryMasId == EntryMasId && x.Proc_PurchaseOrderDet.ItemId == item.ItemId);
                                //var getItem = db.Proc_MaterialEntryDet.Find(Proc_EntryDet_Id.Id);
                                var getItem = db.Proc_MaterialEntryDet.Find(item.Proc_MaterialEntryDetId);

                                getItem.ChallanNo = item.ChallanNo;
                                if (item.ChallanDate == null)
                                {
                                    getItem.ChallanDate = null;
                                }
                                else
                                {
                                    getItem.ChallanDate = DateTime.ParseExact(item.ChallanDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                                }
                                //getItem.ChallanDate = item.ChallanDate;
                                getItem.EntryQty = item.EntryQty;
                                getItem.Status = item.Status;

                                db.Entry(getItem).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }

                    dbContextTransaction.Commit();

                    if (flag == true)
                    {
                        result = new
                        {
                            flag = true,
                            message = "Save Successful!"
                        };
                    }
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

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEntryDetailItem(int EntryDetailId)
        {

            var result = new
            {
                flag = false,
                message = "Delete error !"
            };

            var check = (from entryDet in db.Proc_MaterialEntryDet
                         join vendorDet in db.Proc_VendorPaymentDet on entryDet.Id equals vendorDet.Proc_MaterialEntryDetId
                         where entryDet.Id == EntryDetailId
                         select entryDet).ToList();
            //var data = db.Proc_MaterialEntryDet.Where(x => x.Id == EntryDetailId).FirstOrDefault();
            //db.Proc_MaterialEntryDet.Remove(data);
            //flag = db.SaveChanges() > 0;
            if (check.Count == 0)
            {

                //var 

                result = new
                {
                    flag = true,
                    message = "Delete Successful Successful!"
                };

            }
            else
            {
                result = new
                {
                    flag = false,
                    message = "Delete failed! Please delete Vendor payment first!"
                };
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}