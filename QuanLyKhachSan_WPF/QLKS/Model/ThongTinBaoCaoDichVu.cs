using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QLKS.Model
{
    class ThongTinBaoCaoDichVu : INotifyPropertyChanged
    {
        private string _TenDichVu = string.Empty;
        private int _DoanhThu = 0;

        public string TenDichVu
        {
            get { return _TenDichVu; }
            set
            {
                _TenDichVu = value;
                NotifyPropertyChanged("TenDichVu");
            }
        }

        public int DoanhThu
        {
            get { return _DoanhThu; }
            set
            {
                _DoanhThu = value;
                NotifyPropertyChanged("DoanhThu");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
