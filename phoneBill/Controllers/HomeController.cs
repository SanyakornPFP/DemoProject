using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using phoneBill.Data;
using phoneBill.Models;
using System;
using System.Dynamic;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Newtonsoft.Json.Linq;
using Microsoft.DotNet.Scaffolding.Shared;

namespace phoneBill.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly db_phonebillModel _db;

        public HomeController(db_phonebillModel db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            String NowYear = DateTime.Now.ToString("yyyy");
            //String NowYear = "2020";

            CustomerCount();
            CampCount();
            SumServiceMonthNowYear(NowYear);
            SumServiceCost(NowYear);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult CustomerCount()
        {

            var dashboard = new DashboardResponse();
            dashboard.ListCountMember = new List<MemberResponse>();
            dashboard.ListCountMember.Add(new MemberResponse { CountMember = _db.VMembers.Count(s => s.DeleteStatus != true), CountMemberD = _db.VMembers.Count(d => d.DeleteStatus == true) });
            ViewBag.CountMember = dashboard;

            return View();
        }

        public IActionResult CampCount()
        {

            //DATA DashBoard
            //Sum Total Customer And PriceBeforeMonth
            int nowMonth = int.Parse(DateTime.Now.ToString("MM")) - 1;
            String TextBeforeMonth = "M" + nowMonth.ToString();

            int nowYearPQ = int.Parse(DateTime.Now.ToString("yyyy")) + 543;
            ViewBag.NowYear = nowYearPQ.ToString();

            String[] NameCamp = new String[] { "DTAC", "True", "AIS" };
            String[] ImageCamp = new String[] { "ImageDtac.png", "ImageTrue.png", "ImageAis.png" };
            var Camp = new DashboardResponse();
            Camp.ListCamp = new List<CampResponse>();

            for (int i = 0; i < NameCamp.Length; i++)
            {
                Camp.ListCamp.Add(new CampResponse
                {
                    Name = NameCamp[i],
                    CountCamp = _db.VMembers.Count(d => d.Camp == NameCamp[i]),
                    ImageCamp = ImageCamp[i],
                    SumServiceCamp = (int)_db.VBilllists.Where(d => d.Camp == NameCamp[i] && d.MonthID == TextBeforeMonth).Sum(s => s.PromotionCost + s.ExcessCost + s.InterCallingCharge + s.AdditionalServiceFee + s.VAT)
                });
            }

            ViewBag.SumCamp = Camp;


            return View();
        }
        public IActionResult SumServiceMonthNowYear(String Year)
        {
            String[] MonID = new String[] { "M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8", "M9", "M10", "M11", "M12" };
            //String[] MonName = new String[] { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
            var SumServiceMonth = new DashboardResponse();
            SumServiceMonth.ListService = new List<ServiceMonthReponse>();

            for(int i = 0; i <= 11; i++)
            {
                int SumtotalMonth = (int) _db.VBilllists.Where(d => d.MonthID == MonID[i] && d.YearBill == Year).Sum(s => s.PromotionCost + s.ExcessCost + s.InterCallingCharge + s.AdditionalServiceFee + s.VAT);
                SumServiceMonth.ListService.Add(new ServiceMonthReponse
                {
                    SumMonth = SumtotalMonth
                });
            }

            ViewBag.SumServiceOfMonth = SumServiceMonth;


            return View();
        }


        public IActionResult SumServiceCost(String Year)
        {

            var SumCost = new DashboardResponse();
            SumCost.ListSumCost = new List<SumServiceCostReponse>();

            SumCost.ListSumCost.Add(new SumServiceCostReponse
            {
                SumCostPromotion = (int) _db.VBilllists.Where(d => d.YearBill == Year).Sum(s => s.PromotionCost),
                SumCostExcess = (int)_db.VBilllists.Where(d => d.YearBill == Year).Sum(s => s.ExcessCost),
                SumCostInterCalling = (int)_db.VBilllists.Where(d => d.YearBill == Year).Sum(s => s.InterCallingCharge),
                SumCostAdditinal = (int)_db.VBilllists.Where(d => d.YearBill == Year).Sum(s => s.AdditionalServiceFee),

            });

            ViewBag.SumCostAll = SumCost;

            return View();
        }

    }
}
