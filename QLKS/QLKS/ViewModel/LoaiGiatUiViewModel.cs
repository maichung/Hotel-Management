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
    public class LoaiGiatUiViewModel : BaseViewModel
    {
        private ObservableCollection<LOAIGIATUI> _ListLoaiGiatUi;
        public ObservableCollection<LOAIGIATUI> ListLoaiGiatUi { get => _ListLoaiGiatUi; set { _ListLoaiGiatUi = value; OnPropertyChanged(); } }
        private LOAIGIATUI _SelectedItem;
        public LOAIGIATUI SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenLoaiGiatUi = SelectedItem.TEN_LOAIGU;
                    DonGiaLoaiGiatUi = (int)SelectedItem.DONGIA_LOAIGU;
                }
            }
        }

        private string _TenLoaiGiatUi;
        public string TenLoaiGiatUi { get=>_TenLoaiGiatUi; set { _TenLoaiGiatUi = value;OnPropertyChanged(); } }
        private int _DonGiaLoaiGiatUi;
        public int DonGiaLoaiGiatUi { get => _DonGiaLoaiGiatUi; set { _DonGiaLoaiGiatUi = value; OnPropertyChanged(); } }

        public LoaiGiatUiViewModel()
        {
            ListLoaiGiatUi = new ObservableCollection<LOAIGIATUI>(DataProvider.Ins.model.LOAIGIATUI);
        }
    }
}
