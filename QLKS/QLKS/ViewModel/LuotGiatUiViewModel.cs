using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    public class LuotGiatUiViewModel : BaseViewModel
    {
        private ObservableCollection<ThongTinGiatUi> _ListTTGiatUi;
        public ObservableCollection<ThongTinGiatUi> ListTTGiatUi { get => _ListTTGiatUi; set { _ListTTGiatUi = value; OnPropertyChanged(); } }

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
                    DonGia = (int)SelectedItem.DONGIA_LOAIGU;
                   
                }
            }
        }

        private string _TenLoaiGiatUi;
        public string TenLoaiGiatUi { get => _TenLoaiGiatUi; set { _TenLoaiGiatUi = value; OnPropertyChanged(); } }
        private int _DonGia;
        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }

        private int _CanNang;
        public int CanNang { get => _CanNang; set { _CanNang = value; OnPropertyChanged(); } }

        private DateTime _NgayBatDau;
        public DateTime NgayBatDau { get => _NgayBatDau; set { _NgayBatDau = value; OnPropertyChanged(); } }

        private DateTime _NgayKetThuc;
        public DateTime NgayKetThuc { get => _NgayKetThuc; set { _NgayKetThuc = value; OnPropertyChanged(); } }

        private int _ThanhTien;
        public int ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }


        public LuotGiatUiViewModel()
        {
            ListLoaiGiatUi=new ObservableCollection<LOAIGIATUI>(DataProvider.Ins.model.LOAIGIATUI);
            ThanhTien = CanNang * DonGia;
        }

       

    }
}
