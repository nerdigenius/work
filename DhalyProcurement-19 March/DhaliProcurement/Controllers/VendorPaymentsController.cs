﻿using DhaliProcurement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DhaliProcurement.ViewModel;
using System.Data.Entity;

namespace DhaliProcurement.Controllers
{
    [Authorize]
    public class VendorPaymentsController : Controller
    {

        private DCPSContext db = new DCPSContext();

        // GET: VendorPayments
        public ActionResult Index(int? VendorId)
        {
            ViewBag.VendorId = new SelectList(db.Vendor, "Id", "Name");

            var paymentList = db.Proc_VendorPaymentMas.ToList();


            if (VendorId != null)
            {
                paymentList = paymentList.Where(x => x.VendorId == VendorId).ToList();

                // ViewBag.VendorId = new SelectList(db.Vendor.Where(y => y.Id == vPayId), "Id", "Name");

                return View(paymentList);
            }

            else
            {
                return View(paymentList);
            }


            // return View(paymentList);

        }

        public ActionResult Create()
        {

            var vendors = (from purchaseMas in db.Proc_PurchaseOrderMas
                           join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                           join entryDet in db.Proc_MaterialEntryDet on purchaseDet.Id equals entryDet.Proc_PurchaseOrderDetId
                           join vendor in db.Vendor on purchaseMas.VendorId equals vendor.Id
                           select vendor).Distinct();
            ViewBag.VendorId = new SelectList(vendors, "Id", "Name");




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


            ViewBag.ProjectId = new SelectList(projects.Distinct(), "Id", "Name");
            ViewBag.SiteId = new SelectList(sites, "Id", "Name");

            ViewBag.ItemName = new SelectList(db.Item, "Id", "Name");
            ViewBag.ReqNo = new SelectList(db.Proc_RequisitionMas, "Id", "RCode");
            //ViewBag.VendorId = new SelectList(db.Vendor, "Id", "Name");
            ViewBag.ChallanNo = new SelectList(db.Proc_MaterialEntryDet, "Id", "ChallanNo");
            ViewBag.PONo = new SelectList(db.Proc_PurchaseOrderMas, "Id", "PONo");


            return View();
        }


        public JsonResult GetSelectedItems(int VendorId,int ProjectId,int SiteId,int ReqNo, int PONo, int ItemId)
        {
            List<SelectListItem> ProjectList = new List<SelectListItem>();

            var paymentProject = (from purchaseMas in db.Proc_PurchaseOrderMas
                                  join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                                  join materialEntryDet in db.Proc_MaterialEntryDet on purchaseDet.Id equals materialEntryDet.Proc_PurchaseOrderDetId
                                  join materialEntryMas in db.Proc_MaterialEntryMas on materialEntryDet.Proc_MaterialEntryMasId equals materialEntryMas.Id
                                  join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                  join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                  join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                                  join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                  join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                                  join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                                  join project in db.Project on site.ProjectId equals project.Id
                                  where materialEntryDet.Proc_PurchaseOrderDet.Proc_PurchaseOrderMas.VendorId == VendorId
                                   && materialEntryMas.ProcProject.ProjectSiteId == site.Id
                                  select project).Distinct().ToList();

            foreach (var x in paymentProject.Distinct())
            {

                ProjectList.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            }

            var procsites = (from purchaseMas in db.Proc_PurchaseOrderMas
                             join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                             join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                             join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                             join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                             join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                             join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                             join project in db.Project on site.ProjectId equals project.Id
                             where purchaseMas.VendorId == VendorId && project.Id == ProjectId
                             select site).Distinct().ToList();

            List<SelectListItem> siteList = new List<SelectListItem>();
            foreach (var i in procsites)
            {
                var site = db.ProjectSite.FirstOrDefault(x => x.Id == i.Id);
                siteList.Add(new SelectListItem { Text = site.Name, Value = site.Id.ToString() });

            }

            List<SelectListItem> RequisitionList = new List<SelectListItem>();

            var itemsReq = (from procProject in db.ProcProject
                            join RequisitionMas in db.Proc_RequisitionMas on procProject.Id equals RequisitionMas.ProcProjectId
                            join RequisitionDet in db.Proc_RequisitionDet on RequisitionMas.Id equals RequisitionDet.Proc_RequisitionMasId
                            join tenderDet in db.Proc_TenderDet on RequisitionDet.Id equals tenderDet.Proc_RequisitionDetId
                            join procSite in db.ProjectSite on procProject.ProjectSiteId equals procSite.Id
                            where procSite.Project.Id == ProjectId && procSite.Id == SiteId && RequisitionMas.Status == "A" && tenderDet.VendorId == VendorId
                            select RequisitionMas).Distinct().ToList();


            foreach (var x in itemsReq)
            {
                //var reqName = db.Proc_RequisitionMas.SingleOrDefault(m => m.Id == x.Id);
                RequisitionList.Add(new SelectListItem { Text = x.Rcode, Value = x.Id.ToString() });
            }

            List<SelectListItem> POList = new List<SelectListItem>();

            //var itemsPONo = (from purMas in db.Proc_PurchaseOrderMas
            //                 join purdet in db.Proc_PurchaseOrderDet on purMas.Id equals purdet.Proc_PurchaseOrderMasId
            //                 join metDet in db.Proc_MaterialEntryDet on purdet.Id equals metDet.Proc_PurchaseOrderDetId
            //                 join tenderMas in db.Proc_TenderMas on purMas.Proc_TenderMasId equals tenderMas.Id
            //                 join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
            //                 join reqDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals reqDet.Id
            //                 join reqMas in db.Proc_RequisitionMas on reqDet.Proc_RequisitionMasId equals reqMas.Id
            //                 join procProject in db.ProcProject on reqMas.ProcProjectId equals procProject.Id
            //                 join vendors in db.Vendor on purMas.VendorId equals vendors.Id
            //                 where reqMas.Id == ReqNo && tenderDet.VendorId == VendorId && procProject.ProjectSiteId == SiteId
            //                 select purMas).Distinct().ToList();

            var itemsPONo = (from purMas in db.Proc_PurchaseOrderMas
                             join purdet in db.Proc_PurchaseOrderDet on purMas.Id equals purdet.Proc_PurchaseOrderMasId
                             join metDet in db.Proc_MaterialEntryDet on purdet.Id equals metDet.Proc_PurchaseOrderDetId
                             join metMas in db.Proc_MaterialEntryMas on metDet.Proc_MaterialEntryMasId equals metMas.Id
                             join procProject in db.ProcProject on metMas.ProcProjectId equals procProject.Id
                             join reqMas in db.Proc_RequisitionMas on procProject.Id equals reqMas.ProcProjectId
                             join vendors in db.Vendor on purMas.VendorId equals vendors.Id
                             where reqMas.Id == ReqNo && purMas.VendorId == VendorId && procProject.ProjectSiteId == SiteId
                             select purMas).Distinct().ToList();


            foreach (var x in itemsPONo)
            {

                POList.Add(new SelectListItem { Text = x.PONo, Value = x.Id.ToString() });
            }



           List<SelectListItem> itemList = new List<SelectListItem>();

            var items = (from purDet in db.Proc_PurchaseOrderDet
                             join metDet in db.Proc_MaterialEntryDet on purDet.Id equals metDet.Proc_PurchaseOrderDetId
                             join purMas in db.Proc_PurchaseOrderMas on purDet.Proc_PurchaseOrderMasId equals purMas.Id
                             join itemLists in db.Item on purDet.ItemId equals itemLists.Id
                             where purMas.Id == PONo && purDet.ItemId == itemLists.Id
                             select new { purDet, itemLists }).Distinct().ToList();


            foreach (var x in items)
            {

                itemList.Add(new SelectListItem { Text = x.itemLists.Name, Value = x.purDet.ItemId.ToString() });
            }

            List<SelectListItem> challanList = new List<SelectListItem>();
            var challanData = (from metEntry in db.Proc_MaterialEntryDet
                               join purDet in db.Proc_PurchaseOrderDet on metEntry.Proc_PurchaseOrderDetId equals purDet.Id
                               join purMas in db.Proc_PurchaseOrderMas on purDet.Proc_PurchaseOrderMasId equals purMas.Id
                               join tenderMas in db.Proc_TenderMas on purMas.Proc_TenderMasId equals tenderMas.Id
                               join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                               join vendors in db.Vendor on tenderDet.VendorId equals vendors.Id
                               join reqDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals reqDet.Id
                               where metEntry.Proc_PurchaseOrderDet.ItemId == ItemId && purMas.Id == PONo
                               select metEntry).ToList();


            foreach (var x in challanData.Distinct())
            {

                challanList.Add(new SelectListItem { Text = x.ChallanNo, Value = x.Id.ToString() });
            }

            var result = new
            {
                ProjectList = ProjectList.Distinct(),
                Sites = siteList,
                ReqItems = RequisitionList,
                POItems = POList.Distinct(),
                Items = itemList,
                ChallanNo = challanList.Distinct()
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit(int vPayId)
        {

            ViewBag.VendorPayPrimaryKey = db.Proc_VendorPaymentDet.Max(x => x.Id);

            ViewBag.VPayId = vPayId;
            VendorPaymentMasterDetail vm = new VendorPaymentMasterDetail();
            var proc_vPaymentMasLists = db.Proc_VendorPaymentMas.FirstOrDefault(x => x.Id == vPayId);
            vm.proc_vPaymentMas = proc_vPaymentMasLists;
            List<Proc_VendorPaymentDet> paymentDetList = new List<Proc_VendorPaymentDet>();

            var PaymentDetails = db.Proc_VendorPaymentDet.Where(x => x.Proc_VendorPaymentMasId == vPayId).ToList();
            foreach (var item in PaymentDetails)
            {
                paymentDetList.Add(item);
            }
            vm.proc_vPaymentDet = paymentDetList;

            //vm.proc_vPaymentMas.VPDate = vm.proc_vPaymentMas.VPDate;
            var procprojects = db.ProcProject.ToList();

            //List<ProjectSite> sites = new List<ProjectSite>();
            //foreach (var i in procprojects)
            //{
            //    var site = db.ProjectSite.FirstOrDefault(x => x.Id == i.ProjectSiteId);
            //    sites.Add(site);
            //}

            //List<Project> projects = new List<Project>();
            //foreach (var i in sites)
            //{
            //    var proj = db.Project.FirstOrDefault(x => x.Id == i.ProjectId);
            //    projects.Add(proj);
            //}



            //var findingPIdAndSiteId = (from vPaydet in db.Proc_VendorPaymentDet
            //                           join vPayMas in db.Proc_VendorPaymentMas on vPaydet.Proc_VendorPaymentMasId equals vPayMas.Id
            //                           join metEntryDet in db.Proc_MaterialEntryDet on vPaydet.Proc_MaterialEntryDetId equals metEntryDet.Id
            //                           join purDet in db.Proc_PurchaseOrderDet on metEntryDet.Proc_PurchaseOrderDetId equals purDet.Id
            //                           join procItem in db.ProcProjectItem on purDet.ItemId equals procItem.ItemId
            //                           join procProj in db.ProcProject on procItem.ProcProjectId equals procProj.Id
            //                           join projSite in db.ProjectSite on procProj.ProjectSiteId equals projSite.Id
            //                           where vPayMas.Id == vPayId
            //                           select new { procProj, vPayMas }).FirstOrDefault();

            var findingPIdAndSiteId = (from metEntryDet in db.Proc_MaterialEntryDet
                                       join purDet in db.Proc_PurchaseOrderDet on metEntryDet.Proc_PurchaseOrderDetId equals purDet.Id
                                       join procItem in db.ProcProjectItem on purDet.ItemId equals procItem.ItemId
                                       join procProj in db.ProcProject on procItem.ProcProjectId equals procProj.Id
                                       join projSite in db.ProjectSite on procProj.ProjectSiteId equals projSite.Id
                                       //where vPayMas.Id == vPayId
                                       select new { procProj }).FirstOrDefault();

            List<SelectListItem> ProjectList = new List<SelectListItem>();
            var vendorIdforProject = db.Proc_VendorPaymentMas.SingleOrDefault(x => x.Id == vPayId).VendorId;


            //var VendorProjects = (from purchaseMas in db.Proc_PurchaseOrderMas
            //                join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
            //                join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
            //                join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
            //                join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
            //                join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
            //                join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
            //                join project in db.Project on site.ProjectId equals project.Id
            //                where purchaseMas.VendorId == vendorIdforProject
            //                select project).Distinct().ToList();


            var paymentProject = (from purchaseMas in db.Proc_PurchaseOrderMas
                                  join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                                  join materialEntryDet in db.Proc_MaterialEntryDet on purchaseDet.Id equals materialEntryDet.Proc_PurchaseOrderDetId
                                  join materialEntryMas in db.Proc_MaterialEntryMas on materialEntryDet.Proc_MaterialEntryMasId equals materialEntryMas.Id
                                  join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                  join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                  join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                                  join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                  join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                                  join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                                  join project in db.Project on site.ProjectId equals project.Id
                                  where materialEntryDet.Proc_PurchaseOrderDet.Proc_PurchaseOrderMas.VendorId == vendorIdforProject
                                   && materialEntryMas.ProcProject.ProjectSiteId == site.Id
                                  select project).Distinct().ToList();

            foreach (var x in paymentProject)
            {

                ProjectList.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            }
            ViewBag.ProjectId = ProjectList.Distinct();
            //ViewBag.ProjectId = new SelectList(ProjectList.Distinct(), "Id", "Name");

            //ViewBag.ProjectId = new SelectList(projects.Distinct(), "Id", "Name");
            var paymentSite = (from purchaseMas in db.Proc_PurchaseOrderMas
                               join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                               join materialEntryDet in db.Proc_MaterialEntryDet on purchaseDet.Id equals materialEntryDet.Proc_PurchaseOrderDetId
                               join materialEntryMas in db.Proc_MaterialEntryMas on materialEntryDet.Proc_MaterialEntryMasId equals materialEntryMas.Id
                               join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                               join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                               join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                               join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                               join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                               join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                               join project in db.Project on site.ProjectId equals project.Id
                               where materialEntryDet.Proc_PurchaseOrderDet.Proc_PurchaseOrderMas.VendorId == vendorIdforProject
                                && materialEntryMas.ProcProject.ProjectSiteId == site.Id
                               select site).ToList();


            List<ProjectSite> sites = new List<ProjectSite>();
            foreach (var i in paymentSite.Distinct())
            {
                var site = db.ProjectSite.FirstOrDefault(x => x.Id == i.Id);
                sites.Add(site);
            }

            ViewBag.SiteId = new SelectList(sites, "Id", "Name");

            ViewBag.ProcProjId = findingPIdAndSiteId.procProj.Id;
            ViewBag.ProcProjSiteId = findingPIdAndSiteId.procProj.ProjectSiteId;


            //ViewBag.ReqNo = new SelectList(db.Proc_RequisitionMas, "Id", "RCode");

            var paymentReqNo = (from purchaseMas in db.Proc_PurchaseOrderMas
                                join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                                join materialEntryDet in db.Proc_MaterialEntryDet on purchaseDet.Id equals materialEntryDet.Proc_PurchaseOrderDetId
                                join materialEntryMas in db.Proc_MaterialEntryMas on materialEntryDet.Proc_MaterialEntryMasId equals materialEntryMas.Id
                                join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                                join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                                join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                                join project in db.Project on site.ProjectId equals project.Id
                                where materialEntryDet.Proc_PurchaseOrderDet.Proc_PurchaseOrderMas.VendorId == vendorIdforProject
                                 && materialEntryMas.ProcProject.ProjectSiteId == site.Id
                                select requisitionMas).Distinct().ToList();

            List<Proc_RequisitionMas> requisitions = new List<Proc_RequisitionMas>();
            foreach (var i in paymentReqNo)
            {
                var req = db.Proc_RequisitionMas.FirstOrDefault(x => x.Id == i.Id);
                requisitions.Add(req);
            }
            ViewBag.ReqNo = new SelectList(requisitions, "Id", "RCode");
            var vendors = (from purchaseMas in db.Proc_PurchaseOrderMas
                           join vendor in db.Vendor on purchaseMas.VendorId equals vendor.Id
                           select vendor).Distinct();
            var selectedVendor = db.Proc_VendorPaymentMas.SingleOrDefault(x => x.Id == vPayId).VendorId;
            //ViewBag.VendorId = new SelectList(vendors, "Id", "Name", db.Proc_VendorPaymentMas.SingleOrDefault(x => x.Id == vPayId).VendorId);
            ViewBag.VendorId = new SelectList(vendors, "Id", "Name", selectedVendor);

            //ViewBag.ItemName = new SelectList(db.Item, "Id", "Name");
            var paymentItems = (from purchaseMas in db.Proc_PurchaseOrderMas
                                join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                                join materialEntryDet in db.Proc_MaterialEntryDet on purchaseDet.Id equals materialEntryDet.Proc_PurchaseOrderDetId
                                join materialEntryMas in db.Proc_MaterialEntryMas on materialEntryDet.Proc_MaterialEntryMasId equals materialEntryMas.Id
                                join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                                join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                                join procProjectItem in db.ProcProjectItem on procProject.Id equals procProjectItem.ProcProjectId
                                join item in db.Item on procProjectItem.ItemId equals item.Id
                                join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                                join project in db.Project on site.ProjectId equals project.Id
                                //where materialEntryDet.Proc_PurchaseOrderDet.Proc_PurchaseOrderMas.VendorId == vendorIdforProject
                                where materialEntryDet.Proc_PurchaseOrderDet.Proc_PurchaseOrderMas.VendorId == selectedVendor
                                && materialEntryMas.ProcProject.ProjectSiteId == site.Id
                                select item).Distinct().ToList();

            List<Item> pItems = new List<Item>();
            foreach (var i in paymentItems)
            {
                var item = db.Item.FirstOrDefault(x => x.Id == i.Id);
                pItems.Add(item);
            }
            ViewBag.ItemName = new SelectList(pItems, "Id", "Name");

            //ViewBag.VendorId = new SelectList(db.Vendor, "Id", "Name");

            ViewBag.ChallanNo = new SelectList(db.Proc_MaterialEntryDet, "Id", "ChallanNo");
            ViewBag.PONo = new SelectList(db.Proc_PurchaseOrderMas.Where(x => x.VendorId == selectedVendor), "Id", "PONo");

            return View(vm);
        }




        public ActionResult Details(int vPayId)
        {
            ViewBag.vPayId = vPayId;
            VendorPaymentMasterDetail vm = new VendorPaymentMasterDetail();
            var proc_vPaymentMasLists = db.Proc_VendorPaymentMas.FirstOrDefault(x => x.Id == vPayId);
            vm.proc_vPaymentMas = proc_vPaymentMasLists;
            List<Proc_VendorPaymentDet> paymentDetList = new List<Proc_VendorPaymentDet>();

            var PaymentDetails = db.Proc_VendorPaymentDet.Where(x => x.Proc_VendorPaymentMasId == vPayId).ToList();
            foreach (var item in PaymentDetails)
            {
                paymentDetList.Add(item);
            }
            vm.proc_vPaymentDet = paymentDetList;

            //vm.proc_vPaymentMas.VPDate = vm.proc_vPaymentMas.VPDate;
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



            var findingPIdAndSiteId = (from vPaydet in db.Proc_VendorPaymentDet
                                       join vPayMas in db.Proc_VendorPaymentMas on vPaydet.Proc_VendorPaymentMasId equals vPayMas.Id
                                       join metEntryDet in db.Proc_MaterialEntryDet on vPaydet.Proc_MaterialEntryDetId equals metEntryDet.Id
                                       join purDet in db.Proc_PurchaseOrderDet on metEntryDet.Proc_PurchaseOrderDetId equals purDet.Id
                                       join procItem in db.ProcProjectItem on purDet.ItemId equals procItem.ItemId
                                       join procProj in db.ProcProject on procItem.ProcProjectId equals procProj.Id
                                       join projSite in db.ProjectSite on procProj.ProjectSiteId equals projSite.Id
                                       where vPayMas.Id == vPayId
                                       select new { procProj, vPayMas }).FirstOrDefault();


            ViewBag.ProjectId = new SelectList(projects.Distinct(), "Id", "Name");
            ViewBag.SiteId = new SelectList(sites, "Id", "Name");


            //ViewBag.ProcProjId = findingPIdAndSiteId.procProj.Id;
            //ViewBag.ProcProjSiteId = findingPIdAndSiteId.procProj.ProjectSiteId;
            var vendors = (from purchaseMas in db.Proc_PurchaseOrderMas
                           join vendor in db.Vendor on purchaseMas.VendorId equals vendor.Id
                           select vendor).Distinct();
            ViewBag.VendorId = new SelectList(vendors, "Id", "Name", db.Proc_VendorPaymentMas.SingleOrDefault(x => x.Id == vPayId).VendorId);
            ViewBag.ItemName = new SelectList(db.Item, "Id", "Name");
            ViewBag.ReqNo = new SelectList(db.Proc_RequisitionMas, "Id", "RCode");
            ViewBag.ChallanNo = new SelectList(db.Proc_MaterialEntryDet, "Id", "ChallanNo");
            ViewBag.PONo = new SelectList(db.Proc_PurchaseOrderMas, "Id", "PONo");

            return View(vm);
        }






        //[HttpPost]
        //public JsonResult GetSites(int ProjectId)
        //{
        //    var flag = false;
        //    //var result = new
        //    //{
        //    //    flag = false,
        //    //    message = "Delete error !"
        //    //};

        //    var procsites = (from procProject in db.ProcProject
        //                     join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
        //                     join project in db.Project on site.ProjectId equals project.Id
        //                     where project.Id == ProjectId
        //                     select site).ToList();

        //    List<ProjectSite> siteList = new List<ProjectSite>();
        //    foreach (var i in procsites)
        //    {
        //        var site = db.ProjectSite.FirstOrDefault(x => x.ProjectId == i.ProjectId);
        //        siteList.Add(site);
        //    }


        //   var  result = new
        //    {
        //        flag = true,
        //       sites= siteList

        //   };
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult GetProjects(int VendorId)
        {
            List<SelectListItem> ProjectList = new List<SelectListItem>();

            //var projects = (from purchaseMas in db.Proc_PurchaseOrderMas
            //                join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
            //                join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
            //                join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
            //                join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
            //                join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
            //                join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
            //                join project in db.Project on site.ProjectId equals project.Id
            //                where purchaseMas.VendorId == VendorId
            //                select project).Distinct().ToList();

            var paymentProject = (from purchaseMas in db.Proc_PurchaseOrderMas
                                  join purchaseDet in db.Proc_PurchaseOrderDet on purchaseMas.Id equals purchaseDet.Proc_PurchaseOrderMasId
                                  join materialEntryDet in db.Proc_MaterialEntryDet on purchaseDet.Id equals materialEntryDet.Proc_PurchaseOrderDetId
                                  join materialEntryMas in db.Proc_MaterialEntryMas on materialEntryDet.Proc_MaterialEntryMasId equals materialEntryMas.Id
                                  join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                  join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                  join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                                  join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                  join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                                  join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                                  join project in db.Project on site.ProjectId equals project.Id
                                  where materialEntryDet.Proc_PurchaseOrderDet.Proc_PurchaseOrderMas.VendorId == VendorId
                                   && materialEntryMas.ProcProject.ProjectSiteId == site.Id
                                  select project).Distinct().ToList();

            foreach (var x in paymentProject.Distinct())
            {

                ProjectList.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            }


            var result = new
            {
                ProjectList = ProjectList.Distinct()
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSites(int ProjectId, int VendorId)
        {

            //var procsites = (from procProject in db.ProcProject
            //                 join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
            //                 join project in db.Project on site.ProjectId equals project.Id
            //                 where project.Id == ProjectId
            //                 select project).Distinct().ToList();

            var procsites = (from purchaseMas in db.Proc_PurchaseOrderMas
                             join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                             join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                             join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                             join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                             join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                             join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                             join project in db.Project on site.ProjectId equals project.Id
                             where purchaseMas.VendorId == VendorId && project.Id == ProjectId
                             select site).Distinct().ToList();

            List<SelectListItem> siteList = new List<SelectListItem>();
            foreach (var i in procsites)
            {
                var site = db.ProjectSite.FirstOrDefault(x => x.Id == i.Id);
                siteList.Add(new SelectListItem { Text = site.Name, Value = site.Id.ToString() });

            }


            var result = new
            {
                Sites = siteList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ReqNo(int SiteId, int ProjectId, int vendorId)
        {
            List<SelectListItem> RequisitionList = new List<SelectListItem>();

            var itemsReq = (from procProject in db.ProcProject
                            join RequisitionMas in db.Proc_RequisitionMas on procProject.Id equals RequisitionMas.ProcProjectId
                            join RequisitionDet in db.Proc_RequisitionDet on RequisitionMas.Id equals RequisitionDet.Proc_RequisitionMasId
                            join tenderDet in db.Proc_TenderDet on RequisitionDet.Id equals tenderDet.Proc_RequisitionDetId
                            join procSite in db.ProjectSite on procProject.ProjectSiteId equals procSite.Id
                            where procSite.Project.Id == ProjectId && procSite.Id == SiteId && RequisitionMas.Status == "A" && tenderDet.VendorId == vendorId
                            select RequisitionMas).Distinct().ToList();


            foreach (var x in itemsReq)
            {
                var reqName = db.Proc_RequisitionMas.SingleOrDefault(m => m.Id == x.Id);
                RequisitionList.Add(new SelectListItem { Text = reqName.Rcode, Value = x.Id.ToString() });
            }


            var result = new
            {
                ReqItems = RequisitionList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult GetPONo(int ReqNo, int vendorId, int SiteId)
        {
            List<SelectListItem> POList = new List<SelectListItem>();


            //var itemsPONo = (from purMas in db.Proc_PurchaseOrderMas
            //                 join purdet in db.Proc_PurchaseOrderDet on purMas.Id equals purdet.Proc_PurchaseOrderMasId
            //                 join metDet in db.Proc_MaterialEntryDet on purdet.Id equals metDet.Proc_PurchaseOrderDetId
            //                 join tenderMas in db.Proc_TenderMas on purMas.Proc_TenderMasId equals tenderMas.Id
            //                 join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
            //                 join reqDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals reqDet.Id
            //                 join reqMas in db.Proc_RequisitionMas on reqDet.Proc_RequisitionMasId equals reqMas.Id
            //                 join procProject in db.ProcProject on reqMas.ProcProjectId equals procProject.Id
            //                 join vendors in db.Vendor on purMas.VendorId equals vendors.Id
            //                 where reqMas.Id == ReqNo && tenderDet.VendorId == vendorId && procProject.ProjectSiteId == SiteId && metDet.Proc_PurchaseOrderDetId == purdet.Id
            //                 select purMas).Distinct().ToList();

            var itemsPONo = (from purMas in db.Proc_PurchaseOrderMas
                             join purdet in db.Proc_PurchaseOrderDet on purMas.Id equals purdet.Proc_PurchaseOrderMasId
                             join metDet in db.Proc_MaterialEntryDet on purdet.Id equals metDet.Proc_PurchaseOrderDetId
                             join metMas in db.Proc_MaterialEntryMas on metDet.Proc_MaterialEntryMasId equals metMas.Id
                             join procProject in db.ProcProject on metMas.ProcProjectId equals procProject.Id
                             join reqMas in db.Proc_RequisitionMas on procProject.Id equals reqMas.ProcProjectId
                             join vendors in db.Vendor on purMas.VendorId equals vendors.Id
                             where reqMas.Id == ReqNo && purMas.VendorId == vendorId && procProject.ProjectSiteId == SiteId
                             select purMas).Distinct().ToList();


            foreach (var x in itemsPONo)
            {

                POList.Add(new SelectListItem { Text = x.PONo, Value = x.Id.ToString() });
            }


            var result = new
            {
                POItems = POList.Distinct()
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetTotalAmt(int ChallanNo)
        {

            //var getUnitPrice = (from metDet in db.Proc_MaterialEntryDet
            //                    join purchaseDet in db.Proc_PurchaseOrderDet on metDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
            //                    join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
            //                    join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
            //                    join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
            //                    where metDet.Id == ChallanNo
            //                    select new { metDet, tenderDet }).FirstOrDefault();

            //var qty = getUnitPrice.metDet.EntryQty;
            //var unitPrice = getUnitPrice.tenderDet.TQPrice;


            
           

            var getUnitPrice = (from metDet in db.Proc_MaterialEntryDet
                                join purchaseDet in db.Proc_PurchaseOrderDet on metDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                                join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                                join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                where metDet.Id == ChallanNo
                                select new { purchaseDet, tenderDet }).FirstOrDefault();

            var qty = getUnitPrice.purchaseDet.POQty;
            var unitPrice = getUnitPrice.tenderDet.TQPrice;
            var totalAmt = qty * unitPrice;

            var result = new
            {
                totalAmt = totalAmt,
                Proc_MaterialEntryDetId = ChallanNo

            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        //[HttpPost]
        //public JsonResult GetTotalAmt(int Proc_MaterialEntryDetId)
        //{
        //    //total amount in vendor payments create view
        //    var getUnitPrice = (from metDet in db.Proc_MaterialEntryDet
        //                        join purchaseDet in db.Proc_PurchaseOrderDet on metDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
        //                        join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
        //                        join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
        //                        join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
        //                        where metDet.Id == Proc_MaterialEntryDetId
        //                        select new { purchaseDet, tenderDet }).FirstOrDefault();

        //    var qty = getUnitPrice.purchaseDet.POQty;
        //    var unitPrice = getUnitPrice.tenderDet.TQPrice;
        //    var totalAmt = qty * unitPrice;

        //    var result = new
        //    {
        //        totalAmt = totalAmt
        //    };
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}



        [HttpPost]
        public JsonResult GetItems(int PONo)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();

            var itemsPONo = (from purDet in db.Proc_PurchaseOrderDet
                             join metDet in db.Proc_MaterialEntryDet on purDet.Id equals metDet.Proc_PurchaseOrderDetId
                             join purMas in db.Proc_PurchaseOrderMas on purDet.Proc_PurchaseOrderMasId equals purMas.Id
                             join itemLists in db.Item on purDet.ItemId equals itemLists.Id
                             where purMas.Id == PONo && purDet.ItemId == itemLists.Id
                             select new { purDet, itemLists }).Distinct().ToList();


            foreach (var x in itemsPONo)
            {

                itemList.Add(new SelectListItem { Text = x.itemLists.Name, Value = x.purDet.ItemId.ToString() });
            }


            var result = new
            {
                POItems = itemList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetVendorPayDetData(int itemId, int projectId, int siteId, int PONo)
        {
            List<SelectListItem> challanList = new List<SelectListItem>();

            var data = (from procProjectItem in db.ProcProjectItem
                        join units in db.Unit on procProjectItem.UnitId equals units.Id
                        join procReqDet in db.Proc_RequisitionDet on procProjectItem.ItemId equals procReqDet.ItemId
                        join procReqMas in db.Proc_RequisitionMas on procReqDet.Proc_RequisitionMasId equals procReqMas.Id
                        where procProjectItem.ItemId == itemId && procProjectItem.ProcProject.ProjectSiteId == siteId
                        select new { units, procReqDet }).FirstOrDefault();

            var unit = (from procProject in db.ProcProject
                        join ProcProjectItem in db.ProcProjectItem on procProject.Id equals ProcProjectItem.ProcProjectId
                        join units in db.Unit on ProcProjectItem.UnitId equals units.Id
                        where ProcProjectItem.ItemId == itemId && procProject.ProjectSiteId == siteId
                        select units).SingleOrDefault();


            var challanData = (from metEntry in db.Proc_MaterialEntryDet
                               join purDet in db.Proc_PurchaseOrderDet on metEntry.Proc_PurchaseOrderDetId equals purDet.Id
                               join purMas in db.Proc_PurchaseOrderMas on purDet.Proc_PurchaseOrderMasId equals purMas.Id
                               join tenderMas in db.Proc_TenderMas on purMas.Proc_TenderMasId equals tenderMas.Id
                               join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                               join vendors in db.Vendor on tenderDet.VendorId equals vendors.Id
                               join reqDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals reqDet.Id
                               //join reqMas in db.Proc_RequisitionMas on reqDet.Proc_RequisitionMasId equals reqMas.Id
                               //join procProj in db.ProcProject on reqMas.ProcProjectId equals procProj.Id
                               //join projSite in db.ProjectSite on procProj.ProjectSiteId equals projSite.Id
                               where metEntry.Proc_PurchaseOrderDet.ItemId == itemId && purMas.Id == PONo
                               select new { metEntry }).ToList();



            //var metEntryDet = (from metEntry in db.Proc_MaterialEntryDet
            //                   join purDet in db.Proc_PurchaseOrderDet on metEntry.Proc_PurchaseOrderDetId equals purDet.Id
            //                   join purMas in db.Proc_PurchaseOrderMas on purDet.Proc_PurchaseOrderMasId equals purMas.Id
            //                   join tenderMas in db.Proc_TenderMas on purMas.Proc_TenderMasId equals tenderMas.Id
            //                   join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
            //                   join vendors in db.Vendor on tenderDet.VendorId equals vendors.Id
            //                   join reqDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals reqDet.Id
            //                   join reqMas in db.Proc_RequisitionMas on reqDet.Proc_RequisitionMasId equals reqMas.Id
            //                   join procProj in db.ProcProject on reqMas.ProcProjectId equals procProj.Id
            //                   join projSite in db.ProjectSite on procProj.ProjectSiteId equals projSite.Id
            //                   where reqDet.ItemId == itemId  && procProj.ProjectSiteId == siteId && metEntry.Proc_PurchaseOrderDetId == purDet.Id && metEntry.Id == 
            //                   select new { metEntry }).Distinct().FirstOrDefault();



            var metEntryDet = (from metEntry in db.Proc_MaterialEntryDet
                               join purDet in db.Proc_PurchaseOrderDet on metEntry.Proc_PurchaseOrderDetId equals purDet.Id
                               join items in db.Item on purDet.ItemId equals items.Id
                               where purDet.ItemId == itemId && purDet.Proc_PurchaseOrderMasId == PONo
                               select new { metEntry }).Distinct().FirstOrDefault();


            foreach (var x in challanData.Distinct())
            {

                challanList.Add(new SelectListItem { Text = x.metEntry.ChallanNo, Value = x.metEntry.Id.ToString() });
            }

            var result = new
            {
                //flag = true,
                UnitName = unit.Name,
                UnitId = unit.Id,
                RemQty = data.procReqDet.ReqQty,
                ChallanNo = challanList.Distinct(),
                Proc_MaterialEntryDetId = metEntryDet.metEntry.Id
                //  Proc_VendorPaymentDetId= 

                //TotalRequired = totalRequired.PQuantity
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public JsonResult VendorPaymentCreate(IEnumerable<VMVenderPayment> PaymentItems, int VendorId, DateTime PayDate)
        {
            var result = new
            {
                flag = false,
                message = "Requisition saving error !"
            };
            var flag = false;

            var vendorPayments = db.Proc_VendorPaymentMas.Where(x => x.VendorId == VendorId && x.VPDate == PayDate).ToList();

            if (vendorPayments.Count == 0)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        flag = false;
                        Proc_VendorPaymentMas master = new Proc_VendorPaymentMas();

                        master.VendorId = VendorId;
                        master.VPDate = PayDate;
                        db.Proc_VendorPaymentMas.Add(master);
                        flag = db.SaveChanges() > 0;

                        var Proc_VendorPaymentMasId = master.Id;

                        foreach (var item in PaymentItems)
                        {
                            Proc_VendorPaymentDet detail = new Proc_VendorPaymentDet();
                            detail.Proc_VendorPaymentMasId = Proc_VendorPaymentMasId;
                            detail.Proc_MaterialEntryDetId = item.Proc_MaterialEntryDetId;
                            detail.BillNo = item.BillNo;
                            detail.PayAmt = item.Payment;
                            detail.Remarks = item.Remarks;
                            db.Proc_VendorPaymentDet.Add(detail);
                            db.SaveChanges();
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
                    flag = false,
                    message = "This Vendor and Date already exists!"
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public JsonResult VendorPaymentUpdate(IEnumerable<VendorPaymentEditDetail> PaymentItems, int?[] DeleteItems, int VendorId, int VendorMasId, DateTime PayDate)
        {
            var result = new
            {
                flag = false,
                message = "Vendor Payment saving error !"
            };

            if (DeleteItems != null)
            {
                foreach (var i in DeleteItems)
                {
                    //var delteItem = db.Proc_MaterialEntryDet.SingleOrDefault(x => x.ItemId == i && x.Proc_PurchaseOrderMasId == ProcPurchaseMasterId);
                    var entryDetId = db.Proc_VendorPaymentDet.Find(i);
                    db.Proc_VendorPaymentDet.Remove(entryDetId);
                    db.SaveChanges();

                }
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {

                    var flag = false;
                    var master = db.Proc_VendorPaymentMas.Find(VendorMasId);
                    master.VendorId = VendorId;
                    master.VPDate = PayDate;
                    db.Entry(master).State = EntityState.Modified;
                    flag = db.SaveChanges() > 0;

                    var VendorMasIdAfterSave = master.Id;

                    if (flag)
                    {
                        foreach (var item in PaymentItems)
                        {
                            var check = db.Proc_VendorPaymentDet.FirstOrDefault(x => x.Id == item.Proc_VendorPaymentDetId /*x.Proc_VendorPaymentMasId == VendorMasIdAfterSave*/);
                            if (check == null)
                            {
                                Proc_VendorPaymentDet detail = new Proc_VendorPaymentDet();
                                detail.Proc_VendorPaymentMasId = VendorMasIdAfterSave;
                                detail.Proc_MaterialEntryDetId = item.Proc_MaterialEntryDetId;
                                detail.BillNo = item.BillNo;
                                detail.PayAmt = item.Payment;
                                detail.Remarks = item.Remarks;
                                db.Entry(detail).State = EntityState.Added;
                                db.SaveChanges();
                            }
                            else
                            {

                                var Proc_VendorPayDet = db.Proc_VendorPaymentDet.FirstOrDefault(x => x.Id == item.Proc_VendorPaymentDetId);
                                var getItem = db.Proc_VendorPaymentDet.Find(Proc_VendorPayDet.Id);
                                getItem.PayAmt = item.Payment;
                                getItem.Proc_MaterialEntryDetId = item.Proc_MaterialEntryDetId;
                                getItem.Remarks = item.Remarks;
                                getItem.BillNo = item.BillNo;

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








        public JsonResult getEditPaymentItems(int vPayId)
        {
            bool flag = false;

            var check = db.Proc_VendorPaymentDet.Where(x => x.Proc_VendorPaymentMasId == vPayId).Count();
            if (check > 0)
            {
                flag = true;
                var data = db.Proc_VendorPaymentDet.Where(x => x.Proc_VendorPaymentMasId == vPayId).ToList();

                List<VendorPaymentEditDetail> detailsList = new List<VendorPaymentEditDetail>();
                foreach (var i in data)
                {
                    VendorPaymentEditDetail vm = new VendorPaymentEditDetail();
                    vm.Proc_VendorPaymentDetId = i.Id;
                    vm.Proc_MaterialEntryDetId = i.Proc_MaterialEntryDetId;

                    // vm.Proc_VendorPaymentMasId = i.Proc_VendorPaymentMasId;
                    //var projectId = (from vPayMas in db.Proc_VendorPaymentMas
                    //                 join tenderDet in db.Proc_TenderDet on vPayMas.VendorId equals tenderDet.VendorId
                    //                 join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                    //                 join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //                 join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                    //                 join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                    //                 join project in db.Project on site.ProjectId equals project.Id
                    //                 where vPayMas.Id == i.Proc_VendorPaymentMasId
                    //                 select project).FirstOrDefault();

                    //var projectId = (from vendorPayMas in db.Proc_VendorPaymentMas
                    //                 join vendorPayDet in db.Proc_VendorPaymentDet on vendorPayMas.Id equals vendorPayDet.Proc_VendorPaymentMasId
                    //                 join entryDet in db.Proc_MaterialEntryDet on vendorPayDet.Proc_MaterialEntryDetId equals entryDet.Id
                    //                 join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                    //                 join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                    //                 join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                    //                 join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                    //                 join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                    //                 join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //                 join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                    //                 join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                    //                 join project in db.Project on site.ProjectId equals project.Id
                    //                 where vendorPayMas.Id == vPayId
                    //                 select new { project, site, requisitionMas }).FirstOrDefault();

                    var projectId = (from vendorMas in db.Proc_VendorPaymentMas
                                         join vendorDet in db.Proc_VendorPaymentDet on vendorMas.Id equals vendorDet.Proc_VendorPaymentMasId
                                         join entryDet in db.Proc_MaterialEntryDet on vendorDet.Proc_MaterialEntryDetId equals entryDet.Id
                                         join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                                         join purchaseMas in db.Proc_PurchaseOrderMas on new { a = purchaseDet.Proc_PurchaseOrderMasId, b = vendorMas.VendorId } equals new { a = purchaseMas.Id, b = purchaseMas.VendorId }
                                         join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                                         join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                                         join requisitionDet in db.Proc_RequisitionDet on new { a = tenderDet.Proc_RequisitionDetId, b = purchaseDet.ItemId }
                                         equals new { a = requisitionDet.Id, b = requisitionDet.ItemId }
                                         join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                                         join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                                         join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                                         join project in db.Project on site.ProjectId equals project.Id
                                         join items in db.Item on purchaseDet.ItemId equals items.Id
                                         where vendorMas.Id == vPayId
                                         && requisitionDet.ItemId == i.Proc_MaterialEntryDet.Proc_PurchaseOrderDet.ItemId
                                         select new
                                         {
                                             project,
                                             site,
                                             requisitionMas,
                                             tenderDet,
                                             purchaseDet
                                         }).Distinct().FirstOrDefault();





                    vm.ProjectId = projectId.project.Id;
                    vm.ProjectName = projectId.project.Name;

                    //var siteId = (from vPayMas in db.Proc_VendorPaymentMas
                    //              join tenderDet in db.Proc_TenderDet on vPayMas.VendorId equals tenderDet.VendorId
                    //              join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                    //              join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //              join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                    //              join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                    //              where vPayMas.Id == i.Proc_VendorPaymentMasId
                    //              select site).FirstOrDefault();

                    //vm.SiteId = siteId.Id;
                    //vm.SiteName = siteId.Name;

                    vm.SiteId = projectId.site.Id;
                    vm.SiteName = projectId.site.Name;

                    var requisitionId = (from vPayMas in db.Proc_VendorPaymentMas
                                         join vPayDet in db.Proc_VendorPaymentDet on vPayMas.Id equals vPayDet.Proc_VendorPaymentMasId
                                         join metEntryDet in db.Proc_MaterialEntryDet on vPayDet.Proc_MaterialEntryDetId equals metEntryDet.Id
                                         join purDet in db.Proc_PurchaseOrderDet on metEntryDet.Proc_PurchaseOrderDetId equals purDet.Id
                                         join purMas in db.Proc_PurchaseOrderMas on purDet.Proc_PurchaseOrderMasId equals purMas.Id
                                         join tendermas in db.Proc_TenderMas on purMas.Proc_TenderMasId equals tendermas.Id
                                         join tenderDet in db.Proc_TenderDet on tendermas.Id equals tenderDet.Proc_TenderMasId
                                         join reqDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals reqDet.Id
                                         join reqMas in db.Proc_RequisitionMas on reqDet.Proc_RequisitionMasId equals reqMas.Id
                                         where vPayMas.Id == vPayId && metEntryDet.Id == i.Proc_MaterialEntryDetId && metEntryDet.Proc_PurchaseOrderDetId == i.Proc_MaterialEntryDet.Proc_PurchaseOrderDetId
                                         select new { reqMas, purMas, metEntryDet }).FirstOrDefault();


                    vm.ReqNo = db.Proc_RequisitionMas.SingleOrDefault(x => x.Rcode == projectId.requisitionMas.Rcode).Id;
                    vm.ReqNoName = projectId.requisitionMas.Rcode;

                    //var ItemId = (from vendorPayMas in db.Proc_VendorPaymentMas
                    //              join vendorPayDet in db.Proc_VendorPaymentDet on vendorPayMas.Id equals vendorPayDet.Proc_VendorPaymentMasId
                    //              join entryDet in db.Proc_MaterialEntryDet on vendorPayDet.Proc_MaterialEntryDetId equals entryDet.Id
                    //              join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                    //              join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                    //              join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                    //              join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                    //              join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                    //              join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //              join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                    //              join procProjectItem in db.ProcProjectItem on procProject.Id equals procProjectItem.ProcProjectId
                    //              join items in db.Item on procProjectItem.ItemId equals items.Id
                    //              where vendorPayMas.Id == vPayId && purchaseDet.ItemId == requisitionDet.ItemId && requisitionDet.ItemId==items.Id && entryDet.Id == vm.Proc_MaterialEntryDetId && vendorPayDet.Id == vm.Proc_VendorPaymentDetId
                    //              select items).Distinct().SingleOrDefault();



                    var ItemId = (from vendorPayMas in db.Proc_VendorPaymentMas
                                  join vendorPayDet in db.Proc_VendorPaymentDet on vendorPayMas.Id equals vendorPayDet.Proc_VendorPaymentMasId
                                  join entryDet in db.Proc_MaterialEntryDet on vendorPayDet.Proc_MaterialEntryDetId equals entryDet.Id
                                  join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                                  join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                                  join items in db.Item on purchaseDet.ItemId equals items.Id
                                  where vendorPayDet.Id == i.Id && purchaseDet.ItemId == items.Id
                                  select items).Distinct().SingleOrDefault();


                    vm.ItemId = ItemId.Id;
                    vm.ItemName = ItemId.Name;


                    var quantity = db.Proc_RequisitionDet.FirstOrDefault(x => x.ItemId == ItemId.Id).ReqQty;
                    vm.Qty = quantity;


                    var dataUnits = (from procProjectItem in db.ProcProjectItem
                                     join units in db.Unit on procProjectItem.UnitId equals units.Id
                                     join procReqDet in db.Proc_RequisitionDet on procProjectItem.ItemId equals procReqDet.ItemId
                                     join procReqMas in db.Proc_RequisitionMas on procReqDet.Proc_RequisitionMasId equals procReqMas.Id
                                     where procProjectItem.ItemId == ItemId.Id
                                     select units).FirstOrDefault();



                    /*var unit = (from reqMas in db.Proc_RequisitionMas
                                join procProject in db.ProcProject on reqMas.ProcProjectId equals procProject.Id                                
                                join ProcProjectItem in db.ProcProjectItem on procProject.Id equals ProcProjectItem.ProcProjectId
                                join reqDet in db.Proc_RequisitionDet on ProcProjectItem.ItemId equals reqDet.ItemId
                                join units in db.Unit on ProcProjectItem.UnitId equals units.Id
                                where ProcProjectItem.ItemId == ItemId.Id && procProject.ProjectSiteId == projectId.site.Id
                                select units).SingleOrDefault();*/


                    //var uni = (from vendorPayMas in db.Proc_VendorPaymentMas
                    //           join vendorPayDet in db.Proc_VendorPaymentDet on vendorPayMas.Id equals vendorPayDet.Proc_VendorPaymentMasId
                    //           join entryDet in db.Proc_MaterialEntryDet on vendorPayDet.Proc_MaterialEntryDetId equals entryDet.Id
                    //           join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                    //           join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                    //           join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                    //           join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                    //           join requisitionDet in db.Proc_RequisitionDet on tenderDet.Proc_RequisitionDetId equals requisitionDet.Id
                    //           join requisitionMas in db.Proc_RequisitionMas on requisitionDet.Proc_RequisitionMasId equals requisitionMas.Id
                    //           join procProject in db.ProcProject on requisitionMas.ProcProjectId equals procProject.Id
                    //           join procItem in db.ProcProjectItem on procProject.Id equals procItem.ProcProjectId
                    //           join units in db.Unit on procItem.UnitId equals units.Id
                    //           join site in db.ProjectSite on procProject.ProjectSiteId equals site.Id
                    //           join project in db.Project on site.ProjectId equals project.Id
                    //           where vendorPayMas.Id == vPayId && procProject.ProjectSiteId == projectId.site.Id
                    //           select procItem).FirstOrDefault();





                    //past error shows
                    //var uni = (from procProject in db.ProcProject
                    //           join ProcProjectItem in db.ProcProjectItem on procProject.Id equals ProcProjectItem.ProcProjectId
                    //           join units in db.Unit on ProcProjectItem.UnitId equals units.Id
                    //           where ProcProjectItem.ItemId == ItemId.Id && procProject.ProjectSiteId == projectId.site.Id
                    //           select units).SingleOrDefault();

                    vm.UnitId = dataUnits.Id;
                    vm.UnitName = dataUnits.Name;

                    if (i.BillNo == "" || i.BillNo == null)
                    {
                        vm.BillNo = "";
                    }
                    else
                    {
                        vm.BillNo = i.BillNo;
                    }

                    vm.Payment = i.PayAmt;
                    if (i.Remarks == null || i.Remarks == "")
                    {
                        vm.Remarks = "";
                    }
                    else
                    {
                        vm.Remarks = i.Remarks;
                    }


                    vm.PONo = db.Proc_PurchaseOrderMas.SingleOrDefault(x => x.PONo == requisitionId.purMas.PONo).Id;
                    vm.PONoName = requisitionId.purMas.PONo;

                    //vm.ChallanNo = db.Proc_MaterialEntryDet.SingleOrDefault(x => x.ChallanNo == requisitionId.metEntryDet.ChallanNo).Id;
                    var challan = db.Proc_MaterialEntryDet.SingleOrDefault(x => x.Id == i.Proc_MaterialEntryDetId);
                    vm.ChallanNo = challan.Id;
                    vm.ChallanNoName = challan.ChallanNo;
                    //vm.ChallanNoName = requisitionId.metEntryDet.ChallanNo;

                    //var getUnitPrice = (from vendorPayMas in db.Proc_VendorPaymentMas
                    //                    join vendorPayDet in db.Proc_VendorPaymentDet on vendorPayMas.Id equals vendorPayDet.Proc_VendorPaymentMasId
                    //                    join entryDet in db.Proc_MaterialEntryDet on vendorPayDet.Proc_MaterialEntryDetId equals entryDet.Id
                    //                    join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                    //                    join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                    //                    join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                    //                    join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                    //                    where entryDet.ChallanNo == vendorPayDet.Proc_MaterialEntryDet.ChallanNo && purchaseDet.ItemId == ItemId.Id
                    //                    select new { entryDet, tenderDet }).FirstOrDefault();

                    //var qty = getUnitPrice.entryDet.EntryQty;
                    //var unitPrice = getUnitPrice.tenderDet.TQPrice;



                    //past working
                    //var getUnitPrice = (from vendorPayMas in db.Proc_VendorPaymentMas
                    //                    join vendorPayDet in db.Proc_VendorPaymentDet on vendorPayMas.Id equals vendorPayDet.Proc_VendorPaymentMasId
                    //                    join entryDet in db.Proc_MaterialEntryDet on vendorPayDet.Proc_MaterialEntryDetId equals entryDet.Id
                    //                    join purchaseDet in db.Proc_PurchaseOrderDet on entryDet.Proc_PurchaseOrderDetId equals purchaseDet.Id
                    //                    join purchaseMas in db.Proc_PurchaseOrderMas on purchaseDet.Proc_PurchaseOrderMasId equals purchaseMas.Id
                    //                    join tenderMas in db.Proc_TenderMas on purchaseMas.Proc_TenderMasId equals tenderMas.Id
                    //                    join tenderDet in db.Proc_TenderDet on tenderMas.Id equals tenderDet.Proc_TenderMasId
                    //                    where entryDet.ChallanNo == vendorPayDet.Proc_MaterialEntryDet.ChallanNo && entryDet.Id == i.Proc_MaterialEntryDetId
                    //                    select new { purchaseDet, tenderDet }).FirstOrDefault();

                    var qty = projectId.purchaseDet.POQty;
                    var unitPrice = projectId.tenderDet.TQPrice;

                    var totalAmt = qty * unitPrice;

                    vm.TotalAmt = totalAmt;

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





        public JsonResult DeleteVendorPayment(int VPayId)
        {
            bool flag = false;
            try
            {
                var itemsToDeletePaymentDet = db.Proc_VendorPaymentDet.Where(x => x.Proc_VendorPaymentMasId == VPayId);
                db.Proc_VendorPaymentDet.RemoveRange(itemsToDeletePaymentDet);

                var itemsToDeletePaymentMas = db.Proc_VendorPaymentMas.Where(x => x.Id == VPayId);
                db.Proc_VendorPaymentMas.RemoveRange(itemsToDeletePaymentMas);

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
                    message = "Material Entry deletion successful."
                };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var result = new
                {
                    flag = false,
                    message = "Material Entry deletion failed!\nError Occured."
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }




        public ActionResult DeleteVendorPaymentDetails(int VendorDetailId)
        {
            //var flag = false;
            var result = new
            {
                flag = false,
                message = "Delete error !"
            };


            // Proc_PurchaseOrderDet data = db.Proc_PurchaseOrderDet.Find(PurchaseOrderDetId);
            var checkVendorPaymentDet = db.Proc_VendorPaymentDet.Where(x => x.Id == VendorDetailId).ToList();
            if (checkVendorPaymentDet.Count == 0)
            {
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
                    flag = true,
                    message = "This item has been used!"
                };
            }
            //if (checkVendorPaymentDet != null)
            //{
            //    var data = db.Proc_VendorPaymentDet.Where(x => x.Id == Proc_VendorPaymentDetId).FirstOrDefault();
            //    db.Proc_VendorPaymentDet.Remove(data);
            //    flag = db.SaveChanges() > 0;

            //    if (flag == true)
            //    {
            //        result = new
            //        {
            //            flag = true,
            //            message = "Delete Successful Successful!"
            //        };
            //    }

            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}

            //else
            //{
            //    result = new
            //    {
            //        flag = false,
            //        message = "Delete Failed!"
            //    };
            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}

            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}