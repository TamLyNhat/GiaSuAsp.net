using GiaSu.Models;
using GiaSu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiaSu.Dao
{
    public class ListChiTietHocVien
    {
        GiaSuEntities db = new GiaSuEntities();

        public List<ChiTietHocVien> Details()
        {
            List<ChiTietHocVien> detail = new List<ChiTietHocVien>();

            var e = (from a in db.KHOAHOC
                     join b in db.LOPHOC on a.MA_KH equals b.MA_KH
                     join c in db.THONGTINLOPHOC on b.MA_LH equals c.MA_LH
                     join d in db.PHONG on c.MA_PHONG equals d.MA_PHONG
                     select new ChiTietHocVien
                     {
                         MaLH = b.MA_LH,
                         TenKhoaHoc = a.TEN_KH,
                         MaPhong = c.MA_PHONG,
                         TenLopHoc = b.TEN_LH,
                         Lau = d.LAU,
                         Buoi = b.BUOI,
                         Ngay = b.NGAY

                     }).ToList();

            detail = e;

            return detail;
        }
    }
}