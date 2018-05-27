using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    public class MatHangViewModel : BaseViewModel
    {
        private ObservableCollection<MATHANG> _ListMatHang;
        public ObservableCollection<MATHANG> ListMatHang { get => _ListMatHang; set { _ListMatHang = value; OnPropertyChanged(); } }
        private MATHANG _SelectedItem;
        public MATHANG SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenMatHang = SelectedItem.TEN_MH;
                    DonGia = (int)SelectedItem.DONGIA_MH;
                    NgayNhap = SelectedItem.NGAYNHAP_MH;
                }
            }
        }
        private string _TenMatHang;
        public string TenMatHang { get => _TenMatHang; set { _TenMatHang = value; OnPropertyChanged(); } }
        private int _DonGia;
        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        private DateTime? _NgayNhap;
        public DateTime? NgayNhap { get => _NgayNhap; set { _NgayNhap = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public MatHangViewModel()
        {
            ListMatHang = new ObservableCollection<MATHANG>(DataProvider.Ins.model.MATHANG);

            AddCommand = new RelayCommand<Object>((p) => {
                if (string.IsNullOrEmpty(TenMatHang) || string.IsNullOrEmpty(DonGia.ToString()))
                    return false;

                var listMatHang = DataProvider.Ins.model.MATHANG.Where(x => x.TEN_MH == TenMatHang);
                if (listMatHang == null || listMatHang.Count() != 0)
                    return false;

                return true;
            }, (p) => {
                var matHang = new MATHANG() { TEN_MH = TenMatHang, DONGIA_MH = DonGia, NGAYNHAP_MH = NgayNhap };

                DataProvider.Ins.model.MATHANG.Add(matHang);
                DataProvider.Ins.model.SaveChanges();

                ListMatHang.Add(matHang);
            });

            EditCommand = new RelayCommand<Object>((p) => {
                if (string.IsNullOrEmpty(TenMatHang) || string.IsNullOrEmpty(DonGia.ToString()) || SelectedItem == null)
                    return false;

                var listMatHang = DataProvider.Ins.model.MATHANG.Where(x => x.MA_MH == SelectedItem.MA_MH);
                if (listMatHang != null && listMatHang.Count() != 0)
                    return true;

                return false;
            }, (p) => {
                var matHang = DataProvider.Ins.model.MATHANG.Where(x => x.MA_MH == SelectedItem.MA_MH).SingleOrDefault();
                matHang.TEN_MH = TenMatHang;
                matHang.DONGIA_MH = DonGia;
                matHang.NGAYNHAP_MH = NgayNhap;
                DataProvider.Ins.model.SaveChanges();
            });
        }
    }
}
