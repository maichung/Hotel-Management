using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    class ChuongTrinhKhuyenMaiViewModel : BaseViewModel
    {
        private ObservableCollection<KHUYENMAI> _ListCTKhuyenMai;
        public ObservableCollection<KHUYENMAI> ListCTKhuyenMai { get => _ListCTKhuyenMai; set { _ListCTKhuyenMai = value; OnPropertyChanged(); } }
        private KHUYENMAI _SelectedItem;
        public KHUYENMAI SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenKM = SelectedItem.TEN_KM;
                    NgayBatDauKM = SelectedItem.NGAYBATDAU_KM;
                    NgayKetThucKM = SelectedItem.NGAYKETTHUC_KM;
                    TiLeKM = (int)SelectedItem.TILE_KM;
                }
            }
        }
        private string _TenKM;
        public string TenKM { get => _TenKM; set { _TenKM = value; OnPropertyChanged(); } }
        private DateTime? _NgayBatDauKM;
        public DateTime? NgayBatDauKM { get => _NgayBatDauKM; set { _NgayBatDauKM = value; OnPropertyChanged(); } }
        private DateTime? _NgayKetThucKM;
        public DateTime? NgayKetThucKM { get => _NgayKetThucKM; set { _NgayKetThucKM = value; OnPropertyChanged(); } }
        private int _TiLeKM;
        public int TiLeKM { get => _TiLeKM; set { _TiLeKM = value; OnPropertyChanged(); } }
        private string _SearchKhuyenMai;
        public string SearchKhuyenMai { get => _SearchKhuyenMai; set { _SearchKhuyenMai = value; OnPropertyChanged(); } }
        public bool sort;

        public ICommand SearchKhuyenMaiCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand SortKhuyenMaiCommand { get; set; }

        public ChuongTrinhKhuyenMaiViewModel()
        {
            ListCTKhuyenMai = new ObservableCollection<KHUYENMAI>(DataProvider.Ins.model.KHUYENMAI);

            SearchKhuyenMaiCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(SearchKhuyenMai))
                {
                    CollectionViewSource.GetDefaultView(ListCTKhuyenMai).Filter = (all) => { return true; };
                }
                else
                {
                    CollectionViewSource.GetDefaultView(ListCTKhuyenMai).Filter = (searchKhuyenMai) =>
                    {
                        return (searchKhuyenMai as KHUYENMAI).TEN_KM.IndexOf(SearchKhuyenMai, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               (searchKhuyenMai as KHUYENMAI).NGAYBATDAU_KM.ToString().IndexOf(SearchKhuyenMai, StringComparison.OrdinalIgnoreCase) >= 0;
                    };
                }
            });

            AddCommand = new RelayCommand<Object>((p) =>
            {
                if (String.IsNullOrEmpty(TenKM) || String.IsNullOrEmpty(TiLeKM.ToString()))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin chương trình khuyến mãi muốn thêm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                var km = DataProvider.Ins.model.KHUYENMAI.Where(x => x.TEN_KM == TenKM);
                if (km == null || km.Count() != 0)
                {
                    MessageBox.Show("Chương trình khuyến mãi đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                return true;
            }, (p) =>
            {
                var km = new KHUYENMAI() { TEN_KM = TenKM, NGAYBATDAU_KM = NgayBatDauKM, NGAYKETTHUC_KM = NgayKetThucKM, TILE_KM = TiLeKM };
                DataProvider.Ins.model.KHUYENMAI.Add(km);
                DataProvider.Ins.model.SaveChanges();

                ListCTKhuyenMai.Add(km);

                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                RefershControlsTCQL();
            });

            DeleteCommand = new RelayCommand<Object>((p) =>
            {
                if (String.IsNullOrEmpty(TenKM) || String.IsNullOrEmpty(TiLeKM.ToString()) || SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn chương trình khuyến mãi muốn xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                var km = DataProvider.Ins.model.KHUYENMAI.Where(x => x.MA_KM == SelectedItem.MA_KM);
                if (km != null && km.Count() != 0)
                    return true;

                MessageBox.Show("Chương trình khuyến mãi không tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }, (p) =>
            {
                using (var transactions = DataProvider.Ins.model.Database.BeginTransaction())
                {
                    try
                    {
                        var khuyenmai = DataProvider.Ins.model.KHUYENMAI.Where(x => x.MA_KM == SelectedItem.MA_KM).FirstOrDefault();
                        DataProvider.Ins.model.KHUYENMAI.Remove(khuyenmai);
                        DataProvider.Ins.model.SaveChanges();

                        transactions.Commit();
                        RemoveKhuyenMai(khuyenmai.MA_KM);
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefershControlsTCQL();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        transactions.Rollback();
                    }
                }
            });

            EditCommand = new RelayCommand<Object>((p) =>
            {
                if (String.IsNullOrEmpty(TenKM) || String.IsNullOrEmpty(TiLeKM.ToString()) || SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn chương trình khuyến mãi muốn sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                var km = DataProvider.Ins.model.KHUYENMAI.Where(x => x.MA_KM == SelectedItem.MA_KM);
                if (km != null && km.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                var km = DataProvider.Ins.model.KHUYENMAI.Where(x => x.MA_KM == SelectedItem.MA_KM).SingleOrDefault();
                km.TEN_KM = TenKM;
                km.NGAYBATDAU_KM = NgayBatDauKM;
                km.NGAYKETTHUC_KM = NgayKetThucKM;
                km.TILE_KM = TiLeKM;
                DataProvider.Ins.model.SaveChanges();

                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                RefershControlsTCQL();
            });

            RefreshCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                RefershControlsTCQL();
            });

            SortKhuyenMaiCommand = new RelayCommand<GridViewColumnHeader>((p) => { return p == null ? false : true; }, (p) =>
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListCTKhuyenMai);
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

        void RemoveKhuyenMai(int makm)
        {
            if (ListCTKhuyenMai == null || ListCTKhuyenMai.Count() == 0)
                return;
            foreach (KHUYENMAI item in ListCTKhuyenMai)
            {
                if (item.MA_KM == makm)
                {
                    ListCTKhuyenMai.Remove(item);
                    return;
                }
            }
        }

        void RefershControlsTCQL()
        {
            TenKM = null;
            NgayBatDauKM = null;
            NgayKetThucKM = null;
            TiLeKM = 0;
        }
    }
}
