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
    
    public partial class THONGTINLOPHOC
    {
        public int MA_LH { get; set; }
        public int MA_PHONG { get; set; }
    
        public virtual LOPHOC LOPHOC { get; set; }
        public virtual PHONG PHONG { get; set; }
    }
}
