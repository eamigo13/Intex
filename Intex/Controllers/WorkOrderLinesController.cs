using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intex.DAL;
using Intex.Models;

namespace Intex.Controllers
{
    public class WorkOrderLinesController : Controller
    {
        private IntexContext db = new IntexContext();

        // GET: WorkOrderLine
        public ActionResult Index()
        {
            var WorkOrderLine = db.WorkOrderLine.Include(w => w.Assay).Include(w => w.Sample).Include(w => w.WorkOrder);
            return View(WorkOrderLine.ToList());
        }

        // GET: WorkOrderLine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderLine workOrderLine = db.WorkOrderLine.Find(id);
            if (workOrderLine == null)
            {
                return HttpNotFound();
            }
            return View(workOrderLine);
        }

        // GET: WorkOrderLine/Create
        public ActionResult Create()
        {
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName");
            ViewBag.SampleID = new SelectList(db.Samples, "SampleID", "ReceivingNotes");
            ViewBag.OrderNumber = new SelectList(db.WorkOrders, "OrderNumber", "CustomerID");
            return View();
        }

        // POST: WorkOrderLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderNumber,OrderLine,SampleID,AssayID,LineCost")] WorkOrderLine workOrderLine)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrderLine.Add(workOrderLine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", workOrderLine.AssayID);
            ViewBag.SampleID = new SelectList(db.Samples, "SampleID", "ReceivingNotes", workOrderLine.SampleID);
            ViewBag.OrderNumber = new SelectList(db.WorkOrders, "OrderNumber", "CustomerID", workOrderLine.OrderNumber);
            return View(workOrderLine);
        }

        // GET: WorkOrderLine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderLine workOrderLine = db.WorkOrderLine.Find(id);
            if (workOrderLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", workOrderLine.AssayID);
            ViewBag.SampleID = new SelectList(db.Samples, "SampleID", "ReceivingNotes", workOrderLine.SampleID);
            ViewBag.OrderNumber = new SelectList(db.WorkOrders, "OrderNumber", "CustomerID", workOrderLine.OrderNumber);
            return View(workOrderLine);
        }

        // POST: WorkOrderLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderNumber,OrderLine,SampleID,AssayID,LineCost")] WorkOrderLine workOrderLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrderLine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssayID = new SelectList(db.Assays, "AssayID", "AssayName", workOrderLine.AssayID);
            ViewBag.SampleID = new SelectList(db.Samples, "SampleID", "ReceivingNotes", workOrderLine.SampleID);
            ViewBag.OrderNumber = new SelectList(db.WorkOrders, "OrderNumber", "CustomerID", workOrderLine.OrderNumber);
            return View(workOrderLine);
        }

        // GET: WorkOrderLine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderLine workOrderLine = db.WorkOrderLine.Find(id);
            if (workOrderLine == null)
            {
                return HttpNotFound();
            }
            return View(workOrderLine);
        }

        // POST: WorkOrderLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrderLine workOrderLine = db.WorkOrderLine.Find(id);
            db.WorkOrderLine.Remove(workOrderLine);
            db.SaveChanges();
            return RedirectToAction("Index");
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
