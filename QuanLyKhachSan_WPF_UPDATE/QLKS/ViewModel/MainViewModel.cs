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
using System.Windows.Media;

namespace QLKS.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public enum ChucNangKhachSan
        {
            TrangChu, DichVuAnUong, DichVuGiatUi, DichVuDiChuyen, TraCuu, BaoCao
        };
        private int _ChucNangKS;
        public int ChucNangKS { get => _ChucNangKS; set { _ChucNangKS = value; OnPropertyChanged(); } }

        private ObservableCollection<ThongTinPhong> _ListTTPhong;
        public ObservableCollection<ThongTinPhong> ListTTPhong { get => _ListTTPhong; set { _ListTTPhong = value; OnPropertyChanged(); } }
        private int _TongSoPhong;
        public int TongSoPhong { get => _TongSoPhong; set { _TongSoPhong = value; OnPropertyChanged(); } }
        private int _SoPhongTrong;
        public int SoPhongTrong { get => _SoPhongTrong; set { _SoPhongTrong = value; OnPropertyChanged(); } }
        private int _SoPhongDangThue;
        public int SoPhongDangThue { get => _SoPhongDangThue; set { _SoPhongDangThue = value; OnPropertyChanged(); } }
        private string _MaPhongChonThue;
        public string MaPhongChonThue { get => _MaPhongChonThue; set { _MaPhongChonThue = value; OnPropertyChanged(); } }

        private NHANVIEN _NhanVien;
        public NHANVIEN NhanVien { get => _NhanVien; set { _NhanVien = value; OnPropertyChanged(); } }
        private KHACHHANG _KhachHangThue;
        public KHACHHANG KhachHangThue { get => _KhachHangThue; set { _KhachHangThue = value; OnPropertyChanged(); } }

        private ObservableCollection<KHUYENMAI> _LisCTtKhuyenMai;
        public ObservableCollection<KHUYENMAI> ListCTKhuyenMai { get => _LisCTtKhuyenMai; set { _LisCTtKhuyenMai = value; OnPropertyChanged(); } }
        private KHUYENMAI _CTKhuyenMai;
        public KHUYENMAI CTKhuyenMai { get => _CTKhuyenMai; set { _CTKhuyenMai = value; OnPropertyChanged(); } }
        private string _ThongBaoKM;
        public string ThongBaoKM { get => _ThongBaoKM; set { _ThongBaoKM = value; OnPropertyChanged(); } }
        private ObservableCollection<int> _DSMaPhongChonThue;
        public ObservableCollection<int> DSMaPhongChonThue { get => _DSMaPhongChonThue; set { _DSMaPhongChonThue = value; OnPropertyChanged(); } }

        private ObservableCollection<ThongTinDatPhong> _ListThongTinDatPhong;
        public ObservableCollection<ThongTinDatPhong> ListThongTinDatPhong { get => _ListThongTinDatPhong; set { _ListThongTinDatPhong = value; OnPropertyChanged(); } }

        public ICommand btnTrangChuCommand { get; set; }
        public ICommand btnDVAnUongCommand { get; set; }
        public ICommand btnDVGiatUiCommand { get; set; }
        public ICommand btnDVDiChuyenCommand { get; set; }
        public ICommand btnTraCuuCommand { get; set; }
        public ICommand btnBaoCaoCommand { get; set; }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadTatCaPhongCommand { get; set; }
        public ICommand LoadPhongTrongCommand { get; set; }
        public ICommand LoadPhongDangThueCommand { get; set; }

        public ICommand DangXuatCommand { get; set; }

        public ICommand ChonPhongCommand { get; set; }
        public ICommand ThuePhongCommand { get; set; }
        public ICommand ShowHDTongCommand { get; set; }
        public ICommand RefreshCommand { get; set; }        

        public MainViewModel()
        {
            ListCTKhuyenMai = new ObservableCollection<KHUYENMAI>(DataProvider.Ins.model.KHUYENMAI);
            MaPhongChonThue = "";

            #region Xử lý ản hiện view
            btnTrangChuCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { ChucNangKS = (int)ChucNangKhachSan.TrangChu; DSMaPhongChonThue.Clear(); MaPhongChonThue = ""; });
            btnDVAnUongCommand = new RelayCommand<Grid>((p) => 
            {
                if(p == null || p.DataContext == null)
                    return false;

                return true;
            }, (p) =>
            {
                ChucNangKS = (int)ChucNangKhachSan.DichVuAnUong;
                var mathangVM = p.DataContext as MatHangViewModel;
                mathangVM.GetTTPhongDangThue();
                DSMaPhongChonThue.Clear();
                MaPhongChonThue = "";
            });            
            btnDVGiatUiCommand = new RelayCommand<Grid>((p) => 
            {
                if (p == null || p.DataContext == null)
                    return false;

                return true;
            }, (p) =>
            {
                ChucNangKS = (int)ChucNangKhachSan.DichVuGiatUi;
                var giatuiVM = p.DataContext as LuotGiatUiViewModel;
                giatuiVM.GetTTPhongDangThue();
                DSMaPhongChonThue.Clear();
                MaPhongChonThue = "";
            });
            btnDVDiChuyenCommand = new RelayCommand<Grid>((p) => 
            {
                if (p == null || p.DataContext == null)
                    return false;

                return true;
            }, (p) =>
            {
                ChucNangKS = (int)ChucNangKhachSan.DichVuDiChuyen;
                var dichuyenVM = p.DataContext as DiChuyenViewModel;
                dichuyenVM.GetTTPhongDangThue();
                DSMaPhongChonThue.Clear();
                MaPhongChonThue = "";
            });
            btnTraCuuCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { ChucNangKS = (int)ChucNangKhachSan.TraCuu; DSMaPhongChonThue.Clear(); MaPhongChonThue = ""; });
            btnBaoCaoCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { ChucNangKS = (int)ChucNangKhachSan.BaoCao; DSMaPhongChonThue.Clear(); MaPhongChonThue = ""; });
            #endregion

            #region Xử lý Load trang chủ
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
              {
                  p.Show();
                  LoadTTPhong();
                  RefreshTTP();

                  TongSoPhong = ListTTPhong.Count();
                  SoPhongTrong = ListTTPhong.Where(x => x.Phong.TINHTRANG_PHONG == "Trống").Count();
                  SoPhongDangThue = ListTTPhong.Where(x => x.Phong.TINHTRANG_PHONG == "Đang thuê").Count();
                  //Xử lý lấy khuyến mãi cao nhất
                  ObservableCollection<KHUYENMAI> listKMCungNgay = new ObservableCollection<KHUYENMAI>();
                  foreach (KHUYENMAI item in ListCTKhuyenMai)
                  {
                      
                      if (item.NGAYBATDAU_KM != null && item.NGAYKETTHUC_KM != null)
                      {
                          DateTime end = (DateTime)item.NGAYKETTHUC_KM;
                          if (item.NGAYBATDAU_KM < DateTime.Now && end.AddDays(1) > DateTime.Now)
                          {
                              listKMCungNgay.Add(item);
                          }
                      }
                  }

                  CTKhuyenMai = GetKM_TiLeMax(listKMCungNgay);
                  if (CTKhuyenMai == null)
                  {
                      ThongBaoKM = "Không có chương trình khuyến mãi tại thời điểm hiện tại!";
                  }
                  else
                  {
                      ThongBaoKM = CTKhuyenMai.TEN_KM + " - Khách sạn khuyến mãi " + CTKhuyenMai.TILE_KM + "% cho tổng trị giá của hóa đơn!";
                  }
              });

            DangXuatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
              {
                  MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn đăng xuất khỏi hệ thống không?", "Đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
                  if (result == MessageBoxResult.Yes)
                  {
                      p.Hide();
                      DangNhap dangnhap = new DangNhap();
                      dangnhap.ShowDialog();
                      p.Close();
                  }
              });

            LoadTatCaPhongCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ListTTPhong = LoadTTPhong();
            });

            LoadPhongTrongCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ListTTPhong = LoadTTPhong("Trống");
            });

            LoadPhongDangThueCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ListTTPhong = LoadTTPhong("Đang thuê");
            });
            #endregion

            DSMaPhongChonThue = new ObservableCollection<int>();

            ChonPhongCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) =>
            {                            
                if (DSMaPhongChonThue.Count != 0)
                {
                    foreach (int item in DSMaPhongChonThue)
                    {
                        if (p.Tag.ToString() == item.ToString())
                            return;                            
                    }
                }
                MaPhongChonThue += p.Tag.ToString() + " ";
                DSMaPhongChonThue.Add(Int32.Parse(p.Tag.ToString()));
            });

            ThuePhongCommand = new RelayCommand<Button>((p) => 
            {
                if (DSMaPhongChonThue == null || DSMaPhongChonThue.Count() == 0)
                {
                    MessageBox.Show("Vui lòng chọn phòng cần thuê!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                foreach (int item in DSMaPhongChonThue)
                {
                    var phong = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == item).SingleOrDefault();
                    if (phong == null || phong.TINHTRANG_PHONG == "Đang thuê")
                    {
                        MessageBox.Show("Phòng đang được thuê, vui lòng chọn phòng khác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }                

                return true;
            }, (p) =>
            {                              
                HoaDon hd = new HoaDon();
                if (hd.DataContext == null)
                    return;
                var hoadonVM = hd.DataContext as HoaDonViewModel;
                hoadonVM.LoaiHD = (int)HoaDonViewModel.LoaiHoaDon.HoaDonLuuTru;
                hoadonVM.NhanVienLapHD = NhanVien;
                DATPHONG dp = hoadonVM.GetDatPhong(GetMaPhong(DSMaPhongChonThue));
                if (dp != null)
                {
                    hoadonVM.KhachHangThue = hoadonVM.GetKhachHang(dp);
                    hoadonVM.CMND_KH = hoadonVM.KhachHangThue.CMND_KH;
                }                    

                hoadonVM.ListCTKhuyenMai = ListCTKhuyenMai;
                if (CTKhuyenMai != null)
                    hoadonVM.CTKhuyenMai = CTKhuyenMai;
                hoadonVM.DSMaPhongChonThue = DSMaPhongChonThue;
                hoadonVM.GetDSThongTinPhongThue(DSMaPhongChonThue);
                hoadonVM.ListThongTinDatPhong = GetTTDatPhong(DSMaPhongChonThue);
                hd.ShowDialog();
            });

            ShowHDTongCommand = new RelayCommand<Object>((p) =>
            {
                if (DSMaPhongChonThue == null || DSMaPhongChonThue.Count() == 0 || DSMaPhongChonThue.Count > 1)
                {
                    MessageBox.Show("Vui lòng chọn 1 phòng muốn thanh toán!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                foreach (int item in DSMaPhongChonThue)
                {
                    var phong = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == item).SingleOrDefault();
                    if (phong != null && phong.TINHTRANG_PHONG == "Đang thuê")
                        return true;
                }

                MessageBox.Show("Phòng chưa được thuê không thể thanh toán!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }, (p) =>
            {
                HoaDon wd = new HoaDon();
                if (wd.DataContext == null)
                    return;
                var hoadonVM = wd.DataContext as HoaDonViewModel;
                hoadonVM.LoaiHD = (int)HoaDonViewModel.LoaiHoaDon.HoaDonTong;
                hoadonVM.HoaDon = hoadonVM.GetHoaDon(GetMaPhong(DSMaPhongChonThue));
                hoadonVM.NhanVienLapHD = hoadonVM.GetNhanVien(hoadonVM.HoaDon);
                hoadonVM.KhachHangThue = hoadonVM.GetKhachHang(hoadonVM.HoaDon);
                hoadonVM.CMND_KH = hoadonVM.KhachHangThue.CMND_KH;

                hoadonVM.ListCTKhuyenMai = ListCTKhuyenMai;
                if (CTKhuyenMai != null)
                    hoadonVM.CTKhuyenMai = CTKhuyenMai;
                hoadonVM.ThongBaoKM = ThongBaoKM;
                hoadonVM.GetDSThongTinPhongThue(DSMaPhongChonThue);
                wd.ShowDialog();
            });

            RefreshCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                DSMaPhongChonThue.Clear();
                MaPhongChonThue = "";
                RefreshTTP();
            });
        }

        public ObservableCollection<ThongTinPhong> LoadTTPhong()
        {
            ListTTPhong = new ObservableCollection<ThongTinPhong>();
            var listTTPhong = from p in DataProvider.Ins.model.PHONG
                              join lp in DataProvider.Ins.model.LOAIPHONG
                              on p.MA_LP equals lp.MA_LP    
                              
                              select new ThongTinPhong()
                              {
                                  Phong = p,
                                  LoaiPhong = lp
                              };
            foreach (ThongTinPhong item in listTTPhong)
            {
                ListTTPhong.Add(item);
            }
            return ListTTPhong;

        }

        public ObservableCollection<ThongTinPhong> LoadTTPhong(string TinhTrangPhong)
        {
            ListTTPhong = new ObservableCollection<ThongTinPhong>();
            var listTTPhong = from ph in DataProvider.Ins.model.PHONG
                              join lp in DataProvider.Ins.model.LOAIPHONG
                              on ph.MA_LP equals lp.MA_LP
                              where ph.TINHTRANG_PHONG == TinhTrangPhong
                              select new ThongTinPhong()
                              {
                                  Phong = ph,
                                  LoaiPhong = lp
                              };
            foreach (ThongTinPhong item in listTTPhong)
            {
                ListTTPhong.Add(item);
            }
            return ListTTPhong;
        }
        
        int GetMaPhong(ObservableCollection<int> dsmaphong)
        {
            foreach (int item in dsmaphong)
            {
                return item;
            }

            return 0;
        }

        KHUYENMAI GetKM_TiLeMax(ObservableCollection<KHUYENMAI> ds)
        {
            if (ds.Count() == 0 || ds == null)
                return null;
            
            int max = (int)ds.Max(m => m.TILE_KM);
            foreach (KHUYENMAI item in ds)
            {
                if (item.TILE_KM == max)
                    return item;
            }

            return null;
        }

        ObservableCollection<ThongTinDatPhong> GetTTDatPhong(ObservableCollection<int> dsmaphong)
        {
            if(dsmaphong == null || dsmaphong.Count() == 0)
                return null;

            ListThongTinDatPhong = new ObservableCollection<ThongTinDatPhong>();
            foreach (int maphong in dsmaphong)
            {
                var listTTDatPhong = from dp in DataProvider.Ins.model.DATPHONG
                                     join p in DataProvider.Ins.model.PHONG
                                     on dp.MA_PHONG equals p.MA_PHONG
                                     join nv in DataProvider.Ins.model.NHANVIEN
                                     on dp.MA_NV equals nv.MA_NV
                                     join kh in DataProvider.Ins.model.KHACHHANG
                                     on dp.MA_KH equals kh.MA_KH
                                     where dp.MA_PHONG == maphong && dp.NGAYBATDAU_DP > DateTime.Now
                                     select new ThongTinDatPhong()
                                     {
                                         MaPhong = maphong,
                                         TenNV = nv.HOTEN_NV,
                                         TenKH = kh.HOTEN_KH,
                                         NgayDatPhong = (DateTime)dp.NGAYBATDAU_DP,
                                         NgayTraPhong = (DateTime)dp.NGAYKETTHUC_DP
                                     };
                foreach (ThongTinDatPhong item in listTTDatPhong)
                {
                    ListThongTinDatPhong.Add(item);
                }
            }           

            return ListThongTinDatPhong;
        }

        void RefreshTTP()
        {
            var listdp = DataProvider.Ins.model.DATPHONG.ToList();
            foreach (DATPHONG item in listdp)
            {
                if (DateTime.Now.AddDays(1) > item.NGAYBATDAU_DP)
                {
                    var phong = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == item.MA_PHONG).SingleOrDefault();
                    if(phong.TINHTRANG_PHONG == "Trống")
                    {
                        phong.TINHTRANG_PHONG = "Đã đặt trước";
                        DataProvider.Ins.model.SaveChanges();
                    }                    
                }

                if(DateTime.Now > item.NGAYBATDAU_DP)
                {
                    var phong = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == item.MA_PHONG && x.TINHTRANG_PHONG == "Đã đặt trước").SingleOrDefault();
                    phong.TINHTRANG_PHONG = "Trống";
                    DataProvider.Ins.model.SaveChanges();
                }
            }
        }
    }
}
