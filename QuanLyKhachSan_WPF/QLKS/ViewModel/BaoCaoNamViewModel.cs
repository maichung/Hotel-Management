using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    class BaoCaoNamViewModel:BaseViewModel
    {
        private ObservableCollection<ThongTinBaoCaoNam> _ListThang;
        public ObservableCollection<ThongTinBaoCaoNam> ListThang { get => _ListThang; set { _ListThang = value; OnPropertyChanged(); } }

        private int _TongDoanhThu;
        public int TongDoanhThu { get => _TongDoanhThu; set { _TongDoanhThu = value; OnPropertyChanged(); } }
        private int _Thang;
        public int Thang { get => _Thang; set { _Thang = value; OnPropertyChanged(); } }

        public ICommand ShowCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public BaoCaoNamViewModel()
        {
            ShowCommand = new RelayCommand<Object>((p) =>
              {
                  return true;
              },(p)=> 
              {
              });
        }
    }
}
