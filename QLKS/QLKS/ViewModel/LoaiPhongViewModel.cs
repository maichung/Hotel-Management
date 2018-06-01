using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    public class LoaiPhongViewModel : BaseViewModel
    {
        private ObservableCollection<LOAIPHONG> _ListLoaiPhong;
        public ObservableCollection<LOAIPHONG> ListLoaiPhong { get => _ListLoaiPhong; set { _ListLoaiPhong = value; OnPropertyChanged(); } }
        private LOAIPHONG _SelectedItem;
        public LOAIPHONG SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenLoaiPhong = SelectedItem.TEN_LP;
                    DonGia = (int)SelectedItem.DONGIA_LP;
                }
            }
        }
        private string _TenLoaiPhong;
        public string TenLoaiPhong { get => _TenLoaiPhong; set { _TenLoaiPhong = value; OnPropertyChanged(); } }
        private int _DonGia;
        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        private string _SearchLoaiPhong;
        public string SearchLoaiPhong { get => _SearchLoaiPhong; set { _SearchLoaiPhong = value; OnPropertyChanged(); } }

        public ICommand SearchLoaiPhongCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public LoaiPhongViewModel()
        {
            ListLoaiPhong = new ObservableCollection<LOAIPHONG>(DataProvider.Ins.model.LOAIPHONG);

            SearchLoaiPhongCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                if (!string.IsNullOrEmpty(SearchLoaiPhong))
                {
                    CollectionViewSource.GetDefaultView(ListLoaiPhong).Filter = (searchLoaiPhong) =>
                    {
                        return (searchLoaiPhong as LOAIPHONG).TEN_LP.StartsWith(SearchLoaiPhong) ||
                               (searchLoaiPhong as LOAIPHONG).DONGIA_LP.ToString().StartsWith(SearchLoaiPhong);
                    };
                }
                else
                {
                    CollectionViewSource.GetDefaultView(ListLoaiPhong).Filter = (all) => { return true; };
                }

            });

            AddCommand = new RelayCommand<Object>((p) => {
                if (string.IsNullOrEmpty(TenLoaiPhong) || string.IsNullOrEmpty(DonGia.ToString()))
                    return false;

                var listLoaiPhong = DataProvider.Ins.model.LOAIPHONG.Where(x => x.TEN_LP == TenLoaiPhong);
                if (listLoaiPhong == null || listLoaiPhong.Count() != 0)
                    return false;

                return true;
            }, (p) => {
                var loaiPhong = new LOAIPHONG() { TEN_LP = TenLoaiPhong, DONGIA_LP = DonGia };

                DataProvider.Ins.model.LOAIPHONG.Add(loaiPhong);
                DataProvider.Ins.model.SaveChanges();

                ListLoaiPhong.Add(loaiPhong);
            });

            EditCommand = new RelayCommand<Object>((p) => {
                if (string.IsNullOrEmpty(TenLoaiPhong) || string.IsNullOrEmpty(DonGia.ToString()) || SelectedItem == null)
                    return false;

                var listLoaiPhong = DataProvider.Ins.model.LOAIPHONG.Where(x => x.MA_LP == SelectedItem.MA_LP);
                if (listLoaiPhong != null && listLoaiPhong.Count() != 0)
                    return true;

                return false;
            }, (p) => {
                var loaiPhong = DataProvider.Ins.model.LOAIPHONG.Where(x => x.MA_LP == SelectedItem.MA_LP).SingleOrDefault();
                loaiPhong.TEN_LP = TenLoaiPhong;
                loaiPhong.DONGIA_LP = DonGia;
                DataProvider.Ins.model.SaveChanges();
            });

            RefreshCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                TenLoaiPhong = null;
                DonGia = 0;
            });
        }
    }
}
