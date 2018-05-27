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

namespace QLKS.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private ObservableCollection<ThongTinPhong> _ListTTPhong;
        public ObservableCollection<ThongTinPhong> ListTTPhong { get => _ListTTPhong; set { _ListTTPhong = value; OnPropertyChanged(); } }

        public ICommand LoadedWindowCommand { get; set; }
        

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
              {
                  p.Show();
                  LoadTTPhog();
              });            
        }

        public void LoadTTPhog()
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
            foreach(ThongTinPhong item in listTTPhong)
            {
                ListTTPhong.Add(item);
            }
            
        }
    }
}
