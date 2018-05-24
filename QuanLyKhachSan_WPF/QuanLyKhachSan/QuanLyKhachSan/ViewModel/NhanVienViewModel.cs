using QuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKhachSan.ViewModel
{
    public class NhanVienViewModel : BaseViewModel
    {
        private ObservableCollection<ThongTinNhanVien> _ListTTNhanVien;
        public ObservableCollection<ThongTinNhanVien> ListTTNhanVien { get => _ListTTNhanVien; set { _ListTTNhanVien = value; OnPropertyChanged(); } }
        private ObservableCollection<NHANVIEN> _ListNhanVien;
        public ObservableCollection<NHANVIEN> ListNhanVien { get => _ListNhanVien; set { _ListNhanVien = value; OnPropertyChanged(); } }
        private ThongTinNhanVien _SelectedItem;
        public ThongTinNhanVien SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenDangNhap = SelectedItem.TaiKhoan.TENDANGNHAP_TK;
                    TenNhanVien = SelectedItem.NhanVien.HOTEN_NV;
                    GioiTinh = (bool)SelectedItem.NhanVien.GIOITINH_NV? "Nam" : "Nữ";
                    NgaySinh = SelectedItem.NhanVien.NGAYSINH_NV;
                    SoDienThoai = SelectedItem.NhanVien.SODIENTHOAI_NV;
                    SelectedNhanVien = SelectedItem.NhanVien;
                    DiaChi = SelectedItem.NhanVien.DIACHI_NV;
                    NgayVaoLam = SelectedItem.NhanVien.NGAYVAOLAM_NV;
                }
            }
        }

        #region
        private string _TenDangNhap;
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }
        private string _MatKhau;
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }
        private string _TenNhanVien;
        public string TenNhanVien { get => _TenNhanVien; set { _TenNhanVien = value; OnPropertyChanged(); } }
        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        private DateTime? _NgaySinh;
        public DateTime? NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }
        private string _SoDienThoai;
        public string SoDienThoai { get => _SoDienThoai; set { _SoDienThoai = value; OnPropertyChanged(); } }
        private NHANVIEN _SelectedNhanVien;
        public NHANVIEN SelectedNhanVien { get => _SelectedNhanVien; set { _SelectedNhanVien = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private DateTime? _NgayVaoLam;
        public DateTime? NgayVaoLam { get => _NgayVaoLam; set { _NgayVaoLam = value; OnPropertyChanged(); } }
        #endregion
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public NhanVienViewModel()
        {
            LoadTTNhanVien();
            ListNhanVien = new ObservableCollection<NHANVIEN>(DataProvider.Ins.model.NHANVIENs);

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return p == null ? false : true; }, (p) => { MatKhau = p.Password; });

            AddCommand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(TenDangNhap) || string.IsNullOrEmpty(MatKhau) || string.IsNullOrEmpty(TenNhanVien) || SelectedNhanVien == null)
                    return false;

                var listTenDangNhap = DataProvider.Ins.model.TAIKHOANs.Where(x => x.TENDANGNHAP_TK == TenDangNhap);
                if (listTenDangNhap == null || listTenDangNhap.Count() != 0)
                    return false;

                return true;
            }, (p) =>
            {
                string matKhauMaHoa = MD5Hash(Base64Encode(MatKhau));

                var taiKhoan = new TAIKHOAN() { TENDANGNHAP_TK = TenDangNhap, MATKHAU_TK = matKhauMaHoa };
                DataProvider.Ins.model.TAIKHOANs.Add(taiKhoan);
                DataProvider.Ins.model.SaveChanges();

                var taiKhoanMoi = DataProvider.Ins.model.TAIKHOANs.Where(x => x.TENDANGNHAP_TK == TenDangNhap).SingleOrDefault();
                var nhanVien = new NHANVIEN() { MA_TK = taiKhoanMoi.MA_TK, HOTEN_NV = TenNhanVien, GIOITINH_NV = ConvertGioiTinh(GioiTinh),
                    NGAYSINH_NV = NgaySinh, SODIENTHOAI_NV = SoDienThoai, CHUCVU_NV = SelectedNhanVien.CHUCVU_NV, DIACHI_NV = DiaChi, NGAYVAOLAM_NV = NgayVaoLam };
                DataProvider.Ins.model.NHANVIENs.Add(nhanVien);
                DataProvider.Ins.model.SaveChanges();


                ListTTNhanVien.Add(new ThongTinNhanVien() { TaiKhoan = taiKhoan, NhanVien = nhanVien });
            });

            EditCommand = new RelayCommand<Object>((p) =>
            {
                if (string.IsNullOrEmpty(TenDangNhap) || string.IsNullOrEmpty(MatKhau) || string.IsNullOrEmpty(TenNhanVien) || 
                    SelectedNhanVien == null || SelectedItem == null)
                    return false;

                var listTaiKhoan = DataProvider.Ins.model.TAIKHOANs.Where(x => x.TENDANGNHAP_TK == TenDangNhap);
                var listTTNV = DataProvider.Ins.model.TAIKHOANs.Where(x => x.MA_TK == SelectedItem.NhanVien.MA_TK);
                if (listTaiKhoan != null && listTaiKhoan.Count() != 0 && listTTNV != null && listTTNV.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                string matKhauMaHoa = MD5Hash(Base64Encode(MatKhau));
                var taiKhoan = DataProvider.Ins.model.TAIKHOANs.Where(x => x.MA_TK == SelectedItem.TaiKhoan.MA_TK).SingleOrDefault();
                taiKhoan.MATKHAU_TK = matKhauMaHoa;
                var nhanVien = DataProvider.Ins.model.NHANVIENs.Where(x => x.MA_TK == SelectedItem.NhanVien.MA_TK).SingleOrDefault();
                nhanVien.HOTEN_NV = TenNhanVien;
                nhanVien.GIOITINH_NV = ConvertGioiTinh(GioiTinh);
                nhanVien.NGAYSINH_NV = NgaySinh;
                nhanVien.SODIENTHOAI_NV = SoDienThoai;
                nhanVien.CHUCVU_NV = SelectedNhanVien.CHUCVU_NV;
                nhanVien.DIACHI_NV = DiaChi;
                nhanVien.NGAYVAOLAM_NV = NgayVaoLam;
                DataProvider.Ins.model.SaveChanges();
            });
        }

        void LoadTTNhanVien()
        {
            ListTTNhanVien = new ObservableCollection<ThongTinNhanVien>();

            var listTTNhanVien = from tk in DataProvider.Ins.model.TAIKHOANs
                                 join nv in DataProvider.Ins.model.NHANVIENs
                                 on tk.MA_TK equals nv.MA_TK
                                 select new ThongTinNhanVien()
                                 {
                                     TaiKhoan = tk,
                                     NhanVien = nv
                                 };
            foreach(ThongTinNhanVien item in listTTNhanVien)
            {
                ListTTNhanVien.Add(item);
            }
        }

        bool ConvertGioiTinh(string gt)
        {
            if (gt == "Nam") return true;
            else return false;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
