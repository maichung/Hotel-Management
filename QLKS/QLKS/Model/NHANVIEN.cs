//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLKS.Model
{
    using QLKS.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class NHANVIEN : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.HOADON = new HashSet<HOADON>();
        }

        private int _MA_NV;
        public int MA_NV { get => _MA_NV; set { _MA_NV = value; OnPropertyChanged(); } }
        private int _MA_TK;
        public int MA_TK { get => _MA_TK; set { _MA_TK = value; OnPropertyChanged(); } }
        private string _HOTEN_NV;
        public string HOTEN_NV { get => _HOTEN_NV; set { _HOTEN_NV = value; OnPropertyChanged(); } }
        private Nullable<bool> _GIOITINH_NV;
        public Nullable<bool> GIOITINH_NV { get => _GIOITINH_NV; set { _GIOITINH_NV = value; OnPropertyChanged(); } }
        private Nullable<System.DateTime> _NGAYSINH_NV;
        public Nullable<System.DateTime> NGAYSINH_NV { get => _NGAYSINH_NV; set { _NGAYSINH_NV = value; OnPropertyChanged(); } }
        private string _SODIENTHOAI_NV;
        public string SODIENTHOAI_NV { get => _SODIENTHOAI_NV; set { _SODIENTHOAI_NV = value; OnPropertyChanged(); } }
        private string _CHUCVU_NV;
        public string CHUCVU_NV { get => _CHUCVU_NV; set { _CHUCVU_NV = value; OnPropertyChanged(); } }
        private string _DIACHI_NV;
        public string DIACHI_NV { get => _DIACHI_NV; set { _DIACHI_NV = value; OnPropertyChanged(); } }
        private Nullable<System.DateTime> _NGAYVAOLAM_NV;
        public Nullable<System.DateTime> NGAYVAOLAM_NV { get => _NGAYVAOLAM_NV; set { _NGAYVAOLAM_NV = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADON { get; set; }
        public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}
