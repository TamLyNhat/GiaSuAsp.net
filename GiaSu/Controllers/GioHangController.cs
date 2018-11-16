using GiaSu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiaSu.Controllers
{
    public class GioHangController : Controller
    {
        GiaSuEntities db = new GiaSuEntities();

        public List<ItemGioHang> getGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<ItemGioHang> listGioHang = Session["GioHang"] as List<ItemGioHang>;

            if (listGioHang == null)
            {
                //Nếu session giỏ hàng chưa tồn tại thì khởi tạo giỏ hàng
                listGioHang = new List<ItemGioHang>();
                Session["GioHang"] = listGioHang;
            }

            return listGioHang;
        }

        //Them Gio Hang thông thường bằng load lại trang
        public ActionResult ThemGioHang(int MaLH, string strUrl, FormCollection f)
        {
            MaLH = int.Parse(f["LopHoc"].ToString());
        
            //Kiểm tra sán phẩm có trong csdl hay không
            LOPHOC lh = db.LOPHOC.SingleOrDefault(n => n.MA_LH == MaLH);

            if (lh == null)
            {
                //trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            //Lấy giỏ hàng
            List<ItemGioHang> listGioHang = getGioHang();
            //Trường hợp 1 nếu sản phẩm đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = listGioHang.SingleOrDefault(n => n.Ma_Lop_Hoc == MaLH);

            if (spCheck != null)
            {
                spCheck.SL++;
                spCheck.Tong_Tien = spCheck.SL * spCheck.Hoc_Phi;
                return Redirect(strUrl);
            }

            ItemGioHang itemGH = new ItemGioHang(MaLH);
            listGioHang.Add(itemGH);

            return Redirect(strUrl);
        }

        public double TinhTongSl()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> listGioHang = Session["GioHang"] as List<ItemGioHang>;

            if (listGioHang == null)
                return 0;

            return listGioHang.Sum(n => n.SL);
        }

        //Tính tổng tiền
        public decimal TinhTongTien()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> listGioHang = Session["GioHang"] as List<ItemGioHang>;

            if (listGioHang == null)
                return 0;

            return listGioHang.Sum(n => (decimal)n.Tong_Tien);
        }

        // GET: GioHang
        public ActionResult XemGioHang()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> listGioHang = getGioHang();

            return View(listGioHang);
        }

        public ActionResult GioHangPartial()
        {
            if (TinhTongSl() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }

            ViewBag.TongSoLuong = TinhTongSl();
            ViewBag.TongTien = TinhTongTien();

            return PartialView();
        }

        public ActionResult XoaGioHang(int Ma_lh)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Kiểm tra sán phẩm có trong csdl hay không
            LOPHOC lh = db.LOPHOC.SingleOrDefault(n => n.MA_LH == Ma_lh);

            if (lh == null)
            {
                //trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            //Lấy giỏ hàng
            List<ItemGioHang> listGioHang = getGioHang();
            //Trường hợp 1 nếu sản phẩm đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = listGioHang.SingleOrDefault(n => n.Ma_Lop_Hoc == Ma_lh)   ;

            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Xóa item trong giỏ hàng
            listGioHang.Remove(spCheck);

            return RedirectToAction("XemGioHang");
        }

        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Thêm chi tiết đơn đặt hàng
            List<ItemGioHang> listGH = getGioHang();

            HOCVIEN hocvien = (HOCVIEN)Session["TaiKhoan"];
            CHITIETDANGKY ctdk;
            foreach (var item in listGH)
            {
                ctdk = new CHITIETDANGKY();
                ctdk.MA_HV = hocvien.MA_HV;
                ctdk.NGAY_DK = DateTime.Now;
                ctdk.XACNHAN = false;

                ctdk.MA_LH = item.Ma_Lop_Hoc;
                ctdk.SO_LUONG = item.SL;
                ctdk.TONG_TIEN = item.Tong_Tien;

                db.CHITIETDANGKY.Add(ctdk);
            }

            db.SaveChanges();
            Session["GioHang"] = null;

            return RedirectToAction("XemGioHang");
        }

        //Them Gio Hang thông thường bằng Ajax
        public ActionResult ThemGioHangAjax(string strUrl, FormCollection f)
        {
            int MaLH = int.Parse(f["LopHoc"].ToString());

            //Kiểm tra sán phẩm có trong csdl hay không
            LOPHOC lh = db.LOPHOC.SingleOrDefault(n => n.MA_LH == MaLH);

            if (lh == null)
            {
                //trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            //Lấy giỏ hàng
            List<ItemGioHang> listGioHang = getGioHang();
            //Trường hợp 1 nếu sản phẩm đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = listGioHang.SingleOrDefault(n => n.Ma_Lop_Hoc == MaLH);

            if (spCheck != null)
            {
                spCheck.SL++;
                spCheck.Tong_Tien = spCheck.SL * spCheck.Hoc_Phi;

                ViewBag.TongSoLuong = TinhTongSl();
                ViewBag.TongTien = TinhTongTien();

                return PartialView("GioHangPartial");
            }

            ItemGioHang itemGH = new ItemGioHang(MaLH);
            listGioHang.Add(itemGH);

            ViewBag.TongSoLuong = TinhTongSl();
            ViewBag.TongTien = TinhTongTien();

            return PartialView("GioHangPartial");
        }
    }
}