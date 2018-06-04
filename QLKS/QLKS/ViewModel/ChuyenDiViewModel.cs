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
    class ChuyenDiViewModel : BaseViewModel
    {
        private ObservableCollection<CHUYENDI> _ListChuyenDi;
        public ObservableCollection<CHUYENDI> ListChuyenDi { get => _ListChuyenDi; set { _ListChuyenDi = value; OnPropertyChanged(); } }

        private CHUYENDI _SelectedItem;
        public CHUYENDI SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DonGia = (int)SelectedItem.DONGIA_CD;
                    DiemDen = SelectedItem.DIEMDEN_CD;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        private string _DiemDen;
        public string DiemDen { get => _DiemDen; set { _DiemDen = value; OnPropertyChanged(); } }

        private int _DonGia;
        public int DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }

        public ChuyenDiViewModel()
        {
            ListChuyenDi = new ObservableCollection<CHUYENDI>(DataProvider.Ins.model.CHUYENDI);
            AddCommand = new RelayCommand<Object>((p) =>
              {
                  if (String.IsNullOrEmpty(DiemDen) || String.IsNullOrEmpty(DonGia.ToString()))
                  {
                      return false;
                  }
                  var cd = DataProvider.Ins.model.CHUYENDI.Where(x => x.DIEMDEN_CD == DiemDen);
                  if (cd == null || cd.Count() > 0)
                  {
                      return false;
                  }
                  return true;
              },
            (p) =>
            {
                CHUYENDI cd = new CHUYENDI() { DIEMDEN_CD = DiemDen, DONGIA_CD = DonGia };

                DataProvider.Ins.model.CHUYENDI.Add(cd);
                DataProvider.Ins.model.SaveChanges();
            });

            RefreshCommand = new RelayCommand<Object>((p) =>
            {

                return true;
            },
            (p) =>
            {
                DiemDen = null;
                DonGia = 0;
            });

            EditCommand = new RelayCommand<Object>((p) =>
            {
                if (String.IsNullOrEmpty(DiemDen) || String.IsNullOrEmpty(DonGia.ToString()) || SelectedItem == null)
                {
                    return false;
                }
                var cd = DataProvider.Ins.model.CHUYENDI.Where(x => x.DIEMDEN_CD == DiemDen);
                if(cd!=null && cd.Count() != 0)
                {
                    return true;
                }
                return false;
            },
            (p) =>
            {
                var cd = DataProvider.Ins.model.CHUYENDI.Where(x => x.DIEMDEN_CD == DiemDen).SingleOrDefault();
                cd.DIEMDEN_CD = DiemDen;
                cd.DONGIA_CD = DonGia;
                DataProvider.Ins.model.SaveChanges();
            });
        }
    }
}
