using GiaSu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiaSu.ViewModel
{
    public class ItemGioHang
    {
        public ItemGioHang(int ma_lh, int sl)
        {
            using (GiaSuEntities db = new GiaSuEntities())
            {
                this.Ma_Lop_Hoc = ma_lh;
                
                LOPHOC lh = db.LOPHOC.Single(n => n.MA_LH == ma_lh);

                this.Ten_Lop_Hoc = lh.TEN_LH;
                this.Si_So = lh.SISO;
                this.Buoi = lh.BUOI;
                this.Ngay = lh.NGAY;

                this.Ten_Khoa_Hoc = lh.KHOAHOC.TEN_KH;
                this.Thoi_Gian_Hoc = lh.KHOAHOC.THOIGIANHOC;
                this.Noi_Dung = lh.KHOAHOC.NOIDUNG;
                this.Hoc_Phi = lh.KHOAHOC.HOCPHI;
                this.SL = sl;
                this.Tong_Tien = sl * Hoc_Phi;
            }
        }

        public ItemGioHang(int ma_lh)
        {
            using (GiaSuEntities db = new GiaSuEntities())
            {
                this.Ma_Lop_Hoc = ma_lh;

                LOPHOC lh = db.LOPHOC.Single(n => n.MA_LH == ma_lh);

                this.Ten_Lop_Hoc = lh.TEN_LH;
                this.Si_So = lh.SISO;
                this.Buoi = lh.BUOI;
                this.Ngay = lh.NGAY;

                this.Ten_Khoa_Hoc = lh.KHOAHOC.TEN_KH;
                this.Thoi_Gian_Hoc = lh.KHOAHOC.THOIGIANHOC;
                this.Noi_Dung = lh.KHOAHOC.NOIDUNG;
                this.Hoc_Phi = lh.KHOAHOC.HOCPHI;
                this.SL = 1;
                this.Tong_Tien = Hoc_Phi;
            }
        }

        public string Ten_Khoa_Hoc { get; set; }
        public Nullable<int> Thoi_Gian_Hoc { get; set; }
        public Nullable<decimal> Hoc_Phi { get; set; }
        public int SL { get; set; }
        public Nullable<decimal> Tong_Tien { get; set; }
        public string Noi_Dung { get; set; }
        public int Ma_Lop_Hoc { get; set; }
        public string Ten_Lop_Hoc { get; set; }
        public Nullable<int> Si_So { get; set; }
        public Nullable<bool> Buoi { get; set; }
        public Nullable<bool> Ngay { get; set; }
    }
}