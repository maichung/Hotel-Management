using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    public class DiChuyenViewModel : BaseViewModel
    {
        private ObservableCollection<ThongTinPhong> _ListTTPhongDangThue;
        public ObservableCollection<ThongTinPhong> ListTTPhongDangThue { get => _ListTTPhongDangThue; set { _ListTTPhongDangThue = value; OnPropertyChanged(); } }

        private ObservableCollection<CHUYENDI> _ListChuyenDi;
        public ObservableCollection<CHUYENDI> ListChuyenDi { get => _ListChuyenDi; set { _ListChuyenDi = value; OnPropertyChanged(); } }
        private CHUYENDI _SelectedItem;
        public CHUYENDI SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DonGia = (int)SelectedItem.DONGIA_CD;
                    DiemDen = SelectedItem.DIEMDEN_CD;
                }
            }
        }
        private string _DiemDen;
        public string DiemDen { get => _DiemDen; set { _DiemDen = value; OnPropertyChanged(); } }
        private int _DonGia;
        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        private string _SearchChuyenDi;
        public string SearchChuyenDi { get => _SearchChuyenDi; set { _SearchChuyenDi = value; OnPropertyChanged(); } }
        public bool sort;
        //Chọn phòng thực hiện sử dụng dịch vụ ăn uống
        private int _MaPhong;
        public int MaPhong { get => _MaPhong; set { _MaPhong = value; OnPropertyChanged(); } }
        private ThongTinPhong _SelectedPhong;
        public ThongTinPhong SelectedPhong { get => _SelectedPhong; set { _SelectedPhong = value; OnPropertyChanged(); if (_SelectedPhong != null) MaPhong = SelectedPhong.Phong.MA_PHONG; } }
        private KHACHHANG _KhachHangThue;
        public KHACHHANG KhachHangThue { get => _KhachHangThue; set { _KhachHangThue = value; OnPropertyChanged(); } }
        private NHANVIEN _NhanVienLapHD;
        public NHANVIEN NhanVienLapHD { get => _NhanVienLapHD; set { _NhanVienLapHD = value; OnPropertyChanged(); } }

        public ICommand ShowHDDiChuyenCommand { get; set; }
        public ICommand SearchChuyenDiCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand SortChuyenDiCommand { get; set; }

        public DiChuyenViewModel()
        {
            ListChuyenDi = new ObservableCollection<CHUYENDI>(DataProvider.Ins.model.CHUYENDI);
            DonGia = 0;

            ShowHDDiChuyenCommand = new RelayCommand<Object>((p) =>
            {
                if (DonGia == 0 || SelectedPhong == null || SelectedItem == null)
                    return false;

                return true;
            }, (p) =>
            {              
                HoaDon wd = new HoaDon();
                if (wd.DataContext == null)
                    return;
                var hoadonVM = wd.DataContext as HoaDonViewModel;
                hoadonVM.LoaiHD = (int)HoaDonViewModel.LoaiHoaDon.HoaDonDiChuyen;
                hoadonVM.HoaDon = hoadonVM.GetHoaDon(MaPhong);
                hoadonVM.NhanVienLapHD = hoadonVM.GetNhanVien(hoadonVM.HoaDon);
                hoadonVM.KhachHangThue = hoadonVM.GetKhachHang(hoadonVM.HoaDon);
                hoadonVM.GetThongTinPhongThue(MaPhong);

                hoadonVM.TongTienHDDC = (long)SelectedItem.DONGIA_CD;
                hoadonVM.ChuyenDi = SelectedItem;
                wd.ShowDialog();
                RefershControlsDVDC();
            });

            SearchChuyenDiCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(SearchChuyenDi))
                {
                    CollectionViewSource.GetDefaultView(ListChuyenDi).Filter = (all) => { return true; };
                }
                else
                {
                    CollectionViewSource.GetDefaultView(ListChuyenDi).Filter = (searchChuyenDi) =>
                    {
                        return (searchChuyenDi as CHUYENDI).DIEMDEN_CD.IndexOf(SearchChuyenDi, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               (searchChuyenDi as CHUYENDI).DONGIA_CD.ToString().IndexOf(SearchChuyenDi, StringComparison.OrdinalIgnoreCase) >= 0;
                    };
                }

            });

            AddCommand = new RelayCommand<Object>((p) =>
            {
                if (String.IsNullOrEmpty(DiemDen) || String.IsNullOrEmpty(DonGia.ToString()))
                {
                    return false;
                }
                var cd = DataProvider.Ins.model.CHUYENDI.Where(x => x.DIEMDEN_CD == DiemDen);
                if (cd == null || cd.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var cd = new CHUYENDI() { DIEMDEN_CD = DiemDen, DONGIA_CD = DonGia };
                DataProvider.Ins.model.CHUYENDI.Add(cd);
                DataProvider.Ins.model.SaveChanges();
            });

            EditCommand = new RelayCommand<Object>((p) =>
            {
                if (String.IsNullOrEmpty(DiemDen) || String.IsNullOrEmpty(DonGia.ToString()) || SelectedItem == null)
                {
                    return false;
                }
                var cd = DataProvider.Ins.model.CHUYENDI.Where(x => x.DIEMDEN_CD == DiemDen);
                if (cd != null && cd.Count() != 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                var cd = DataProvider.Ins.model.CHUYENDI.Where(x => x.DIEMDEN_CD == DiemDen).SingleOrDefault();
                cd.DIEMDEN_CD = DiemDen;
                cd.DONGIA_CD = DonGia;
                DataProvider.Ins.model.SaveChanges();
            });

            RefreshCommand = new RelayCommand<Object>((p) =>{ return true; }, (p) => { DiemDen = null; DonGia = 0; });

            SortChuyenDiCommand = new RelayCommand<GridViewColumnHeader>((p) => { return p == null ? false : true; }, (p) =>
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListChuyenDi);
                if (sort)
                {
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription(p.Tag.ToString(), ListSortDirection.Ascending));
                }
                else
                {
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription(p.Tag.ToString(), ListSortDirection.Descending));
                }
                sort = !sort;
            });
        }

        public void GetTTPhongDangThue()
        {
            ListTTPhongDangThue = new ObservableCollection<ThongTinPhong>();
            var listTTPhongdangthue = from ph in DataProvider.Ins.model.PHONG
                                      join lp in DataProvider.Ins.model.LOAIPHONG
                                      on ph.MA_LP equals lp.MA_LP
                                      where ph.TINHTRANG_PHONG == "Đang thuê"
                                      select new ThongTinPhong()
                                      {
                                          Phong = ph,
                                          LoaiPhong = lp
                                      };
            foreach (ThongTinPhong item in listTTPhongdangthue)
            {
                ListTTPhongDangThue.Add(item);
            }
        }

        void RefershControlsDVDC()
        {
            SelectedPhong = null;
            SelectedItem = null;
            DonGia = 0;
            DiemDen = null;
        }
    }
}
