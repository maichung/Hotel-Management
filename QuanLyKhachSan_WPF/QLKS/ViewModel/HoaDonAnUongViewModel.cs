using QLKS.Model;
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
    public class HoaDonAnUongViewModel : BaseViewModel
    {
        private int _MaPhong;
        public int MaPhong { get => _MaPhong; set { _MaPhong = value; OnPropertyChanged(); } }
        private string _LoaiPhucVu;
        public string LoaiPhucVu { get => _LoaiPhucVu; set { _LoaiPhucVu = value; OnPropertyChanged(); } }
        private long _TongTien;
        public long TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
        private ObservableCollection<ThongTinOrder> _ListOrder;
        public ObservableCollection<ThongTinOrder> ListOrder { get => _ListOrder; set { _ListOrder = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public HoaDonAnUongViewModel()
        {
            CancelCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => { p.Close(); });

            SaveCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                ////lấy thông tin phòng chọn thuê và nhân viên làm hóa đơn
                //var hoadonVM = p.DataContext as HoaDonViewModel;
                //MaPhong = hoadonVM.MaPhong;
                //LoaiPhucVu = hoadonVM.LoaiPhucVu;
                //TongTien = hoadonVM.TongTien;
                //ListOrder = hoadonVM.ListOrder;

                //DataProvider.Ins.model.SaveChanges();
                ////Tạo hóa đơn lưu trú
                //var hdau = new HOADONANUONG() { TRIGIA_HDAU = TongTien };
                //DataProvider.Ins.model.HOADONANUONG.Add(hdau);
                //DataProvider.Ins.model.SaveChanges();
                ////Tạo chi tiết hóa đơn lưu trú
                //foreach(ThongTinOrder item in ListOrder)
                //{
                //    var chitietHDAU = new CHITIET_HDAU() { MA_HDAU = hdau.MA_HDAU, MA_MH = item.MatHang.MA_MH, SOLUONG_MH = item.SoLuong};
                //    DataProvider.Ins.model.CHITIET_HDAU.Add(chitietHDAU);
                //}                
                //DataProvider.Ins.model.SaveChanges();
                ////Tạo hóa đơn tổng
                //var maHDLT = from cthdlt in DataProvider.Ins.model.CHITIET_HDLT
                //             join hdlt in DataProvider.Ins.model.HOADONLUUTRU
                //             on cthdlt.MA_HDLT equals hdlt.MA_HDLT
                //             where cthdlt.MA_PHONG == MaPhong && hdlt.TINHTRANG_HDLT == false
                //             select hdlt.MA_HDLT;
                //if (maHDLT == null)
                //    return;
                //int mahdlt = Int32.Parse(maHDLT.ToString());
                //var hd = DataProvider.Ins.model.HOADON.Where(x => x.MA_HDLT == mahdlt).SingleOrDefault();
                //hd.MA_HDAU = hdau.MA_HDAU;
            });
        }
    }
}
