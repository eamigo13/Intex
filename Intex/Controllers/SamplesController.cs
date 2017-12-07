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
    public class SamplesController : Controller
    {
        private IntexContext db = new IntexContext();

        // GET: Samples
        public ActionResult Index()
        {
            var samples = db.Samples.Include(s => s.Appearance).Include(s => s.Compound).Include(s => s.Customer);
            return View(samples.ToList());
        }

        // GET: Samples/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            return View(sample);
        }

        // GET: Samples/Create
        public ActionResult Create()
        {
            ViewBag.AppearanceID = new SelectList(db.Appearances, "AppearanceID", "AppearanceDesc");
            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CustomerID");
            ViewBag.ReceivedBy = new SelectList(db.Customers, "CustomerID", "FirstName");
            return View();
        }

        // POST: Samples/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SampleID,CompoundID,ReportedQty,MeasuredQty,DateArrived,ReceivedBy,ReceivingNotes,MTD,AppearanceID")] Sample sample)
        {
            if (ModelState.IsValid)
            {
                db.Samples.Add(sample);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppearanceID = new SelectList(db.Appearances, "AppearanceID", "AppearanceDesc", sample.AppearanceID);
            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CustomerID", sample.CompoundID);
            ViewBag.ReceivedBy = new SelectList(db.Customers, "CustomerID", "FirstName", sample.ReceivedBy);
            return View(sample);
        }

        // GET: Samples/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppearanceID = new SelectList(db.Appearances, "AppearanceID", "AppearanceDesc", sample.AppearanceID);
            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CustomerID", sample.CompoundID);
            ViewBag.ReceivedBy = new SelectList(db.Customers, "CustomerID", "FirstName", sample.ReceivedBy);
            return View(sample);
        }

        // POST: Samples/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SampleID,CompoundID,ReportedQty,MeasuredQty,DateArrived,ReceivedBy,ReceivingNotes,MTD,AppearanceID")] Sample sample)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sample).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppearanceID = new SelectList(db.Appearances, "AppearanceID", "AppearanceDesc", sample.AppearanceID);
            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CustomerID", sample.CompoundID);
            ViewBag.ReceivedBy = new SelectList(db.Customers, "CustomerID", "FirstName", sample.ReceivedBy);
            return View(sample);
        }

        // GET: Samples/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            return View(sample);
        }

        // POST: Samples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sample sample = db.Samples.Find(id);
            db.Samples.Remove(sample);
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
