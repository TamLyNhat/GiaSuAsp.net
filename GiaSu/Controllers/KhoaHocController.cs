using GiaSu.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiaSu.Controllers
{
    public class KhoaHocController : Controller
    {
        // GET: KhoaHoc
        GiaSuEntities db = new GiaSuEntities();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult DropdownKhoaHoc()
        {
            var list = db.KHOAHOC.ToList().Take(4);

            return PartialView(list);
        }

        public ActionResult ChiTietKhoaHoc(int? id)
        {
            if (id == null)
                return HttpNotFound();

            KHOAHOC khoahoc = db.KHOAHOC.FirstOrDefault(s => s.MA_KH == id);

            if (khoahoc == null)
                return HttpNotFound();

            return View(khoahoc);
        }

        public ActionResult TatCaKhoaHoc(int? page)
        {
            //Thực hiện chưc năng phân trang
            //Tạo biến số sản phẩm trên trang
            int PageSize = 2;
            //Tạo biến số trang hiện tại
            //Nếu page không có giá trị thì sẽ gán cho page bằng 1
            int PageNumber = (page ?? 1);

            return View(db.KHOAHOC.OrderBy(n => n.MA_KH).ToPagedList(PageNumber, PageSize));
        }

        [HttpPost]
        public JsonResult get(int idLopHoc)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                List<LOPHOC> list = db.LOPHOC.Where(x => x.MA_LH == idLopHoc).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { err = e.ToString() });
            }

        }

    
    }
}