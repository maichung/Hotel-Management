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
    class MainViewModel : BaseViewModel
    {
        #region Khai báo biến, command ẩn/hiện nội dung phần mềm
        private bool _isTrangChuVisible = true;
        public bool isTrangChuVisible { get => _isTrangChuVisible; set { _isTrangChuVisible = value; OnPropertyChanged(); } }
        public ICommand btnTrangChuCommand { get; set; }

        private bool _isAnUongVisible = false;
        public bool isAnUongVisible { get => _isAnUongVisible; set { _isAnUongVisible = value; OnPropertyChanged(); } }
        public ICommand btnAnUongCommand { get; set; }

        private bool _isGiatUiVisible = false;
        public bool isGiatUiVisible { get => _isGiatUiVisible; set { _isGiatUiVisible = value; OnPropertyChanged(); } }
        public ICommand btnGiatUiCommand { get; set; }

        private bool _isDiChuyenVisible = false;
        public bool isDiChuyenVisible { get => _isDiChuyenVisible; set { _isDiChuyenVisible = value; OnPropertyChanged(); } }
        public ICommand btnDiChuyenCommand { get; set; }

        private bool _isTraCuuVisible = false;
        public bool isTraCuuVisible { get => _isTraCuuVisible; set { _isTraCuuVisible = value; OnPropertyChanged(); } }
        public ICommand btnTraCuuCommand { get; set; }

        private bool _isBaoCaoVisible = false;
        public bool isBaoCaoVisible { get => _isBaoCaoVisible; set { _isBaoCaoVisible = value; OnPropertyChanged(); } }
        public ICommand btnBaoCaoCommand { get; set; }

        private bool _isThongTinVisible = false;
        public bool isThongTinVisible { get => _isThongTinVisible; set { _isThongTinVisible = value; OnPropertyChanged(); } }
        public ICommand btnThongTinCommand { get; set; }
        #endregion
        
        private ObservableCollection<ThongTinPhong> _ListTTPhong;
        public ObservableCollection<ThongTinPhong> ListTTPhong { get => _ListTTPhong; set { _ListTTPhong = value; OnPropertyChanged(); } }
        private ObservableCollection<ThongTinPhong> _ListTTPhongDangThue;
        public ObservableCollection<ThongTinPhong> ListTTPhongDangThue { get => _ListTTPhongDangThue; set { _ListTTPhongDangThue = value; OnPropertyChanged(); } }
        private int _SoPhong;
        public int SoPhong { get => _SoPhong; set { _SoPhong = value; OnPropertyChanged(); } }
        private int _SoPhongTrong;
        public int SoPhongTrong { get => _SoPhongTrong; set { _SoPhongTrong = value; OnPropertyChanged(); } }
        private int _SoPhongDangThue;
        public int SoPhongDangThue { get => _SoPhongDangThue; set { _SoPhongDangThue = value; OnPropertyChanged(); } }
        private int _SoPhongDatTruoc;
        public int SoPhongDatTruoc { get => _SoPhongDatTruoc; set { _SoPhongDatTruoc = value; OnPropertyChanged(); } }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand DangXuatCommand { get; set; }
        public ICommand TatCaCommand { get; set; }
        public ICommand TrongCommand { get; set; }
        public ICommand DangThueCommand { get; set; }
        public ICommand DaDatTruocCommand { get; set; }


        public MainViewModel()
        {
            #region Command ẩn/hiện nội dung
            btnTrangChuCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
              {
                  isTrangChuVisible = true;
                  isAnUongVisible = false;
                  isGiatUiVisible = false;
                  isDiChuyenVisible = false;
                  isTraCuuVisible = false;
                  isBaoCaoVisible = false;
                  isThongTinVisible = false;
              });

            btnAnUongCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                isTrangChuVisible = false;
                isAnUongVisible = true;
                isGiatUiVisible = false;
                isDiChuyenVisible = false;
                isTraCuuVisible = false;
                isBaoCaoVisible = false;
                isThongTinVisible = false;
            });
            btnGiatUiCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                isTrangChuVisible = false;
                isAnUongVisible = false;
                isGiatUiVisible = true;
                isDiChuyenVisible = false;
                isTraCuuVisible = false;
                isBaoCaoVisible = false;
                isThongTinVisible = false;
            });
            btnDiChuyenCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                isTrangChuVisible = false;
                isAnUongVisible = false;
                isGiatUiVisible = false;
                isDiChuyenVisible = true;
                isTraCuuVisible = false;
                isBaoCaoVisible = false;
                isThongTinVisible = false;
            });
            btnTraCuuCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                isTrangChuVisible = false;
                isAnUongVisible = false;
                isGiatUiVisible = false;
                isDiChuyenVisible = false;
                isTraCuuVisible = true;
                isBaoCaoVisible = false;
                isThongTinVisible = false;
            });
            btnBaoCaoCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                isTrangChuVisible = false;
                isAnUongVisible = false;
                isGiatUiVisible = false;
                isDiChuyenVisible = false;
                isTraCuuVisible = false;
                isBaoCaoVisible = true;
                isThongTinVisible = false;
            });
            btnThongTinCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                isTrangChuVisible = false;
                isAnUongVisible = false;
                isGiatUiVisible = false;
                isDiChuyenVisible = false;
                isTraCuuVisible = false;
                isBaoCaoVisible = false;
                isThongTinVisible = true;
            });
            #endregion
            
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
              {
                  p.Show();
                  LoadTTPhong();

                  SoPhong = ListTTPhong.Count();
                  SoPhongTrong = ListTTPhong.Where(x => x.Phong.TINHTRANG_PHONG == "Trống").Count();
                  SoPhongDangThue = ListTTPhong.Where(x => x.Phong.TINHTRANG_PHONG == "Đang thuê").Count();
                  SoPhongDatTruoc = ListTTPhong.Where(x => x.Phong.TINHTRANG_PHONG == "Đã đặt trước").Count();
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

            TatCaCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ListTTPhong = LoadTTPhong();
            });

            TrongCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ListTTPhong = LoadTTPhong("Trống");
            });

            DangThueCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ListTTPhong = LoadTTPhong("Đang thuê");
            });

            DaDatTruocCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ListTTPhong = LoadTTPhong("Đã đặt trước");
            });

            ListTTPhongDangThue = LoadTTPhong("Đang thuê");
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

        public void SetVisibility(bool isVisible)
        {
            bool[] list = { isTrangChuVisible, isAnUongVisible, isGiatUiVisible, isDiChuyenVisible, isTraCuuVisible, isBaoCaoVisible, isThongTinVisible };
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == isVisible)
                {
                    list[i] = true;
                }
                else
                {
                    list[i] = false;
                }
            }

        }

    }
}
