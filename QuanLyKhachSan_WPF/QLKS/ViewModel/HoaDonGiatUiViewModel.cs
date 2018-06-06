﻿using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    public class HoaDonGiatUiViewModel : BaseViewModel
    {
        private int _MaHD;
        public int MaHD { get => _MaHD; set { _MaHD = value; OnPropertyChanged(); } }
        private ThongTinGiatUi _TTGiatUi;
        public ThongTinGiatUi TTGiatUi { get => _TTGiatUi; set { _TTGiatUi = value; OnPropertyChanged(); } }
        private long _TongTien;
        public long TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public HoaDonGiatUiViewModel()
        {
            CancelCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => { p.Close(); });

            SaveCommand = new RelayCommand<Window>((p) => 
            {
                if (p == null)
                    return false;

                var hoadonVM = p.DataContext as HoaDonViewModel;
                if (hoadonVM.TTGiatUi == null)
                    return false;

                return true;
            }, (p) =>
            {
                //lấy thông tin phòng chọn thuê và nhân viên làm hóa đơn
                var hoadonVM = p.DataContext as HoaDonViewModel;
                MaHD = hoadonVM.MaHD;
                TTGiatUi = hoadonVM.TTGiatUi;
                TongTien = hoadonVM.TongTienHDGU;
                //Thêm lượt giặt ủi vào csdl
                DataProvider.Ins.model.LUOTGIATUI.Add(TTGiatUi.LuotGiatUi);
                DataProvider.Ins.model.SaveChanges();
                //Thêm chi tiết hóa đơn giặt ủi
                var chitietHDGU = new CHITIET_HDGU() { MA_HD = MaHD, MA_LUOTGU = TTGiatUi.LuotGiatUi.MA_LUOTGU, TRIGIA_CTHDGU = TongTien };
                DataProvider.Ins.model.CHITIET_HDGU.Add(chitietHDGU);
                DataProvider.Ins.model.SaveChanges();

                p.Close();
            });            
        }
    }
}
