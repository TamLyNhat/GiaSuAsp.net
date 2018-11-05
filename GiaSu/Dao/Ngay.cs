using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiaSu.Models;

namespace GiaSu.Dao
{
    public class Ngay
    {
        public List<BooleanLopHoc> sm()
        {
            List<BooleanLopHoc> BuoiList = new List<BooleanLopHoc>();
            BooleanLopHoc a;

            a = new BooleanLopHoc();
            a.Number = 1;
            a.Key = "Sáng";
            BuoiList.Add(a);

            a = new BooleanLopHoc();
            a.Number = 0;
            a.Key = "Chiều";
            BuoiList.Add(a);

            return BuoiList;
        }
    }
}