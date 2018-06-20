using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKS.Model
{
    public class ThongTinDatPhong
    {
        public int MaPhong { get; set; }
        public string TenNV { get; set; }
        public string TenKH { get; set; }
        public DateTime NgayDatPhong { get; set; }
        public DateTime NgayTraPhong { get; set; }
    }
}
