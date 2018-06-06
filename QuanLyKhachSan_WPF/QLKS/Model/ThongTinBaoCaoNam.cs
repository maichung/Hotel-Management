using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QLKS.Model
{
    class ThongTinBaoCaoNam : INotifyPropertyChanged
    {
        private string _Thang = string.Empty;
        private int _DoanhThu = 0;

        public string Thang
        {
            get { return _Thang; }
            set
            {
                _Thang = value;
                NotifyPropertyChanged("Thang");
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
