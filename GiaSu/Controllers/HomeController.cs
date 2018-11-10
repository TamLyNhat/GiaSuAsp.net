using GiaSu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GiaSu.Controllers
{
    public class HomeController : Controller
    {
        GiaSuEntities db = new GiaSuEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult KhoaHoc()
        {
            var listKH = db.KHOAHOC.Take(4).ToList();

            return PartialView(listKH);
        }

        public PartialViewResult GiaoVien()
        {
            var listGV = db.GIAOVIEN.Take(4).ToList();

            return PartialView(listGV);
        }

        public PartialViewResult MoTaGiaoVien()
        {
            var listGV = db.GIAOVIEN.Take(3).ToList();

            return PartialView(listGV);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public ActionResult DangKy(HOCVIEN hocvien)
        {
            //gán mặc nhiên Ma_Kh khi tạo để tránh bị lỗi not null
            hocvien.MA_KH = 1;

            try
            {
                if (ModelState.IsValid)
                {
                    db.HOCVIEN.Add(hocvien);
                    db.SaveChanges();
                    return View("Index");
                }
            }
            catch(Exception e)
            {
                throw;
            }
            
            ViewBag.ErrorDangKy = 1;

            return View("Index");
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string ten = f["TEN_HV"].ToString();
            string password = f["MATKHAUHV"].ToString();

            // Chọn ra học viên có tên và mật khẩu trùng khi nhập form Dang Nhap
            HOCVIEN hocvien = db.HOCVIEN.SingleOrDefault(n => n.TEN_HV == ten && n.MATKHAUHV == password);

            try
            {
                //nếu đăng nhập đúng mk và pass
                if (hocvien != null)
                {
                    //gán session Tài khoản
                    Session["TaiKhoan"] = hocvien as HOCVIEN;

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                throw;
            }
         
            ViewBag.ErrorDangNhap = 1;
            //gán viewbag bằng text khi đăng nhập sai
            ViewBag.ErrorText = "Tên đăng nhập hoặc mật khẩu sai!! Vui lòng kiểm tra lại.";

            return View("Index");
        }

        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            Session["GioHang"] = null;
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}