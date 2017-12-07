using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Intex.DAL;
using Intex.Models;
using System.Data.Entity.Validation;

namespace Intex.Controllers
{
    [Authorize]
    public class WorkOrdersController : Controller
    {
        private IntexContext db = new IntexContext();
        
        // GET: WorkOrders
        public ActionResult Index()
        {
            var workOrders = db.WorkOrders.Include(w => w.Customer).Include(w => w.OrderStatus).Include(w => w.OrderType);
            return View(workOrders.ToList());
        }
        
        // GET:  Quote
        public ActionResult Quote(int? workOrderID)
        {
            string custID = User.Identity.GetUserId();

            //Create a selectlist to power a dropdown list of available Assays
            //ViewBag.Assays = new SelectList(db.Assays, "AssayID", "AssayName");
            ViewBag.Assays = db.Assays;

            //Create a selectlist to power a dropdown list of compounds associated witht the customer account
            ViewBag.Compounds = db.Compounds.Where(x => x.CustomerID == custID); //Set to be nick
            ViewBag.workOrderID = workOrderID;

            if (workOrderID != null)
            {

            }
            else
            {
                workOrderID = 1;
            }

            //Pass the WorkOrderLines associated with the selected work order into the view
            var lines = db.WorkOrderLine.Where(x => x.OrderNumber == workOrderID);

            return View(lines.ToList());
        }

        
        public ActionResult DeleteLine(int orderNumber, int lineID, int sampleID)
        {
            WorkOrderLine workOrderLine = db.WorkOrderLine.Find(orderNumber, lineID);
            db.WorkOrderLine.Remove(workOrderLine);

            Sample sample = db.Samples.Find(sampleID);
            db.Samples.Remove(sample);

            db.SaveChanges();

            return RedirectToAction("Quote", new { workOrderNumber = orderNumber });
        }

        //GET: WorkOrders/ReceiveSample
        public ActionResult ReceiveSample(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sample Sample = db.Samples.Find(id);

            ViewBag.Compound = db.Compounds.Find(Sample.CompoundID);
            WorkOrderLine line = db.WorkOrderLine.Where(x => x.SampleID == id).First();
            ViewBag.OrderLine = line;

            if (Sample == null)
            {
                return HttpNotFound();
            }
            return View(Sample);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Quote([Bind(Include = "CompoundID,ReportedQty")] Sample sample,
                                  [Bind(Include = "OrderLine,OrderNumber,AssayID")] WorkOrderLine line)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Samples.Add(sample);  //Check if we can access the AutoIncremented sampleID
                    db.SaveChanges();

                    line.SampleID = sample.SampleID;
                    db.WorkOrderLine.Add(line);
                    db.SaveChanges();

                    return RedirectToAction("Quote", new { workOrderID = line.OrderNumber });
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
            
            return RedirectToAction("Quote", new { workOrderID = line.OrderNumber });
        }

        // GET: WorkOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            ViewBag.StatusID = new SelectList(db.OrderStatus, "StatusID", "Status");
            ViewBag.OrderTypeID = new SelectList(db.OrderTypes, "OrderTypeID", "OrderTypeDesc");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderNumber,OrderTypeID,CustomerID,QuoteDate,OrderDate,StatusID,Comments")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrders.Add(workOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", workOrder.CustomerID);
            ViewBag.StatusID = new SelectList(db.OrderStatus, "StatusID", "Status", workOrder.StatusID);
            ViewBag.OrderTypeID = new SelectList(db.OrderTypes, "OrderTypeID", "OrderTypeDesc", workOrder.OrderTypeID);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", workOrder.CustomerID);
            ViewBag.StatusID = new SelectList(db.OrderStatus, "StatusID", "Status", workOrder.StatusID);
            ViewBag.OrderTypeID = new SelectList(db.OrderTypes, "OrderTypeID", "OrderTypeDesc", workOrder.OrderTypeID);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderNumber,OrderTypeID,CustomerID,QuoteDate,OrderDate,StatusID,Comments")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", workOrder.CustomerID);
            ViewBag.StatusID = new SelectList(db.OrderStatus, "StatusID", "Status", workOrder.StatusID);
            ViewBag.OrderTypeID = new SelectList(db.OrderTypes, "OrderTypeID", "OrderTypeDesc", workOrder.OrderTypeID);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrder workOrder = db.WorkOrders.Find(id);
            db.WorkOrders.Remove(workOrder);
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
