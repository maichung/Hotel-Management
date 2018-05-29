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
    public class MatHangViewModel : BaseViewModel
    {
        //
        private ObservableCollection<MATHANG> _ListMatHang;
        public ObservableCollection<MATHANG> ListMatHang { get => _ListMatHang; set { _ListMatHang = value; OnPropertyChanged(); } }
        //
        private ObservableCollection<ThongTinOrder> _ListOrder;
        public ObservableCollection<ThongTinOrder> ListOrder { get => _ListOrder; set { _ListOrder = value; OnPropertyChanged(); } }
        //
        private ObservableCollection<string> _ListLoaiPhucVu;
        public ObservableCollection<string> ListLoaiPhucVu { get => _ListLoaiPhucVu; set { _ListLoaiPhucVu = value; OnPropertyChanged(); } }
        private string _SelectedLoaiPhucVu;
        public string SelectedLoaiPhucVu { get => _SelectedLoaiPhucVu; set { _SelectedLoaiPhucVu = value; OnPropertyChanged(); } }
        private MATHANG _SelectedItemMH;
        public MATHANG SelectedItemMH { get => _SelectedItemMH; set { _SelectedItemMH = value; OnPropertyChanged(); } }
        private ThongTinOrder _SelectedItemOrder;
        public ThongTinOrder SelectedItemOrder
        {
            get => _SelectedItemOrder;
            set
            {
                _SelectedItemOrder = value;
                OnPropertyChanged();
                if (SelectedItemOrder != null)
                {
                    if (Order == null)
                    {
                        Order = new ThongTinOrder();
                    }
                    Order.MatHang = SelectedItemMH;
                    Order.SoLuong = 1;
                    Order.ThanhTien = Order.SoLuong * (int)Order.MatHang.DONGIA_MH;
                }
            }
        }
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
        private ThongTinOrder _Order;
        public ThongTinOrder Order { get => _Order; set { _Order = value; OnPropertyChanged(); } }
        private long _TongTien;
        public long TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
        private int _TongSoLuongMHDC;
        public int TongSoLuongMHDC { get => _TongSoLuongMHDC; set { _TongSoLuongMHDC = value; OnPropertyChanged(); } }
        private string _TenMatHang;
        public string TenMatHang { get => _TenMatHang; set { _TenMatHang = value; OnPropertyChanged(); } }
        private int _DonGia;
        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        private DateTime? _NgayNhap;
        public DateTime? NgayNhap { get => _NgayNhap; set { _NgayNhap = value; OnPropertyChanged(); } }
        private string _SearchMatHang;
        public string SearchMatHang { get => _SearchMatHang; set { _SearchMatHang = value; OnPropertyChanged(); } }

        //Dịch vụ ăn uống
        public ICommand AddOrderCommand { get; set; }
        public ICommand DeleteOrderCommand { get; set; }
        public ICommand ThemSLCommand { get; set; }
        public ICommand BotSLCommand { get; set; }
        //Tra cứu và quản lý
        public ICommand SearchMatHangCommand { get; set; }
        public ICommand AddMHCommand { get; set; }
        public ICommand EditMHCommand { get; set; }

        public MatHangViewModel()
        {
            ListMatHang = new ObservableCollection<MATHANG>(DataProvider.Ins.model.MATHANG);
            ListOrder = new ObservableCollection<ThongTinOrder>();
            TongTien = 0;
            TongSoLuongMHDC = 0;
            string[] arrayLPV = new string[] { "Tại phòng", "Tại sảnh ăn uống" };
            ListLoaiPhucVu = new ObservableCollection<string>(arrayLPV);

            AddOrderCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItemMH == null)
                    return false;

                if (ListOrder.Count != 0)
                {
                    foreach (ThongTinOrder item in ListOrder)
                    {
                        if (SelectedItemMH.MA_MH == item.MatHang.MA_MH)
                            return false;
                    }
                }

                return true;
            }, (p) =>
            {
                ThongTinOrder orderMatHang = new ThongTinOrder() { MatHang = SelectedItemMH, SoLuong = 1, ThanhTien = (int)SelectedItemMH.DONGIA_MH };
                ListOrder.Add(orderMatHang);
                TongTien += (int)orderMatHang.MatHang.DONGIA_MH;
                TongSoLuongMHDC++;
            });

            DeleteOrderCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItemOrder == null)
                    return false;

                return true;
            }, (p) =>
            {
                int i = 0;
                foreach (ThongTinOrder item in ListOrder)
                {
                    if (item.MatHang.MA_MH == SelectedItemOrder.MatHang.MA_MH)
                    {
                        ListOrder.Remove(item);
                        TongTien -= item.ThanhTien;
                        TongSoLuongMHDC -= item.SoLuong;
                        break;
                    }
                    i++;
                }
            });

            ThemSLCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItemOrder == null)
                    return false;

                return true;
            }, (p) =>
            {
                foreach (ThongTinOrder item in ListOrder)
                {
                    if (item.MatHang.MA_MH == SelectedItemOrder.MatHang.MA_MH)
                    {
                        item.SoLuong++;
                        item.ThanhTien = item.SoLuong * (int)item.MatHang.DONGIA_MH;
                        TongTien += (int)item.MatHang.DONGIA_MH;
                        TongSoLuongMHDC++;
                        break;
                    }
                }
            });

            BotSLCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItemOrder == null)
                    return false;

                foreach (ThongTinOrder item in ListOrder)
                {
                    if (item.MatHang.MA_MH == SelectedItemOrder.MatHang.MA_MH)
                    {
                        if (item.SoLuong == 1)
                            return false;
                    }
                }

                return true;
            }, (p) =>
            {
                foreach (ThongTinOrder item in ListOrder)
                {
                    if (item.MatHang.MA_MH == SelectedItemOrder.MatHang.MA_MH)
                    {
                        item.SoLuong--;
                        item.ThanhTien = item.SoLuong * (int)item.MatHang.DONGIA_MH;
                        TongTien -= (int)item.MatHang.DONGIA_MH;
                        TongSoLuongMHDC--;
                        break;
                    }
                }
            });

            SearchMatHangCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                if (!string.IsNullOrEmpty(SearchMatHang))
                {
                    CollectionViewSource.GetDefaultView(ListMatHang).Filter = (searchMatHang) =>
                    {
                        return (searchMatHang as MATHANG).TEN_MH.StartsWith(SearchMatHang) ||
                               (searchMatHang as MATHANG).DONGIA_MH.ToString().StartsWith(SearchMatHang);
                    };
                }
                else
                {
                    CollectionViewSource.GetDefaultView(ListMatHang).Filter = (all) => { return true; };
                }

            });

            AddMHCommand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(TenMatHang) || string.IsNullOrEmpty(DonGia.ToString()))
                    return false;

                var listMatHang = DataProvider.Ins.model.MATHANG.Where(x => x.TEN_MH == TenMatHang);
                if (listMatHang == null || listMatHang.Count() != 0)
                    return false;

                return true;
            }, (p) =>
            {
                MATHANG matHang = new MATHANG() { TEN_MH = TenMatHang, DONGIA_MH = DonGia, NGAYNHAP_MH = NgayNhap };
                DataProvider.Ins.model.MATHANG.Add(matHang);
                DataProvider.Ins.model.SaveChanges();

                ListMatHang.Add(matHang);
            });

            EditMHCommand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(TenMatHang) || string.IsNullOrEmpty(DonGia.ToString()) || SelectedItem == null)
                    return false;

                var listMatHang = DataProvider.Ins.model.MATHANG.Where(x => x.TEN_MH == TenMatHang);
                if (listMatHang != null && listMatHang.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                var matHang = DataProvider.Ins.model.MATHANG.Where(x => x.MA_MH == SelectedItem.MA_MH).SingleOrDefault();
                matHang.TEN_MH = TenMatHang;
                matHang.DONGIA_MH = DonGia;
                matHang.NGAYNHAP_MH = NgayNhap;
                DataProvider.Ins.model.SaveChanges();
            });
        }
    }
}
