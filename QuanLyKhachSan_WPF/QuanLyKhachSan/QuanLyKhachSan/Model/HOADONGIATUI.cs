//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKhachSan.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOADONGIATUI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADONGIATUI()
        {
            this.HOADONs = new HashSet<HOADON>();
            this.LUOTGIATUIs = new HashSet<LUOTGIATUI>();
        }
    
        public int MA_HDGU { get; set; }
        public Nullable<System.DateTime> THOIGIANLAP_HDGU { get; set; }
        public Nullable<decimal> TRIGIA_HDGU { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LUOTGIATUI> LUOTGIATUIs { get; set; }
    }
}