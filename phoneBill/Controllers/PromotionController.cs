using Microsoft.AspNetCore.Mvc;
using phoneBill.Data;
using phoneBill.Models;

namespace phoneBill.Controllers
{
    public class PromotionController : Controller
    {

        private readonly db_phonebillModel _db;
        public PromotionController(db_phonebillModel db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            List<VPromotion> List = _db.VPromotions.ToList();
            ViewBag.ListPromotion = List;
            //var model = new Promotion();
            //model.PStatus = false;
            return View();
        }

        public IActionResult Edit(int? ID)
        {

            if (ID == null || ID == 0)
            {
                return NotFound();
            }

            var obj = _db.Promotions.Find(ID);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // ------------------------------------------------------------ Create

        [HttpPost] // รับค่าเป็น post
        [ValidateAntiForgeryToken]
        public IActionResult Create(Promotion obj) // สร้างAction Create รับค่า  promotion obj
        {
            //var data = obj;
            //if (data.Checkbox == "true") {
            //    data.PStatus = true;
            //}
            //else
            //{
            //    data.PStatus = false;
            //}
            //data.PStatus = false;
            _db.Promotions.Add(obj); //เพิ่มข้อมูล obj เข้าตาราง Promotions
            _db.SaveChanges(); // บันทึกข้อมูล
            return RedirectToAction("Index"); //กลับไปหน้า index


        }

        // ------------------------------------------------------------- Update

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Promotion obj)
        {
            _db.Promotions.Update(obj);
            Boolean result = _db.SaveChanges() > 0;
            return RedirectToAction(nameof(Index));
        }

        // ------------------------------------------------------------ Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _db.Promotions.Find(id);
            data!.DeleteStatus = 1;
            _db.Promotions.Update(data);
            Boolean result = _db.SaveChanges() > 0;
            return RedirectToAction(nameof(Index));
        }

    }
}
