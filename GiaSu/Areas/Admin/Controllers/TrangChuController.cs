using GiaSu.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiaSu.Areas.Admin.Controllers
{
    public class TrangChuController : Controller
    {
        GiaSuEntities db = new GiaSuEntities();

        // GET: Admin/TrangChu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult XacNhanDangKy()
        {
            //lọc ra danh sách từ ngày đăng ký dến DateTime.Now <= 7
            var list = db.CHITIETDANGKY.Where( d => d.NGAY_DK >= EntityFunctions.AddDays(DateTime.Now,-7) && d.XACNHAN == false);

            return View(list);
        }

        public ActionResult EditXacNhanDangKy(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            //gán xác nhận là đã đóng tiền
            CHITIETDANGKY chitiet = db.CHITIETDANGKY.Find(id);
            chitiet.XACNHAN = true;

            db.SaveChanges();

            return RedirectToAction("XacNhanDangKy");
        }

        public ActionResult HetThoiGianDangKy()
        {
            //lọc ra danh sách từ ngày đăng ký dến DateTime.Now > 7
            var list = db.CHITIETDANGKY.Where(d => d.NGAY_DK < EntityFunctions.AddDays(DateTime.Now, -7) && d.XACNHAN == false);

            return View(list);
        }

        //Xóa học viên chưa đóng tiền trên 7 ngày
        public ActionResult XoaHocVienHetThoiGianDK(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            //gán xác nhận là đã đóng tiền
            CHITIETDANGKY chitiet = db.CHITIETDANGKY.Find(id);
            db.CHITIETDANGKY.Remove(chitiet);

            db.SaveChanges();

            return RedirectToAction("HetThoiGianDangKy");
        }
    }
}