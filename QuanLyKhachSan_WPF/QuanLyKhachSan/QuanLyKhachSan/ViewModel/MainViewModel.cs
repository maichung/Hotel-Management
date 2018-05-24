using QuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhachSan.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<ThongTinPhong> _ListTTPhong;
        public ObservableCollection<ThongTinPhong> ListTTPhong { get => _ListTTPhong; set { _ListTTPhong = value; OnPropertyChanged(); } }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoaiPhongCommand { get; set; }
        public ICommand NhanVienCommand { get; set; }
        public ICommand KhachHangCommand { get; set; }

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                p.Hide();
                DangNhap manHinhDangNhap = new DangNhap();
                manHinhDangNhap.ShowDialog();
                //Lấy ra DangNhapViewModel sau đó kiểm tra xem đã đang nhập thành công hay chưa mới mở trang chủ lên
                if (manHinhDangNhap.DataContext == null)
                    return;
                var dangnhapVM = manHinhDangNhap.DataContext as DangNhapViewModel;
                if (dangnhapVM.ktDangNhap)
                {
                    p.Show();
                    LoadTTPhong();
                }
                else
                    p.Close();
            });
            LoaiPhongCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { ManHinhLoaiPhong wd = new ManHinhLoaiPhong(); wd.ShowDialog(); });
            NhanVienCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { ManHinhNhanVien wd = new ManHinhNhanVien(); wd.ShowDialog(); });
            KhachHangCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { ManHinhKhachHang wd = new ManHinhKhachHang(); wd.ShowDialog(); });
        }

        void LoadTTPhong()
        {
            ListTTPhong = new ObservableCollection<ThongTinPhong>();

            var listTTPhong = from p in DataProvider.Ins.model.PHONGs
                              join lp in DataProvider.Ins.model.LOAIPHONGs
                              on p.MA_LP equals lp.MA_LP
                              select new ThongTinPhong()
                              {
                                  MaPhong = p.MA_PHONG,
                                  LoaiPhong = lp.TEN_LP,
                                  DonGia = (int)lp.DONGIA_LP,
                                  TinhTrangPhong = p.TINHTRANG_PHONG
                              };
            foreach(ThongTinPhong item in listTTPhong)
            {
                ListTTPhong.Add(item);
            }
        }
    }
}
