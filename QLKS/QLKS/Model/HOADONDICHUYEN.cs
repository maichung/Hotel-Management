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
    using System;
    using System.Collections.Generic;
    
    public partial class HOADONDICHUYEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADONDICHUYEN()
        {
            this.CHITIET_HDDC = new HashSet<CHITIET_HDDC>();
            this.HOADON = new HashSet<HOADON>();
        }
    
        public int MA_HDDC { get; set; }
        public Nullable<System.DateTime> THOIGIANLAP_HDDC { get; set; }
        public Nullable<decimal> TRIGIA_HDDC { get; set; }
        public Nullable<bool> TINHTRANG_HDDC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIET_HDDC> CHITIET_HDDC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADON { get; set; }
    }
}
