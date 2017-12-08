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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.IO;

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

            ViewBag.CurrentUser = User.Identity.GetUserId();

            if (Sample == null)
            {
                return HttpNotFound();
            }
            return View(Sample);
        }

        //GET: WorkOrders/ReceiveSampleConfirmation
        public ActionResult ReceiveSampleConfirmation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sample Sample = db.Samples.Find(id);

            ViewBag.Compound = db.Compounds.Find(Sample.CompoundID);

            WorkOrderLine line = db.WorkOrderLine.Where(x => x.SampleID == id).First();
            ViewBag.OrderLine = line;

            ViewBag.Assay = db.Assays.Find(line.AssayID);

            ViewBag.CurrentUser = db.Customers.Find(User.Identity.GetUserId());


            if (Sample == null)
            {
                return HttpNotFound();
            }
            return View(Sample);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiveSample([Bind(Include = "SampleID,CompoundID,ReportedQty,MeasuredQty,DateArrived,ReceivedBy,ReceivingNotes,MTD,AppearanceID")] Sample sample)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sample).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ReceiveSampleConfirmation", new { id = sample.SampleID });
            }

            return RedirectToAction("ReceiveSampleConfirmation", new { id = sample.SampleID });

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

        // GET:  Work Order after Results.  For employees only.
        public ActionResult ViewOrder(int workOrderID)
        {
            //Pass the WorkOrderLines associated with the selected work order into the view
            var lines = db.WorkOrderLine.Where(x => x.OrderNumber == workOrderID);

            return View(lines.ToList());
        }

        public FileStreamResult Report(int workOrderID)
        {
            // Get the work order for the report
            WorkOrder woForReport = db.WorkOrders.Find(workOrderID);

            // Get each work order line
            var lines = db.WorkOrderLine.Where(x => x.OrderNumber == workOrderID).ToList();

            // Get the customer information
            Customer woCustomer = db.Customers.Find(woForReport.CustomerID);

            // Load tests for each sample/line in the work order
            var tests = db.Tests.Where(x => x.OrderNumber == workOrderID).ToList();

            // Load test results for each test
            var results = db.Results.Where(x => x.PassFail != null).ToList();

            // Load test types for comparison
            var types = db.TestTypes.Where(x => x.TestName != null).ToList();
            
            // Start constructing the PDF
            MemoryStream workStream = new MemoryStream();
            Document document = new Document(PageSize.A4, 10, 10, 10, 20);
            PdfWriter writer = PdfWriter.GetInstance(document, workStream);
            writer.CloseStream = false;
            document.Open();
            /** All document changes go below this line **/


            /* Fonts and styles */

            //For Headers
            BaseFont bfCourier = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, false);
            Font headingFont = new Font(bfCourier, 24.0f, Font.BOLD, BaseColor.BLUE);

            //For cell headings
            BaseFont bfArial = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, false);
            Font cellHeadFont = new Font(bfArial, 16.0f, Font.NORMAL, BaseColor.WHITE);

            //For passed tests
            Font testPassFont = new Font(bfArial, 15.0f, Font.BOLD, new BaseColor(1, 198, 87));

            //For failed tests
            Font testFailFont = new Font(bfArial, 15.0f, Font.BOLD, new BaseColor(255, 90, 61));


            /* Header */
            Paragraph header = new Paragraph();
            Chunk glue = new Chunk(new VerticalPositionMark());
            Image logo = Image.GetInstance(Server.MapPath("~/Content/img/report_header.png"));
            logo.ScaleAbsolute(document.PageSize.Width - 20, 80);
            header.Add(new Chunk(logo, 0, -65));
            document.Add(header);

            /* Two horizontal rules */
            PdfContentByte cb = writer.DirectContent;
            cb.MoveTo(10, document.PageSize.Height - 110);
            cb.LineTo(document.PageSize.Width - 10, document.PageSize.Height - 110);
            cb.Stroke();
            cb.MoveTo(10, document.PageSize.Height - 111);
            cb.LineTo(document.PageSize.Width - 10, document.PageSize.Height - 111);
            cb.Stroke();

            /* Break */
            document.Add(new Chunk("\n\n\n\n\n\n\n\n\n"));

            /* Address to client */

            PdfPTable client_memo = new PdfPTable(3);
            client_memo.DefaultCell.Border = Rectangle.NO_BORDER;

            client_memo.AddCell(woCustomer.FirstName + " " + woCustomer.LastName);
            client_memo.AddCell("");
            client_memo.AddCell("Work Order: " + woForReport.OrderNumber);

            client_memo.AddCell(woCustomer.City + ", " + woCustomer.State.StateName);
            client_memo.AddCell("");
            client_memo.AddCell("Ordered: " + (woForReport.OrderDate).ToShortDateString());

            client_memo.AddCell(woCustomer.Address);
            client_memo.AddCell("");
            client_memo.AddCell("Completed: " + DateTime.Now.ToShortDateString());           

            document.Add(client_memo);

            /* Break */

            document.Add(new Chunk("\n"));

            /* Introduce test results section */

            var testIntro = new PdfPTable(1);
            testIntro.DefaultCell.Border = Rectangle.NO_BORDER;
            testIntro.WidthPercentage = 100;

            PdfPCell introText = new PdfPCell( new Phrase("TEST RESULTS", headingFont));
            introText.Border = Rectangle.NO_BORDER;
            introText.HorizontalAlignment = Element.ALIGN_CENTER;

            testIntro.AddCell(introText);

            document.Add(testIntro);

            /* Break */

            document.Add(new Chunk("\n"));

            /* Report Table */

            var reportTable = new PdfPTable(3);
            reportTable.SetWidths(new float[] { 25, 35, 40 });
            reportTable.DefaultCell.Border = Rectangle.NO_BORDER;

            //Set outer table header
            var hText = new Phrase("Line #", cellHeadFont);
            var hCell = new PdfPCell(hText);
            hCell.Border = Rectangle.NO_BORDER;
            hCell.BackgroundColor = BaseColor.DARK_GRAY;
            hCell.AddElement(hText);
            reportTable.AddCell(hCell);  // Line number

            hText = new Phrase("Compound", cellHeadFont);
            hCell = new PdfPCell(hText);
            hCell.Border = Rectangle.NO_BORDER;
            hCell.BackgroundColor = BaseColor.DARK_GRAY;
            hCell.AddElement(hText);
            reportTable.AddCell(hCell); // Compound name

            hText = new Phrase("Assay", cellHeadFont);
            hCell = new PdfPCell(hText);
            hCell.Border = Rectangle.NO_BORDER;
            hCell.BackgroundColor = BaseColor.DARK_GRAY;
            hCell.AddElement(hText);
            reportTable.AddCell(hCell);  // Assay name

            /* Break */
            reportTable.AddCell(" ");
            reportTable.AddCell(" ");
            reportTable.AddCell(" ");

            foreach (var line in lines)
            {
                //Set line item details as a header
                var liText = new Phrase(line.OrderLine.ToString());
                var liCell = new PdfPCell(liText);
                liCell.BackgroundColor = new BaseColor(255, 250, 191);
                liCell.AddElement(liText);
                reportTable.AddCell(liCell); // Line number
                
                liText = new Phrase(line.Sample.Compound.CompoundName.ToString());
                liCell = new PdfPCell(liText);
                liCell.BackgroundColor = new BaseColor(255, 250, 191);
                liCell.AddElement(liText);
                reportTable.AddCell(liCell); // Compound name

                liText = new Phrase(line.Assay.AssayName.ToString());
                liCell = new PdfPCell(liText);
                liCell.BackgroundColor = new BaseColor(255, 250, 191);
                liCell.AddElement(liText);
                reportTable.AddCell(liCell); // Assay name

                var thisLinesTests = tests.Where(x => x.OrderLine == line.OrderLine).ToList();

                foreach (var test in thisLinesTests)
                {
                    var thisTestsType = types.FirstOrDefault(x => x.TestTypeID == test.TestTypeID);
                    var thisTestsResults = results.FirstOrDefault(x => x.TestID == test.TestID);

                    //Output results for the test
                    var trText = new Phrase(thisTestsType.TestName.ToString());
                    var trCell = new PdfPCell(trText);
                    trCell.BackgroundColor = new BaseColor(232, 239, 252);
                    trCell.AddElement(trText);
                    reportTable.AddCell(trCell); // Test name
                    
                    trText = new Phrase(thisTestsResults.Comments.ToString());
                    trCell = new PdfPCell(trText);
                    trCell.BackgroundColor = new BaseColor(232, 239, 252);
                    trCell.AddElement(trText);
                    reportTable.AddCell(trCell); // Test comments
                    
                    trText = new Phrase(thisTestsResults.PassFail.ToString(),
                                        thisTestsResults.PassFail == "Pass" ? testPassFont : testFailFont);
                    trCell = new PdfPCell(trText);
                    trCell.BackgroundColor = new BaseColor(232, 239, 252);
                    trCell.AddElement(trText);
                    reportTable.AddCell(trCell); // Test result

                }

                /* Break */
                reportTable.AddCell(" ");
                reportTable.AddCell(" ");
                reportTable.AddCell(" ");

            }

            document.Add(reportTable);

            /* Footer */


            /** All document changes go above this line **/
            document.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }


    }
}
