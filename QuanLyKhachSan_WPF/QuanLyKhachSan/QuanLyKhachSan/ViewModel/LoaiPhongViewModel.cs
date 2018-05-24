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
    public class LoaiPhongViewModel : BaseViewModel
    {
        private ObservableCollection<LOAIPHONG> _ListLoaiPhong;
        public ObservableCollection<LOAIPHONG> ListLoaiPhong { get => _ListLoaiPhong; set { _ListLoaiPhong = value; OnPropertyChanged(); } }
        private LOAIPHONG _SelectedItem;
        public LOAIPHONG SelectedItem { get => _SelectedItem;
            set {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null) {
                    TenLoaiPhong = SelectedItem.TEN_LP;
                    DonGia = (int)SelectedItem.DONGIA_LP;
                }
            }
        }
        private string _TenLoaiPhong;
        public string TenLoaiPhong { get => _TenLoaiPhong; set { _TenLoaiPhong = value; OnPropertyChanged(); } }
        private int _DonGia;
        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public LoaiPhongViewModel()
        {
            ListLoaiPhong = new ObservableCollection<LOAIPHONG>(DataProvider.Ins.model.LOAIPHONGs);

            AddCommand = new RelayCommand<Object>((p) => {
                if (string.IsNullOrEmpty(TenLoaiPhong) || string.IsNullOrEmpty(DonGia.ToString()))
                    return false;

                var listLoaiPhong = DataProvider.Ins.model.LOAIPHONGs.Where(x => x.TEN_LP == TenLoaiPhong);
                if (listLoaiPhong == null || listLoaiPhong.Count() != 0)
                    return false;

                return true;
            }, (p) => {
                var LoaiPhong = new LOAIPHONG() { TEN_LP = TenLoaiPhong, DONGIA_LP = DonGia };

                DataProvider.Ins.model.LOAIPHONGs.Add(LoaiPhong);
                DataProvider.Ins.model.SaveChanges();

                ListLoaiPhong.Add(LoaiPhong);
            });

            EditCommand = new RelayCommand<Object>((p) => {
                if (string.IsNullOrEmpty(TenLoaiPhong) || string.IsNullOrEmpty(DonGia.ToString()) || SelectedItem == null)
                    return false;

                var listLoaiPhong = DataProvider.Ins.model.LOAIPHONGs.Where(x => x.MA_LP == SelectedItem.MA_LP);
                if (listLoaiPhong != null && listLoaiPhong.Count() != 0)
                    return true;

                return false;
            }, (p) => {
                var LoaiPhong = DataProvider.Ins.model.LOAIPHONGs.Where(x => x.MA_LP == SelectedItem.MA_LP).SingleOrDefault();
                LoaiPhong.TEN_LP = TenLoaiPhong;
                LoaiPhong.DONGIA_LP = DonGia;
                DataProvider.Ins.model.SaveChanges();
            });
        }
    }
}
