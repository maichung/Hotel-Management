using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    public class HoaDonLuuTruViewModel : BaseViewModel
    {
        private ObservableCollection<ThongTinPhong> _ListThongTinPhongChonThue;
        public ObservableCollection<ThongTinPhong> ListThongTinPhongChonThue { get => _ListThongTinPhongChonThue; set { _ListThongTinPhongChonThue = value; OnPropertyChanged(); } }
        private NHANVIEN _NhanVienLapHD;
        public NHANVIEN NhanVienLapHD { get => _NhanVienLapHD; set { _NhanVienLapHD = value; OnPropertyChanged(); } }
        private KHACHHANG _KhachHangThue;
        public KHACHHANG KhachHangThue { get => _KhachHangThue; set { _KhachHangThue = value; OnPropertyChanged(); } }

        private ObservableCollection<KHUYENMAI> _LisCTtKhuyenMai;
        public ObservableCollection<KHUYENMAI> ListCTKhuyenMai { get => _LisCTtKhuyenMai; set { _LisCTtKhuyenMai = value; OnPropertyChanged(); } }
        private KHUYENMAI _CTKhuyenMai;
        public KHUYENMAI CTKhuyenMai { get => _CTKhuyenMai; set { _CTKhuyenMai = value; OnPropertyChanged(); } }

        private DateTime _DateDatPhong;
        public DateTime DateDatPhong { get => _DateDatPhong; set { _DateDatPhong = value; OnPropertyChanged(); } }
        private DateTime _TimeDatPhong;
        public DateTime TimeDatPhong { get => _TimeDatPhong; set { _TimeDatPhong = value; OnPropertyChanged(); } }
        private DateTime _DateTraPhong;
        public DateTime DateTraPhong { get => _DateTraPhong; set { _DateTraPhong = value; OnPropertyChanged(); } }
        private DateTime _TimeTraPhong;
        public DateTime TimeTraPhong { get => _TimeTraPhong; set { _TimeTraPhong = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }
        public ICommand BookCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public HoaDonLuuTruViewModel()
        {
            KhachHangThue = new KHACHHANG();
            DateDatPhong = DateTime.Now;
            DateTraPhong = DateTime.Now;

            CancelCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => { p.Close(); });

            BookCommand = new RelayCommand<Window>((p) => 
            {
                if (DateDatPhong == null || TimeDatPhong == null || DateTraPhong == null || TimeTraPhong == null || p == null || p.DataContext == null)
                    return false;

                if (DateDatPhong > DateTraPhong)
                {
                    return false;
                }

                var hoadonVM = p.DataContext as HoaDonViewModel;
                foreach (ThongTinDatPhong item in hoadonVM.ListThongTinDatPhong)
                {                       
                    if(item.NgayDatPhong < DateDatPhong && DateDatPhong < item.NgayTraPhong)
                    {
                        return false;
                    }
                }

                if (hoadonVM.MaHD == 0 && (string.IsNullOrEmpty(hoadonVM.KhachHangThue.CMND_KH) || string.IsNullOrEmpty(hoadonVM.KhachHangThue.HOTEN_KH)))
                {
                    return false;
                }

                return true;
            }, (p) => 
            {
                var hoadonVM = p.DataContext as HoaDonViewModel;
                ListThongTinPhongChonThue = hoadonVM.ListThongTinPhongChonThue;
                NhanVienLapHD = hoadonVM.NhanVienLapHD;
                KhachHangThue = hoadonVM.KhachHangThue;

                DateTime ThoiGianDatPhong = new DateTime(DateDatPhong.Year, DateDatPhong.Month, DateDatPhong.Day,
                                                        TimeDatPhong.Hour, TimeDatPhong.Minute, TimeDatPhong.Second);
                DateTime ThoiGianTraPhong = new DateTime(DateTraPhong.Year, DateTraPhong.Month, DateTraPhong.Day,
                                                        TimeTraPhong.Hour, TimeTraPhong.Minute, TimeTraPhong.Second);
                
                var khachHang = DataProvider.Ins.model.KHACHHANG.Where(x => x.CMND_KH == KhachHangThue.CMND_KH).SingleOrDefault();
                if (khachHang == null)
                {
                    KHACHHANG newKhachHang = new KHACHHANG() { HOTEN_KH = KhachHangThue.HOTEN_KH, SODIENTHOAI_KH = KhachHangThue.SODIENTHOAI_KH, CMND_KH = KhachHangThue.CMND_KH, TEN_LOAIKH = "Khách vãng lai" };
                    DataProvider.Ins.model.KHACHHANG.Add(newKhachHang);
                    DataProvider.Ins.model.SaveChanges();
                    khachHang = DataProvider.Ins.model.KHACHHANG.Where(x => x.CMND_KH == KhachHangThue.CMND_KH).SingleOrDefault();
                }

                foreach (var item in ListThongTinPhongChonThue)
                {
                    DATPHONG datphong = new DATPHONG() { MA_PHONG = item.Phong.MA_PHONG, MA_NV = NhanVienLapHD.MA_NV, MA_KH = khachHang.MA_KH, NGAYBATDAU_DP = ThoiGianDatPhong, NGAYKETTHUC_DP = ThoiGianTraPhong };
                    DataProvider.Ins.model.DATPHONG.Add(datphong);
                    DataProvider.Ins.model.SaveChanges();
                }
                
                p.Close();
            });

            SaveCommand = new RelayCommand<Window>((p) => 
            {
                if (p == null || p.DataContext == null)
                    return false;

                var hoadonVM = p.DataContext as HoaDonViewModel;
                if (hoadonVM.DSMaPhongChonThue.Count() == 0 || hoadonVM.DSMaPhongChonThue == null)
                    return false;

                if (hoadonVM.MaHD == 0 && (string.IsNullOrEmpty(hoadonVM.KhachHangThue.CMND_KH) || string.IsNullOrEmpty(hoadonVM.KhachHangThue.HOTEN_KH)))
                {
                    return false;
                }

                if (hoadonVM.MaHD != 0)
                    return false;

                return true;
            }, (p) =>
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        //lấy thông tin phòng chọn thuê, nhân viên làm hóa đơn và thời gian làm hóa đơn
                        var hoadonVM = p.DataContext as HoaDonViewModel;
                        ListThongTinPhongChonThue = hoadonVM.ListThongTinPhongChonThue;
                        NhanVienLapHD = hoadonVM.NhanVienLapHD;
                        KhachHangThue = hoadonVM.KhachHangThue;
                        ListCTKhuyenMai = hoadonVM.ListCTKhuyenMai;
                        CTKhuyenMai = hoadonVM.CTKhuyenMai;
                        //DateTime ThoiGianLapHD = new DateTime(hoadonVM.DateLapHD.Year, hoadonVM.DateLapHD.Month, hoadonVM.DateLapHD.Day,
                        //                                      hoadonVM.TimeLapHD.Hour, hoadonVM.TimeLapHD.Minute, hoadonVM.TimeLapHD.Second);
                        //kiểm tra xem khách hàng đã có trong csdl của khách sạn hay chưa
                        var khachHang = DataProvider.Ins.model.KHACHHANG.Where(x => x.CMND_KH == KhachHangThue.CMND_KH).SingleOrDefault();
                        if (khachHang == null)
                        {
                            KHACHHANG newKhachHang = new KHACHHANG() { HOTEN_KH = KhachHangThue.HOTEN_KH, SODIENTHOAI_KH = KhachHangThue.SODIENTHOAI_KH, CMND_KH = KhachHangThue.CMND_KH, TEN_LOAIKH = "Khách vãng lai" };
                            DataProvider.Ins.model.KHACHHANG.Add(newKhachHang);
                            DataProvider.Ins.model.SaveChanges();
                            khachHang = DataProvider.Ins.model.KHACHHANG.Where(x => x.CMND_KH == KhachHangThue.CMND_KH).SingleOrDefault();
                        }
                        //lấy ra mức khuyến mãi cao nhất
                        foreach (KHUYENMAI item in ListCTKhuyenMai)
                        {
                            if (item.TEN_KM == khachHang.TEN_LOAIKH)
                            {
                                if (CTKhuyenMai == null)
                                {
                                    CTKhuyenMai = item;
                                }
                                else
                                {
                                    if (item.TILE_KM > CTKhuyenMai.TILE_KM)
                                    {
                                        CTKhuyenMai = item;
                                    }
                                }
                                break;
                            }
                        }

                        //Tạo hóa đơn tổng
                        var hd = new HOADON() { MA_KM = CTKhuyenMai.MA_KM, MA_NV = NhanVienLapHD.MA_NV, MA_KH = khachHang.MA_KH, THOIGIANLAP_HD = DateTime.Now, TINHTRANG_HD = false };
                        DataProvider.Ins.model.HOADON.Add(hd);
                        DataProvider.Ins.model.SaveChanges();
                        foreach (ThongTinPhong item in ListThongTinPhongChonThue)
                        {
                            //Tạo chi tiết hóa đơn lưu trú
                            var chitietHDLT = new CHITIET_HDLT() { MA_HD = hd.MA_HD, MA_PHONG = item.Phong.MA_PHONG, THOIGIANNHAN_PHONG = DateTime.Now };
                            DataProvider.Ins.model.CHITIET_HDLT.Add(chitietHDLT);
                            DataProvider.Ins.model.SaveChanges();
                            //Đổi trạng thái của phòng
                            var phong = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == item.Phong.MA_PHONG).SingleOrDefault();
                            phong.TINHTRANG_PHONG = "Đang thuê";
                            DataProvider.Ins.model.SaveChanges();
                        }                        

                        ts.Complete();
                        MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "\n\tLưu không thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }                

                p.Close();
            });
        }        
    }
}
