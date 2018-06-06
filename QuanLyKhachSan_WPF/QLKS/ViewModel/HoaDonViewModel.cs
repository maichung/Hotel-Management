using QLKS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    public class HoaDonViewModel : BaseViewModel
    {
        public enum LoaiHoaDon
        {
            HoaDonTong, HoaDonLuuTru, HoaDonAnUong, HoaDonGiatUi, HoaDonDiChuyen
        };

        private int _LoaiHD;
        public int LoaiHD { get => _LoaiHD; set { _LoaiHD = value; OnPropertyChanged(); } }
        private DateTime _ThoiGianLapHD;
        public DateTime ThoiGianLapHD { get => _ThoiGianLapHD; set { _ThoiGianLapHD = value; OnPropertyChanged(); } }

        //Truyền thông tin qua những hd dịch vụ
        private int _MaHD;
        public int MaHD { get => _MaHD; set { _MaHD = value; OnPropertyChanged(); } }
        private int _MaPhong;
        public int MaPhong { get => _MaPhong; set { _MaPhong = value; OnPropertyChanged(); } }
        private KHACHHANG _KhachHangThue;
        public KHACHHANG KhachHangThue { get => _KhachHangThue; set { _KhachHangThue = value; OnPropertyChanged(); } }
        private NHANVIEN _NhanVienLapHD;
        public NHANVIEN NhanVienLapHD { get => _NhanVienLapHD; set { _NhanVienLapHD = value; OnPropertyChanged(); } }
        private HOADON _HoaDon;
        public HOADON HoaDon { get => _HoaDon; set { _HoaDon = value; OnPropertyChanged(); } }
        //Show hóa đơn tổng
        private ObservableCollection<ThongTinHoaDon> _ListThongTinHD;
        public ObservableCollection<ThongTinHoaDon> ListThongTinHD { get => _ListThongTinHD; set { _ListThongTinHD = value; OnPropertyChanged(); } }
        private ThongTinHoaDon _ThongTinHD;
        public ThongTinHoaDon ThongTinHD { get => _ThongTinHD; set { _ThongTinHD = value; OnPropertyChanged(); } }
        private long _TongTienHD;
        public long TongTienHD { get => _TongTienHD; set { _TongTienHD = value; OnPropertyChanged(); } }

        //Truyền thông tin qua hd lưu trú
        private ThongTinPhong _ThongTinPhongChonThue;
        public ThongTinPhong ThongTinPhongChonThue { get => _ThongTinPhongChonThue; set { _ThongTinPhongChonThue = value; OnPropertyChanged(); } }

        //Truyền thông tin qua hd ăn uống
        private string _LoaiPhucVu;
        public string LoaiPhucVu { get => _LoaiPhucVu; set { _LoaiPhucVu = value; OnPropertyChanged(); } }
        private ObservableCollection<ThongTinOrder> _ListOrder;
        public ObservableCollection<ThongTinOrder> ListOrder { get => _ListOrder; set { _ListOrder = value; OnPropertyChanged(); } }
        private long _TongTienHDAU;
        public long TongTienHDAU { get => _TongTienHDAU; set { _TongTienHDAU = value; OnPropertyChanged(); } }

        //Truyền thông tin qua hd giặt ủi
        private ThongTinGiatUi _TTGiatUi;
        public ThongTinGiatUi TTGiatUi { get => _TTGiatUi; set { _TTGiatUi = value; OnPropertyChanged(); } }
        private long _TongTienHDGU;
        public long TongTienHDGU { get => _TongTienHDGU; set { _TongTienHDGU = value; OnPropertyChanged(); } }

        //Truyền thông tin qua hd di chuyển
        private CHUYENDI _ChuyenDi;
        public CHUYENDI ChuyenDi { get => _ChuyenDi; set { _ChuyenDi = value; OnPropertyChanged(); } }
        private long _TongTienHDDC;
        public long TongTienHDDC { get => _TongTienHDDC; set { _TongTienHDDC = value; OnPropertyChanged(); } }

        public ICommand btnHDTongCommand { get; set; }
        public ICommand btnHDLuuTruCommand { get; set; }
        public ICommand btnHDAnUongCommand { get; set; }
        public ICommand btnHDGiatUiCommand { get; set; }
        public ICommand btnHDDiChuyenCommand { get; set; }

        public ICommand LoadHoaDonTongCommand { get; set; }
        public ICommand PayCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ClosedWindowCommand { get; set; }

        public HoaDonViewModel()
        {
            ThoiGianLapHD = DateTime.Now;
            ListThongTinHD = new ObservableCollection<ThongTinHoaDon>();
            ThongTinHD = new ThongTinHoaDon();
            TongTienHD = 0;

            btnHDTongCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonTong);
            btnHDLuuTruCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonLuuTru);
            btnHDAnUongCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonAnUong);
            btnHDGiatUiCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonGiatUi);
            btnHDDiChuyenCommand = new RelayCommand<Object>((p) => { return true; }, (p) => LoaiHD = (int)LoaiHoaDon.HoaDonDiChuyen);

            LoadHoaDonTongCommand = new RelayCommand<Object>((p) => 
            {
                if (ListThongTinHD.Count() != 0)
                    return false;

                return true;
            }, (p) =>
            {
                //lấy hóa đơn lưu trú
                var hdlt = DataProvider.Ins.model.CHITIET_HDLT.Where(x => x.MA_HD == HoaDon.MA_HD).SingleOrDefault();
                DateTime tgTraPhong = DateTime.Now;
                TimeSpan timehdlt = tgTraPhong.Subtract((DateTime)hdlt.THOIGIANNHAN_PHONG);
                ThongTinHD.LoaiHoaDon = "Hóa đơn lưu trú";
                ThongTinHD.NoiDungHD = "Phòng " + hdlt.MA_PHONG + "\nThời gian nhận phòng " + hdlt.THOIGIANNHAN_PHONG + "\nThời gian trả phòng " + tgTraPhong;
                ThongTinHD.DonGia = (int)ThongTinPhongChonThue.LoaiPhong.DONGIA_LP;
                ThongTinHD.TriGia = (int)ThongTinPhongChonThue.LoaiPhong.DONGIA_LP * (int)(timehdlt.TotalDays + 1);
                ListThongTinHD.Add(ThongTinHD);
                //lấy hóa đơn ăn uống
                var listHDAU = DataProvider.Ins.model.CHITIET_HDAU.Where(x => x.MA_HD == HoaDon.MA_HD).ToList();
                foreach (CHITIET_HDAU item in listHDAU)
                {
                    ThongTinHD = new ThongTinHoaDon();
                    var mathang = DataProvider.Ins.model.MATHANG.Where(x => x.MA_MH == item.MA_MH).SingleOrDefault();
                    ThongTinHD.LoaiHoaDon = "Hóa đơn ăn uống";
                    ThongTinHD.NoiDungHD = mathang.TEN_MH + " - SL " + item.SOLUONG_MH;
                    ThongTinHD.DonGia = (int)mathang.DONGIA_MH;
                    ThongTinHD.TriGia = (int)item.TRIGIA_CTHDAU;
                    ListThongTinHD.Add(ThongTinHD);
                }
                //lấy hóa đơn giặt ủi
                var listHDGU = DataProvider.Ins.model.CHITIET_HDGU.Where(x => x.MA_HD == HoaDon.MA_HD).ToList();
                foreach (CHITIET_HDGU item in listHDGU)
                {
                    ThongTinHD = new ThongTinHoaDon();
                    var luotgu = DataProvider.Ins.model.LUOTGIATUI.Where(x => x.MA_LUOTGU == item.MA_LUOTGU).SingleOrDefault();
                    var loaigu = DataProvider.Ins.model.LOAIGIATUI.Where(x => x.MA_LOAIGU == luotgu.MA_LOAIGU).SingleOrDefault();
                    ThongTinHD.LoaiHoaDon = "Hóa đơn giặt ủi";
                    if(loaigu.MA_LOAIGU == 1)
                    {
                        ThongTinHD.NoiDungHD = loaigu.TEN_LOAIGU + " - SL " + luotgu.SOKILOGRAM_LUOTGU + " Kg";
                        ThongTinHD.DonGia = (int)loaigu.DONGIA_LOAIGU;
                    }else if (loaigu.MA_LOAIGU == 2)
                    {
                        DateTime ngaykt = (DateTime)luotgu.NGAYKETTHUC_LUOTGU;
                        TimeSpan timehdgu = ngaykt.Subtract((DateTime)luotgu.NGAYBATDAU_LUOTGU);
                        ThongTinHD.NoiDungHD = loaigu.TEN_LOAIGU + " - SL " + (int)(timehdgu.TotalDays + 1) + " Ngày";
                        ThongTinHD.DonGia = (int)loaigu.DONGIA_LOAIGU;
                    }
                    ThongTinHD.TriGia = (int)item.TRIGIA_CTHDGU;
                    ListThongTinHD.Add(ThongTinHD);
                }
                //lấy hóa đơn di chuyển
                var listHDDC = DataProvider.Ins.model.CHITIET_HDDC.Where(x => x.MA_HD == HoaDon.MA_HD).ToList();
                foreach (CHITIET_HDDC item in listHDDC)
                {
                    ThongTinHD = new ThongTinHoaDon();
                    var chuyendi = DataProvider.Ins.model.CHUYENDI.Where(x => x.MA_CD == item.MA_CD).SingleOrDefault();
                    ThongTinHD.LoaiHoaDon = "Hóa đơn di chuyển";
                    ThongTinHD.NoiDungHD = chuyendi.DIEMDEN_CD;
                    ThongTinHD.DonGia = (int)chuyendi.DONGIA_CD;
                    ThongTinHD.TriGia = (int)item.TRIGIA_CTHDDC;
                    ListThongTinHD.Add(ThongTinHD);
                }

                foreach (var item in ListThongTinHD)
                {
                    TongTienHD += item.TriGia;
                }

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListThongTinHD);
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(new PropertyGroupDescription("LoaiHoaDon"));
            });

            PayCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {

            });

            CancelCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => { p.Close(); });

            ClosedWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => 
            {
                //refersh hd tổng
                ListThongTinHD.Clear();
                TongTienHD = 0;
                //refersh hd lưu trú
                ThongTinPhongChonThue = null;
                //refersh hd ăn uống
                LoaiPhucVu = null;
                ListOrder = null;
                TongTienHDAU = 0;
                //refersh hd giặt ủi
                TTGiatUi = null;
                TongTienHDGU = 0;
                //refersh hd di chuyển
                ChuyenDi = null;
                TongTienHDDC = 0;
            });
        }

        public void GetThongTinPhongThue(int maphong)
        {
            var phongChonThue = DataProvider.Ins.model.PHONG.Where(x => x.MA_PHONG == maphong).SingleOrDefault();
            var loaiPhongChonThue = DataProvider.Ins.model.LOAIPHONG.Where(x => x.MA_LP == phongChonThue.MA_LP).SingleOrDefault();
            ThongTinPhongChonThue = new ThongTinPhong() { Phong = phongChonThue, LoaiPhong = loaiPhongChonThue };
        }

        public HOADON GetHoaDon(int maphong)
        {
            var cthdlt = DataProvider.Ins.model.CHITIET_HDLT.Where(x => x.MA_PHONG == maphong).ToList();
            foreach (var item in cthdlt)
            {
                var hd = DataProvider.Ins.model.HOADON.Where(x => x.MA_HD == item.MA_HD && x.TINHTRANG_HD == false).SingleOrDefault();
                if (hd != null)
                {
                    MaHD = hd.MA_HD;
                    MaPhong = maphong;
                    return hd;
                }
            }
            return null;
        }

        public NHANVIEN GetNhanVien(HOADON hoadon)
        {
            if(hoadon == null)
                return null;
            var nv = DataProvider.Ins.model.NHANVIEN.Where(x => x.MA_NV == hoadon.MA_NV).SingleOrDefault();
            if (nv != null)
                return nv;
            return null;
        }

        public KHACHHANG GetKhachHang(HOADON hoadon)
        {
            if (hoadon == null)
                return null;
            var kh = DataProvider.Ins.model.KHACHHANG.Where(x => x.MA_KH == hoadon.MA_KH).SingleOrDefault();
            if (kh != null)
                return kh;
            return null;
        }
    }
}
