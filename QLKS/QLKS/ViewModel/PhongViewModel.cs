using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QLKS.Model;

namespace QLKS.ViewModel
{
    public class PhongViewModel : BaseViewModel
    {
        //Datacontext
        private ObservableCollection<ThongTinPhong> _ListTTPhong;
        public ObservableCollection<ThongTinPhong> ListTTPhong { get => _ListTTPhong; set { _ListTTPhong = value; OnPropertyChanged(); } }
        //ItemsSource combobox
        private ObservableCollection<LOAIPHONG> _ListLoaiPhong;
        public ObservableCollection<LOAIPHONG> ListLoaiPhong { get => _ListLoaiPhong; set { _ListLoaiPhong = value; OnPropertyChanged(); } }
        private ObservableCollection<string> _ListTinhTrangPhong;
        public ObservableCollection<string> ListTinhTrangPhong { get => _ListTinhTrangPhong; set { _ListTinhTrangPhong = value; OnPropertyChanged(); } }

        private ThongTinPhong _SelectedItem;
        public ThongTinPhong SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaPhong = SelectedItem.Phong.MA_PHONG;
                    SelectedLoaiPhong = SelectedItem.LoaiPhong;
                    SelectedTinhTrangPhong = SelectedItem.Phong.TINHTRANG_PHONG;
                }
            }
        }
        private int _MaPhong;
        public int MaPhong { get => _MaPhong; set { _MaPhong = value; OnPropertyChanged(); } }
        private LOAIPHONG _SelectedLoaiPhong;
        public LOAIPHONG SelectedLoaiPhong { get => _SelectedLoaiPhong; set { _SelectedLoaiPhong = value; OnPropertyChanged(); } }
        private string _SelectedTinhTrangPhong;
        public string SelectedTinhTrangPhong { get => _SelectedTinhTrangPhong; set { _SelectedTinhTrangPhong = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public PhongViewModel()
        {
            LoadTTPhong();
            ListLoaiPhong = new ObservableCollection<LOAIPHONG>(DataProvider.Ins.model.LOAIPHONG);
            string[] tinhtrangphongs = new string[] { "Trống", "Đang thuê", "Đã đặt trước" };
            ListTinhTrangPhong = new ObservableCollection<string>(tinhtrangphongs);

            AddCommand = new RelayCommand<Object>((p) => {
                if (string.IsNullOrEmpty(MaPhong.ToString()) || SelectedLoaiPhong == null || SelectedTinhTrangPhong == null)
                    return false;

                var listPhong = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == MaPhong);
                if (listPhong == null || listPhong.Count() != 0)
                    return false;

                return true;
            }, (p) => {                
                var phong = new PHONG() { MA_PHONG = MaPhong, MA_LP = SelectedLoaiPhong.MA_LP, TINHTRANG_PHONG = SelectedTinhTrangPhong };

                DataProvider.Ins.model.PHONG.Add(phong);
                DataProvider.Ins.model.SaveChanges();
                //lấy loại phòng từ phòng vừa thêm vào và tạo ra thongtinphong sau đó thêm vào list
                var loaiPhong = DataProvider.Ins.model.LOAIPHONG.Where(x => x.MA_LP == phong.MA_LP).SingleOrDefault();
                ListTTPhong.Add(new ThongTinPhong() { Phong = phong, LoaiPhong = loaiPhong });
            });

            EditCommand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(MaPhong.ToString()) || SelectedItem == null || 
                    SelectedLoaiPhong == null || SelectedTinhTrangPhong == null)
                    return false;

                var listPhong = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == MaPhong);
                if (listPhong != null && listPhong.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                var phong = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == SelectedItem.Phong.MA_PHONG).SingleOrDefault();
                phong.MA_LP = SelectedLoaiPhong.MA_LP;
                phong.TINHTRANG_PHONG = SelectedTinhTrangPhong;
                SelectedItem.LoaiPhong = DataProvider.Ins.model.LOAIPHONG.Where(x => x.MA_LP == SelectedLoaiPhong.MA_LP).SingleOrDefault();
                DataProvider.Ins.model.SaveChanges();
            });
        }

        public void LoadTTPhong()
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
        }
    }
}
