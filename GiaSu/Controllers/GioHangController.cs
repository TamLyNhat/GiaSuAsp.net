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



            return View();
        }
    }
}