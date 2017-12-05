using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intex.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public FileStreamResult Report()
        {
            MemoryStream workStream = new MemoryStream();
            Document document = new Document(PageSize.A4, 10, 10, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, workStream);
            writer.CloseStream = false;
            document.Open();
            /** All document changes go below this line **/

            /* Header */
            Paragraph header = new Paragraph();
            Chunk glue = new Chunk(new VerticalPositionMark());
            header.Add(new Phrase("Northwest Labs"));
            Image logo = Image.GetInstance(Server.MapPath("~/Content/img/report_brand.png"));
            logo.ScaleAbsolute(16, 20);
            header.Add(new Chunk(logo, 5, -5));
            header.Add(new Chunk(glue));
            header.Add(new Phrase("Report generated at: " + DateTime.Now.ToString()));
            document.Add(header);

            /* Two horizontal rules */
            PdfContentByte cb = writer.DirectContent;
            cb.MoveTo(10, document.PageSize.Height - 38);
            cb.LineTo(document.PageSize.Width - 10, document.PageSize.Height - 38);
            cb.Stroke();
            cb.MoveTo(10, document.PageSize.Height - 40);
            cb.LineTo(document.PageSize.Width - 10, document.PageSize.Height - 40);
            cb.Stroke();

            /* Report Table */

            PdfPTable report_table = new PdfPTable(3);
            PdfPCell header_cell = new PdfPCell(new Phrase("Report data"));
            header_cell.Colspan = 3;
            header_cell.HorizontalAlignment = 1;
            report_table.AddCell(header_cell);

            report_table.AddCell("Assay");
            report_table.AddCell("Result");
            report_table.AddCell("Retest?");

            report_table.AddCell("Evan ;)");
            report_table.AddCell("Positive");
            report_table.AddCell("No");

            report_table.AddCell("Riley ;)");
            report_table.AddCell("Positive");
            report_table.AddCell("Yes");

            report_table.AddCell("Andrew");
            report_table.AddCell("Negative");
            report_table.AddCell("Yes");

            document.Add(report_table);

            /** All document changes go above this line **/
            document.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }
    }
}