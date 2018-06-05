﻿using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    public class HoaDonLuuTruViewModel : BaseViewModel
    {
        private ThongTinPhong _ThongTinPhongChonThue;
        public ThongTinPhong ThongTinPhongChonThue { get => _ThongTinPhongChonThue; set { _ThongTinPhongChonThue = value; OnPropertyChanged(); } }
        private NHANVIEN _NhanVienLapHD;
        public NHANVIEN NhanVienLapHD { get => _NhanVienLapHD; set { _NhanVienLapHD = value; OnPropertyChanged(); } }
        private KHACHHANG _KhachHangThue;
        public KHACHHANG KhachHangThue { get => _KhachHangThue; set { _KhachHangThue = value; OnPropertyChanged(); } }
        
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand LoadKhachHangCommand { get; set; }

        public HoaDonLuuTruViewModel()
        {
            KhachHangThue = new KHACHHANG();

            LoadKhachHangCommand = new RelayCommand<Object>((p) => { return string.IsNullOrEmpty(KhachHangThue.CMND_KH) ? false : true; }, (p) =>
            {
                var kh = DataProvider.Ins.model.KHACHHANG.Where(x => x.CMND_KH == KhachHangThue.CMND_KH).SingleOrDefault();
                if (kh == null)
                {
                    KhachHangThue.HOTEN_KH = "";
                    KhachHangThue.SODIENTHOAI_KH = "";
                }
                else
                {
                    KhachHangThue.HOTEN_KH = kh.HOTEN_KH;
                    KhachHangThue.SODIENTHOAI_KH = kh.SODIENTHOAI_KH;
                }
            });

            SaveCommand = new RelayCommand<Window>((p) => 
            {
                if (p == null || p.DataContext == null)
                    return false;

                var hoadonVM = p.DataContext as HoaDonViewModel;
                if (hoadonVM.MaPhong == 0)
                    return false;

                if (string.IsNullOrEmpty(KhachHangThue.HOTEN_KH) || string.IsNullOrEmpty(KhachHangThue.CMND_KH))
                    return false;

                return true;
            }, (p) =>
            {
                //kiểm tra xem khách hàng đã có trong csdl của khách sạn hay chưa
                var khachHang = DataProvider.Ins.model.KHACHHANG.Where(x => x.CMND_KH == KhachHangThue.CMND_KH).SingleOrDefault();
                if (khachHang == null)
                {
                    KHACHHANG newKhachHang = new KHACHHANG() { HOTEN_KH = KhachHangThue.HOTEN_KH, SODIENTHOAI_KH = KhachHangThue.SODIENTHOAI_KH, CMND_KH = KhachHangThue.CMND_KH };
                    DataProvider.Ins.model.KHACHHANG.Add(newKhachHang);
                    DataProvider.Ins.model.SaveChanges();
                    khachHang = DataProvider.Ins.model.KHACHHANG.Where(x => x.CMND_KH == KhachHangThue.CMND_KH).SingleOrDefault();
                }
                //lấy thông tin phòng chọn thuê và nhân viên làm hóa đơn
                var hoadonVM = p.DataContext as HoaDonViewModel;
                ThongTinPhongChonThue = hoadonVM.ThongTinPhongChonThue;
                NhanVienLapHD = hoadonVM.NhanVienLapHD;
                //Tạo hóa đơn tổng
                var hd = new HOADON() { MA_NV = NhanVienLapHD.MA_NV, MA_KH = khachHang.MA_KH, THOIGIANLAP_HD = DateTime.Now, TINHTRANG_HD = false };
                DataProvider.Ins.model.HOADON.Add(hd);
                DataProvider.Ins.model.SaveChanges();
                //Tạo chi tiết hóa đơn lưu trú
                var chitietHDLT = new CHITIET_HDLT() { MA_HD = hd.MA_HD, MA_PHONG = ThongTinPhongChonThue.Phong.MA_PHONG, THOIGIANNHAN_PHONG = DateTime.Now };
                DataProvider.Ins.model.CHITIET_HDLT.Add(chitietHDLT);
                DataProvider.Ins.model.SaveChanges();                
                //Đổi trạng thái của phòng
                var phong = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == ThongTinPhongChonThue.Phong.MA_PHONG).SingleOrDefault();
                phong.TINHTRANG_PHONG = "Đang thuê";
                DataProvider.Ins.model.SaveChanges();

                p.Close();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => p.Close());
        }
    }
}