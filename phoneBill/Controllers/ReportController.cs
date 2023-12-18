using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Reporting.NETCore;
using phoneBill.Data;
using phoneBill.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace phoneBill.Controllers
{
    public class ReportController : Controller
    {
        private readonly db_phonebillModel _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportController(db_phonebillModel db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            SelectOptionReport();
            return View();

        }

        [HttpPost]
        public IActionResult PrintReport(ReportRequest models)
        {

            var ReportData = new ReportRespose();
            ReportData.ListDataReport = new List<DataReportRespose>();
            List<VBilllist> ListDataBill = new List<VBilllist>();

            if (models.Phonenumber == "all")
            {
                if (models.MonthBill == "all")
                {
                    ListDataBill = _db.VBilllists.Where(s => s.YearBill == models.YearBill && s.DeleteStatus != true).ToList();
                }
                else
                {
                    ListDataBill = _db.VBilllists.Where(s => s.YearBill == models.YearBill && s.DeleteStatus != true && s.MonthBill == models.MonthBill).ToList();
                }
            }
            else if (models.Phonenumber != "all")
            {
                if (models.MonthBill == "all")
                {
                    ListDataBill = _db.VBilllists.Where(s => s.YearBill == models.YearBill && s.DeleteStatus != true && s.Phonenumber == models.Phonenumber).ToList();
                }
                else
                {
                    ListDataBill = _db.VBilllists.Where(s => s.YearBill == models.YearBill && s.DeleteStatus != true && s.Phonenumber == models.Phonenumber && s.MonthBill == models.MonthBill).ToList();
                }
            }

            foreach (var item in ListDataBill)
            {

                ReportData.ListDataReport.Add(new DataReportRespose
                {
                    Phonenumber = item.Phonenumber,
                    Name = item.Name,
                    MonthBill = item.MonthBill,
                    YearBill = item.Dateonly,
                    PromotionCost = (double)item.PromotionCost,
                    ExcessCost = (double)item.ExcessCost,
                    InterCallingCharge = (double)item.InterCallingCharge,
                    AdditionalServiceFee = (double)item.AdditionalServiceFee,
                    TotalService = (double)item.TotalService,
                    VAT = (double)item.VAT,

                });
            }


            if (ReportData.ListDataReport.Count() > 0)
            {
                if (models.TypeReport == "PDF")//GeneratePDF
                {
                    string renderFormat = "PDF";
                    string mimetype = "application/pdf";
                    using var report = new LocalReport();
                    var data = new DataTable();
                    data = GenerateReportPDF(ReportData);
                    report.DataSources.Add(new ReportDataSource("dBillList", data));
                    ReportParameter[] parameters = new ReportParameter[2];
                    parameters[0] = new ReportParameter("Year", models.YearBill);
                    parameters[1] = new ReportParameter("DatePrint", DateTime.Now.ToString());
                    report.ReportPath = $"{this._webHostEnvironment.WebRootPath}\\Report\\ReportBill.rdlc";
                    report.SetParameters(parameters);

                    var pdf = report.Render(format: renderFormat);
                    return File(pdf, mimetype, "RB" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf");
                    //return File(pdf, mimetype,"report."+extension);
                }
                else //GenerateExcel
                {
                    var fileName = "RB"+ DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                    DataTable dataTable = new DataTable("Bill");
                    dataTable.Columns.AddRange(new DataColumn[9] {
                                        new DataColumn("Phonenumber"),
                                        new DataColumn("Name"),
                                        new DataColumn("Mouth"),
                                        new DataColumn("PromotionCost"),
                                        new DataColumn("ExcessCost"),
                                        new DataColumn("InterCallingCharge"),
                                        new DataColumn("AdditionalServiceFee"),
                                        new DataColumn("TotalCost"),
                                        new DataColumn("VAT")
                     });


                    foreach (var item in ReportData.ListDataReport)
                    {
                        dataTable.Rows.Add(
                            item.Phonenumber,
                            item.Name,
                            item.MonthBill,
                            item.PromotionCost,
                            item.ExcessCost,
                            item.InterCallingCharge,
                            item.AdditionalServiceFee,
                            item.TotalService,
                            item.VAT
                            );
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dataTable);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                        }
                    }

                }
            }
            else
            {
                TempData["DataNull"] = "ไม่พบข้อมูล โปรดตรวจสอบข้อมูลและลองอีกครั้งนะครับ";
            }

            return RedirectToAction(nameof(Index));

        }

        private DataTable GenerateReportPDF(ReportRespose models)
        {
            var data = new DataTable();
            data.Columns.Add("Phonenumber");
            data.Columns.Add("Name");
            data.Columns.Add("MonthBill");
            data.Columns.Add("PromotionCost");
            data.Columns.Add("YearBill");
            data.Columns.Add("ExcessCost");
            data.Columns.Add("AdditionalServiceFee");
            data.Columns.Add("InterCallingCharge");
            data.Columns.Add("TotalCost");
            data.Columns.Add("VAT");
            DataRow row;
            foreach (var item in models.ListDataReport)
            {
                row = data.NewRow();
                row["Phonenumber"] = item.Phonenumber;
                row["Name"] = item.Name;
                row["MonthBill"] = item.MonthBill;
                row["YearBill"] = item.YearBill;
                row["PromotionCost"] = item.PromotionCost;
                row["ExcessCost"] = item.ExcessCost;
                row["AdditionalServiceFee"] = item.AdditionalServiceFee;
                row["InterCallingCharge"] = item.InterCallingCharge;
                row["TotalCost"] = item.TotalService;
                row["VAT"] = item.VAT;
                data.Rows.Add(row);
            }
            return data;
        }


        public IActionResult SelectOptionReport()
        {
            //Select Option ListTelephone-Customer
            List<VMember> ListMember = _db.VMembers.Where(status => status.DeleteStatus != true).ToList();
            ViewBag.ListTelephone = new SelectList(ListMember, "Telephone", "Telephone");

            //Select Option ListMonth
            List<VMonth> ListMount = _db.VMonths.ToList();
            ViewBag.ListMonth = new SelectList(ListMount, "Month", "Month");

            //Select Option ListYear
            List<VBilllist> ListYear = _db.VBilllists.Where(s => s.DeleteStatus != true).GroupBy(s => s.YearBill).Select(s => new VBilllist { YearBill = s.Key }).ToList();
            ViewBag.ListYear = new SelectList(ListYear, "YearBill", "YearBill");

            return View();
        }


    }
}
