//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GiaSu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRUNGTAM
    {
        public TRUNGTAM()
        {
            this.GIAOVIEN = new HashSet<GIAOVIEN>();
            this.KHOAHOC = new HashSet<KHOAHOC>();
        }
    
        public int MA_TT { get; set; }
        public string TEN_TT { get; set; }
        public string DIACHI { get; set; }
        public Nullable<int> SDT { get; set; }
        public string EMAIL { get; set; }
        public string HINH { get; set; }
    
        public virtual ICollection<GIAOVIEN> GIAOVIEN { get; set; }
        public virtual ICollection<KHOAHOC> KHOAHOC { get; set; }
    }
}
