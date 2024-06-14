using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniBook.Models
{
    public class MatHangMua
    {
        BOOKSTOREEntities db = new BOOKSTOREEntities();
        public int IDSach { get; set; }
        public string TenSach { get; set; }
        public string AnhMinhHoa { get; set; }
        public double GiaBan { get; set; }
        public int SoLuong { get; set; }

        public double ThanhTien()
        {
            return SoLuong * GiaBan;
        }
        public MatHangMua(int IDSach)
        {
            this.IDSach = IDSach;
            var sach = db.SACHes.Single(s => s.IDSach == this.IDSach);
            this.TenSach = sach.TenSach;
            this.AnhMinhHoa = sach.AnhMinhHoa;
            this.GiaBan = double.Parse(sach.GiaBan.ToString());
            this.SoLuong = 1;
        }
    }
}