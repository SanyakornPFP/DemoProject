using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using phoneBill.Data;
using phoneBill.Models;

namespace phoneBill.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private readonly db_phonebillModel _db;

        public BillsController(db_phonebillModel db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            DataSeleteOp();
            return View();
        }


        [HttpPost]
        public JsonResult CustomerDetailResult(String Telephone)
        {
            VMember result = _db.VMembers.FirstOrDefault(s => s.Telephone == Telephone)!;
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bill obj)
        {

            _db.Bills.Add(obj);
            _db.SaveChanges();
            TempData["Success"] = "เพิ่มบิลค่าบริการโทรศัพท์เรียบร้อยแล้วครับ";
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int? ID)
        {

            //Select Option ListTelephone-Customer
            List<VMember> List = _db.VMembers.Where(status => status.DeleteStatus != true).ToList();
            ViewBag.ListTelephone = new SelectList(List, "Telephone", "Telephone");

            if (ID == null || ID == 0)
            {
                return NotFound();
            }

            var obj = _db.Bills.Find(ID);

            //Select Option Mouth
            List<VMonthlist> Mount = _db.VMonthlists.ToList();
            ViewBag.ListMonth = new SelectList(Mount, "MonthID", "MonthName", obj.MonthID);

            if (obj == null)
            {
                return NotFound();
            }

            ViewBag.VAT = obj.VAT;
            ViewBag.MonthID = obj.MonthID;

            List<VMember> Data = _db.VMembers.Where(s => s.ID.ToString() == obj.MemberID).ToList();
            foreach (var item in Data) {
                ViewBag.Name = item.Name;
                ViewBag.Promotion = item.Promotion;
                ViewBag.Camp = item.Camp;
            }

            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Bill data = _db.Bills.Find(id);
            data.DeleteStatus = true;
            _db.Bills.Update(data);
            Boolean result = _db.SaveChanges() > 0;
            TempData["Success"] = "ลบบิลค่าบริการโทรศัพท์เรียบร้อยแล้วครับ";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Bill obj)
        {
            _db.Bills.Update(obj);
            Boolean result = _db.SaveChanges() > 0;
            TempData["Success"] = "แก้ไขบิลค่าบริการโทรศัพท์เรียบร้อยแล้วครับ";
            return RedirectToAction(nameof(Index));

        }

        public IActionResult DataSeleteOp()
        {
            //Data Bill
            List<VBilllist> Data = _db.VBilllists.Where(delete => delete.DeleteStatus != true).OrderByDescending(s => s.IDAUTO).ToList();
            ViewBag.ListBill = Data;

            //Select Option ListTelephone-Customer
            List<VMember> List = _db.VMembers.Where(status => status.DeleteStatus != true).ToList();
            ViewBag.ListTelephone = new SelectList(List, "Telephone", "Telephone");

            //Select Option Mouth
            List<VMonthlist> Mount = _db.VMonthlists.ToList();
            ViewBag.ListMonth = new SelectList(Mount, "MonthID", "MonthName");

            return View();
        }
       
    }
}
