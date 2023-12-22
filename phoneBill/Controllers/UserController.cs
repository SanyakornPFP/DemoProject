using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using phoneBill.Data;
using phoneBill.Helpers;
using phoneBill.Models;
using System.Drawing.Imaging;
using System.Drawing;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using Microsoft.AspNetCore.Authorization;

namespace phoneBill.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly db_phonebillModel _db;
        private readonly IWebHostEnvironment _webHost;
        public UserController(db_phonebillModel db, IWebHostEnvironment webHost)
        {
            _db = db;
            _webHost = webHost;
        }

        public IActionResult Index()
        {


            List<VUserAuth> List = _db.VUserAuths.Where(s => s.EMPID == User.GetLoggedInEmpID()).ToList();
            ViewBag.ListUserProfile = List;

            return View();
        }

        public IActionResult Adminstrator()
        {
            List<VUserAuth> List = _db.VUserAuths.ToList();
            ViewBag.ListUser = List;

            //Select Option EmpID
            List<VEmplist> EmpList = _db.VEmplists.Where(s => s.ENDDATE == null).ToList();
            ViewBag.ListEmpID = new SelectList(EmpList, "EMPID", "EMPID");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePass(RequestUser user)
        {
            var Data = _db.Users.FirstOrDefault(s => s.Username == User.GetLoggedInEmpID());
            Data!.Password = user.Password;
            Boolean result = _db.SaveChanges() > 0;
            TempData["Success"] = "เปลี่ยนรหัสผ่านเรียบร้อยครับ";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfile(RequestUser model)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Profile");


            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //get file extension
            FileInfo fileInfo = new FileInfo(model.File.FileName);

            string fileName = model.FileName + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            //RESIZE IMAGE START
            var image = Image.FromStream(model.File.OpenReadStream());
            int newWidht = 0, newHeight = 0;

            if (image.Height < 1000 || image.Width < 1000)
            {
                newWidht = image.Width;
                newHeight = image.Height;
            }
            else
            {
                if (image.Width > image.Height)
                {
                    double ratio = 1200.00 / image.Width;
                    newWidht = Convert.ToInt32(ratio * image.Width);
                    newHeight = Convert.ToInt32(ratio * image.Height);
                }
                else
                {
                    double ratio = 1200.00 / image.Height;
                    newWidht = Convert.ToInt32(ratio * image.Width);
                    newHeight = Convert.ToInt32(ratio * image.Height);
                }
            }

            var resized = new Bitmap(image, new Size(newWidht, newHeight));
            using var imageStream = new MemoryStream();
            resized.Save(imageStream, ImageFormat.Jpeg);
            //RESIZE IMAGE END

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                model.File.CopyTo(stream);
            }

            var Data = _db.Users.FirstOrDefault(s => s.Username == User.GetLoggedInEmpID());
            Data!.ImgProfile = fileName;
            Boolean reusult = _db.SaveChanges() > 0;

            TempData["Success"] = "อัปโหลดรูปโปรไฟล์เรียบร้อยครับ";

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public JsonResult EmpDetail(String EMPID)
        {
            VEmplist result = _db.VEmplists.FirstOrDefault(s => s.EMPID == EMPID);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RequestUser obj)
        {
            _db.Users.Add( new Models.User
            {
                Username = obj.Username,
                Password = obj.Password
            });

            _db.SaveChanges();
            TempData["Success"] = "เพิ่มผู้ใช้งานระบบเรียบร้อยแล้วครับ";
            return RedirectToAction(nameof(Adminstrator));

        }

        [HttpGet]
        public IActionResult Delete(int ID)
        {
            Models.User data = _db.Users.Find(ID);
            data.DeleteStatus = true;
            _db.Users.Update(data);
            Boolean result = _db.SaveChanges() > 0;
            return RedirectToAction(nameof(Adminstrator));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePassUser(RequestUser user)
        {
            var Data = _db.Users.FirstOrDefault(s => s.Username == user.Username);
            Data!.Password = user.Password;
            Boolean result = _db.SaveChanges() > 0;
            TempData["Success"] = "เปลี่ยนรหัสผ่านเรียบร้อยครับ";
            return RedirectToAction(nameof(Adminstrator));
        }

    }
}
