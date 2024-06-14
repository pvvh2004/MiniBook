//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiniBook.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SACH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SACH()
        {
            this.CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
            this.DANHGIAs = new HashSet<DANHGIA>();
            this.VAITROes = new HashSet<VAITRO>();
        }
    
        public int IDSach { get; set; }
        public string TenSach { get; set; }
        public Nullable<double> GiaBan { get; set; }
        public Nullable<int> SoLuongKho { get; set; }
        public string MoTa { get; set; }
        public string AnhMinhHoa { get; set; }
        public Nullable<System.DateTime> NgayPhatHanh { get; set; }
        public Nullable<int> SLXem { get; set; }
        public Nullable<int> SLBan { get; set; }
        public Nullable<int> IDNXB { get; set; }
        public Nullable<int> IDTheLoai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA> DANHGIAs { get; set; }
        public virtual NXB NXB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VAITRO> VAITROes { get; set; }
        public virtual THELOAI THELOAI { get; set; }
    }
}
