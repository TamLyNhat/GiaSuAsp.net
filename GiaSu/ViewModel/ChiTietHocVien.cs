using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiaSu.ViewModel
{
    public class ChiTietHocVien
    {
        public Nullable<int> MaLH { get; set; }
        public string TenKhoaHoc { get; set; }
        public string TenLopHoc { get; set; }
        public Nullable<int> Lau { get; set; }
        public Nullable<int> MaPhong { get; set; }
        public Nullable<Boolean> Buoi { get; set; }
        public Nullable<Boolean> Ngay { get; set; }
    }
}