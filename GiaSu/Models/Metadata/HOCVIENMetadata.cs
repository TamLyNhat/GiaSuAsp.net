using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiaSu.Models
{
    [MetadataTypeAttribute(typeof(HOCVIENMetadata))]
    public partial class HOCVIEN
    {
        internal sealed class HOCVIENMetadata
        {
            public int MA_HV { get; set; }
            [Required(ErrorMessage = "Bạn cần nhập tên học viên")]
            public string TEN_HV { get; set; }
            [Required(ErrorMessage = "Bạn cần chọn ngày sinh")]
            [DisplayName("Date of Birth")]
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> NGAYSINH { get; set; }
            [Required(ErrorMessage = "Bạn cần nhập Địa chỉ")]
            public string DIACHI { get; set; }
            [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
            public Nullable<int> SDT { get; set; }
            [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
            public string MATKHAUHV { get; set; }

        }
    }
}