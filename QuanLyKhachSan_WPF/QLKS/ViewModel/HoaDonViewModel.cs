using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private int _MaPhong;
        public int MaPhong { get => _MaPhong; set { _MaPhong = value; OnPropertyChanged(); } }
        private KHACHHANG _KhachHangThue;
        public KHACHHANG KhachHangThue { get => _KhachHangThue; set { _KhachHangThue = value; OnPropertyChanged(); } }
        private NHANVIEN _NhanVienLapHD;
        public NHANVIEN NhanVienLapHD { get => _NhanVienLapHD; set { _NhanVienLapHD = value; OnPropertyChanged(); } }
        private long _TongTien;
        public long TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

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

        //Truyền thông tin qua hd giặt ủi
        private CHUYENDI _ChuyenDi;
        public CHUYENDI ChuyenDi { get => _ChuyenDi; set { _ChuyenDi = value; OnPropertyChanged(); } }

        public ICommand btnHDTongCommand { get; set; }
        public ICommand btnHDLuuTruCommand { get; set; }
        public ICommand btnHDAnUongCommand { get; set; }
        public ICommand btnHDGiatUiCommand { get; set; }
        public ICommand btnHDDiChuyenCommand { get; set; }

        public ICommand LoadKhachHangCommand { get; set; }

        public HoaDonViewModel()
        {
            ThoiGianLapHD = DateTime.Now;

            btnHDTongCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonTong);
            btnHDLuuTruCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonLuuTru);
            btnHDAnUongCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonAnUong);
            btnHDGiatUiCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonGiatUi);
            btnHDDiChuyenCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonDiChuyen);

            LoadKhachHangCommand = new RelayCommand<StackPanel>((p) => 
            {
                if (string.IsNullOrEmpty(KhachHangThue.CMND_KH))
                    return false;

                if (p == null || p.DataContext == null)
                    return false;

                return true;
            }, (p) =>
            {
                var kh = DataProvider.Ins.model.KHACHHANG.Where(x => x.CMND_KH == KhachHangThue.CMND_KH).SingleOrDefault();
                if (kh == null)
                {
                    KhachHangThue.HOTEN_KH = "";
                    KhachHangThue.SODIENTHOAI_KH = "";
                }
                else
                {
                    KhachHangThue.HOTEN_KH = kh.HOTEN_KH;
                    KhachHangThue.SODIENTHOAI_KH = kh.SODIENTHOAI_KH;
                }

                var hoadonluutruVM = p.DataContext as HoaDonLuuTruViewModel;
                hoadonluutruVM.KhachHangThue = KhachHangThue;
            });
        }
    }
}
