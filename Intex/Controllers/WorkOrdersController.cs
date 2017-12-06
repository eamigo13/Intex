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
            IQueryable<OrderCompound> compoundsInOrder;

            if (workOrderID != null)
            {
                WorkOrder activeOrder = db.WorkOrders.Find(workOrderID);

                ViewBag.ActiveOrder = activeOrder;

                compoundsInOrder = from c in db.OrderCompounds
                                       where c.OrderNumber == activeOrder.OrderNumber
                                       select c;
            }
            else
            {
                WorkOrder workOrder = new WorkOrder();
                workOrder.CustomerID = User.Identity.GetUserId();
                workOrder.StatusID = 1;  // New
                if (ModelState.IsValid)
                {
                    db.WorkOrders.Add(workOrder);
                    db.SaveChanges();
                }

                compoundsInOrder = from c in db.OrderCompounds
                                       where c.OrderNumber == workOrder.OrderNumber
                                       select c;
            }
            
            return View(compoundsInOrder.ToList());
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
