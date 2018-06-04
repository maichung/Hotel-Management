using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    class HoaDonDiChuyenViewModel : BaseViewModel
    {
        private CHUYENDI _ChuyenDi;
        public CHUYENDI ChuyenDi { get => _ChuyenDi; set { _ChuyenDi = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public HoaDonDiChuyenViewModel()
        {
            CancelCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => { p.Close(); });

            SaveCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {

            });
        }
    }
}
