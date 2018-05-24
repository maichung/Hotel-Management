using QuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKhachSan.ViewModel
{
    public class KhachHangViewModel : BaseViewModel
    {
        private ObservableCollection<KHACHHANG> _ListKhachHang;
        public ObservableCollection<KHACHHANG> ListKhachHang { get => _ListKhachHang; set { _ListKhachHang = value; OnPropertyChanged(); } }
        private KHACHHANG _SelectedItem;
        public KHACHHANG SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenKhachHang = SelectedItem.HOTEN_KH;
                    SoDienThoai = SelectedItem.SODIENTHOAI_KH;
                    CMND = SelectedItem.CMND_KH;
                }
            }
        }
        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }
        private string _SoDienThoai;
        public string SoDienThoai { get => _SoDienThoai; set { _SoDienThoai = value; OnPropertyChanged(); } }
        private string _CMND;
        public string CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public KhachHangViewModel()
        {
            ListKhachHang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.model.KHACHHANGs);

            AddCommand = new RelayCommand<Object>((p) => {
                if (string.IsNullOrEmpty(TenKhachHang) || string.IsNullOrEmpty(CMND))
                    return false;

                var listKhachHang = DataProvider.Ins.model.KHACHHANGs.Where(x => x.CMND_KH == CMND);
                if (listKhachHang == null || listKhachHang.Count() != 0)
                    return false;

                return true;
            }, (p) => {
                var khachHang = new KHACHHANG() { HOTEN_KH = TenKhachHang, SODIENTHOAI_KH = SoDienThoai, CMND_KH = CMND };

                DataProvider.Ins.model.KHACHHANGs.Add(khachHang);
                DataProvider.Ins.model.SaveChanges();

                ListKhachHang.Add(khachHang);
                TenKhachHang = "";
                SoDienThoai = "";
                CMND = "";
            });

            EditCommand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(TenKhachHang) || string.IsNullOrEmpty(CMND) || SelectedItem == null)
                    return false;

                var listKhachHang = DataProvider.Ins.model.KHACHHANGs.Where(x => x.MA_KH == SelectedItem.MA_KH);
                if (listKhachHang != null && listKhachHang.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                var khachHang = DataProvider.Ins.model.KHACHHANGs.Where(x => x.MA_KH == SelectedItem.MA_KH).SingleOrDefault();
                khachHang.HOTEN_KH = TenKhachHang;
                khachHang.SODIENTHOAI_KH = SoDienThoai;
                khachHang.CMND_KH = CMND;
                DataProvider.Ins.model.SaveChanges();
            });
        }
    }
}
