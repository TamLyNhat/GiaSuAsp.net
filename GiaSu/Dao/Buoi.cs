using GiaSu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiaSu.Dao
{
    public class Buoi
    {
        public List<BooleanLopHoc> sm()
        {
            List<BooleanLopHoc> NgayList = new List<BooleanLopHoc>();
            BooleanLopHoc a;

            a = new BooleanLopHoc();
            a.Number = 1;
            a.Key = "246";
            NgayList.Add(a);

            a = new BooleanLopHoc();
            a.Number = 0;
            a.Key = "357";
            NgayList.Add(a);

            return NgayList;
        }
    }
}