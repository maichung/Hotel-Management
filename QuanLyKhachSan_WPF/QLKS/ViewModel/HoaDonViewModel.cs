using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    public class HoaDonViewModel : BaseViewModel
    {
        public enum LoaiHoaDon
        {
            HoaDonTong, HoaDonLuuTru, HoaDonAnUong, HoaDonGiatUi, HoaDonDiChuyen
        };

        private int _LoaiHD;
        public int LoaiHD { get => _LoaiHD; set { _LoaiHD = value; OnPropertyChanged(); } }
        private DateTime _ThoiGianLapHD;
        public DateTime ThoiGianLapHD { get => _ThoiGianLapHD; set { _ThoiGianLapHD = value; OnPropertyChanged(); } }

        //Truyền thông tin qua những hd dịch vụ
        private int _MaHD;
        public int MaHD { get => _MaHD; set { _MaHD = value; OnPropertyChanged(); } }
        private int _MaPhong;
        public int MaPhong { get => _MaPhong; set { _MaPhong = value; OnPropertyChanged(); } }
        private KHACHHANG _KhachHangThue;
        public KHACHHANG KhachHangThue { get => _KhachHangThue; set { _KhachHangThue = value; OnPropertyChanged(); } }
        private NHANVIEN _NhanVienLapHD;
        public NHANVIEN NhanVienLapHD { get => _NhanVienLapHD; set { _NhanVienLapHD = value; OnPropertyChanged(); } }
        private long _TongTien;
        public long TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        private ThongTinHoaDon _ThongTinHD;
        public ThongTinHoaDon ThongTinHD { get => _ThongTinHD; set { _ThongTinHD = value; OnPropertyChanged(); } }

        //Truyền thông tin qua hd lưu trú
        private ThongTinPhong _ThongTinPhongChonThue;
        public ThongTinPhong ThongTinPhongChonThue { get => _ThongTinPhongChonThue; set { _ThongTinPhongChonThue = value; OnPropertyChanged(); } }

        //Truyền thông tin qua hd ăn uống
        private string _LoaiPhucVu;
        public string LoaiPhucVu { get => _LoaiPhucVu; set { _LoaiPhucVu = value; OnPropertyChanged(); } }
        private ObservableCollection<ThongTinOrder> _ListOrder;
        public ObservableCollection<ThongTinOrder> ListOrder { get => _ListOrder; set { _ListOrder = value; OnPropertyChanged(); } }

        //Truyền thông tin qua hd giặt ủi
        private ThongTinGiatUi _TTGiatUi;
        public ThongTinGiatUi TTGiatUi { get => _TTGiatUi; set { _TTGiatUi = value; OnPropertyChanged(); } }

        //Truyền thông tin qua hd di chuyển
        private CHUYENDI _ChuyenDi;
        public CHUYENDI ChuyenDi { get => _ChuyenDi; set { _ChuyenDi = value; OnPropertyChanged(); } }

        public ICommand btnHDTongCommand { get; set; }
        public ICommand btnHDLuuTruCommand { get; set; }
        public ICommand btnHDAnUongCommand { get; set; }
        public ICommand btnHDGiatUiCommand { get; set; }
        public ICommand btnHDDiChuyenCommand { get; set; }

        public ICommand LoadHoaDonTongCommand { get; set; }
        public ICommand PayCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public HoaDonViewModel()
        {
            ThoiGianLapHD = DateTime.Now;
            ThongTinHD = new ThongTinHoaDon();

            btnHDTongCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonTong);
            btnHDLuuTruCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonLuuTru);
            btnHDAnUongCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonAnUong);
            btnHDGiatUiCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonGiatUi);
            btnHDDiChuyenCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonDiChuyen);

            LoadHoaDonTongCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {                
                HOADON hoadon = new HOADON();
                var cthdlt = DataProvider.Ins.model.CHITIET_HDLT.Where(x => x.MA_PHONG == MaPhong).ToList();
                foreach (var item in cthdlt)
                {
                    var hd = DataProvider.Ins.model.HOADON.Where(x => x.MA_HD == item.MA_HD && x.TINHTRANG_HD == false).SingleOrDefault();
                    hoadon = hd;
                }
                ThongTinHD.HoaDon = hoadon;
                //lấy hóa đơn lưu trú
                var hdlt = DataProvider.Ins.model.CHITIET_HDLT.Where(x => x.MA_HD == hoadon.MA_HD).SingleOrDefault();
                ThongTinHD.CTHDLuuTru = hdlt;
                //lấy hóa đơn ăn uống
                var listHDAU = DataProvider.Ins.model.CHITIET_HDAU.Where(x => x.MA_HD == hoadon.MA_HD).ToList();
                foreach (CHITIET_HDAU item in listHDAU)
                {
                    ThongTinHD.ListCTHDAnUong.Add(item);
                }
                //lấy hóa đơn giặt ủi
                var listHDGU = DataProvider.Ins.model.CHITIET_HDGU.Where(x => x.MA_HD == hoadon.MA_HD).ToList();
                foreach (CHITIET_HDGU item in listHDGU)
                {                    
                    ThongTinHD.ListCTHDGiatUi.Add(item);
                }
                //lấy hóa đơn di chuyển
                var listHDDC = DataProvider.Ins.model.CHITIET_HDDC.Where(x => x.MA_HD == hoadon.MA_HD).ToList();
                foreach (CHITIET_HDDC item in listHDDC)
                {
                    ThongTinHD.ListCTHDDiChuyen.Add(item);
                }
            });

            PayCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {

            });

            CancelCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonDiChuyen);
        }
    }
}
