using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKS.Model
{
    public class ThongTinHoaDon
    {
        public HOADON HoaDon { get; set; }
        public CHITIET_HDLT CTHDLuuTru { get; set; }
        public ObservableCollection<CHITIET_HDAU> ListCTHDAnUong { get; set; }
        public ObservableCollection<CHITIET_HDGU> ListCTHDGiatUi { get; set; }
        public ObservableCollection<CHITIET_HDDC> ListCTHDDiChuyen { get; set; }

        public ThongTinHoaDon()
        {
            ListCTHDAnUong = new ObservableCollection<CHITIET_HDAU>();
            ListCTHDGiatUi = new ObservableCollection<CHITIET_HDGU>();
            ListCTHDDiChuyen = new ObservableCollection<CHITIET_HDDC>();
        }
    }
}
