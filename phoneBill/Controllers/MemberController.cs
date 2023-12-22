using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using phoneBill.Data;
using phoneBill.Models;
using System;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;


namespace phoneBill.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly db_phonebillModel _db;

        public MemberController(db_phonebillModel db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //Data Member 
            List<VMember> Data = _db.VMembers.Where(s => s.DeleteStatus != true).OrderByDescending(s => s.ID).ToList();
            ViewBag.ListMember = Data;

            //Select Option Protion
            List<Promotion> List = _db.Promotions.ToList();
            ViewBag.ListPromotion = new SelectList(List, "ID", "Promotion1");

            return View();
        }

        public IActionResult Edit(int? ID)
        {

            //Select Option Protion
            List<Promotion> List = _db.Promotions.ToList();
            ViewBag.ListPromotion = new SelectList(List, "ID", "Promotion1");

            if (ID == null || ID == 0)
            {
                return NotFound();
            }

            var obj = _db.Members.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }

            List<VMember> Data = _db.VMembers.Where(s => s.ID == ID).ToList();
            foreach (var item in Data)
            {
                ViewBag.CampID = item.Camp;
            }


            return View(obj);
        }


        [HttpPost]
        public JsonResult CampResult(int ID)
        {
            Promotion result = _db.Promotions.FirstOrDefault(s => s.ID == ID)!;
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Member obj)
        {

            _db.Members.Add(obj);
            _db.SaveChanges();
            TempData["Success"] = "เพิ่มผู้ใช้งานโทรศัพท์เรียบร้อยแล้วครับ";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _db.Members.Find(id);
            data!.DeleteStatus = true;
            _db.Members.Update(data);
            Boolean result = _db.SaveChanges() > 0;
            TempData["Success"] = "ลบผู้ใช้งานโทรศัพท์เรียบร้อยแล้วครับ";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Member obj)
        {
            _db.Members.Update(obj);
            Boolean result = _db.SaveChanges() > 0;
            TempData["Success"] = "แก้ไขผู้ใช้งานโทรศัพท์เรียบร้อยแล้วครับ";
            return RedirectToAction(nameof(Index));
        }


    }
}
