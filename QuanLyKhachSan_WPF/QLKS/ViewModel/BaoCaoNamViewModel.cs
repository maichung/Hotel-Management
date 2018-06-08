using QLKS.Model;
using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLKS.ViewModel
{
    class BaoCaoNamViewModel : BaseViewModel
    {
        private ObservableCollection<ThongTinBaoCao> _ListDoanhThuThang;
        public ObservableCollection<ThongTinBaoCao> ListDoanhThuThang { get => _ListDoanhThuThang; set { _ListDoanhThuThang = value; OnPropertyChanged(); } }
        private NHANVIEN _NhanVien;
        public NHANVIEN NhanVien { get => _NhanVien; set { _NhanVien = value; OnPropertyChanged(); } }

        private int _Nam;
        public int Nam { get => _Nam; set { _Nam = value; OnPropertyChanged(); } }
        private int _TongDoanhThu;
        public int TongDoanhThu { get => _TongDoanhThu; set { _TongDoanhThu = value; OnPropertyChanged(); } }
        private string _TieuDeBieuDo;
        public string TieuDeBieuDo { get => _TieuDeBieuDo; set { _TieuDeBieuDo = value; OnPropertyChanged(); } }
        private ObservableCollection<int> _ListThang;
        public ObservableCollection<int> ListThang { get => _ListThang; set { _ListThang = value; OnPropertyChanged(); } }

        public ICommand ShowCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand LoadReportCommand { get; set; }

        public BaoCaoNamViewModel()
        {
            int[] thangs = new int[12];
            ListThang = new ObservableCollection<int>(thangs);
            Nam = DateTime.Now.Year;

            ShowCommand = new RelayCommand<Object>((p) =>
              {
                  if (string.IsNullOrEmpty(Nam.ToString()))
                  {
                      MessageBox.Show("Vui lòng nhập năm cần lập báo cáo!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                      return false;
                  }
                  return true;
              }, (p) =>
               {
                   TongDoanhThu = 0;
                   TieuDeBieuDo = string.Empty;
                   ListDoanhThuThang = null;

                   var tong = (from hd in DataProvider.Ins.model.HOADON
                               where (hd.THOIGIANLAP_HD.Value.Year == Nam) && (hd.TINHTRANG_HD == true)
                               select hd.TRIGIA_HD).Sum();
                   if (tong == null)
                   {
                       ListDoanhThuThang = null;
                       MessageBox.Show("Không có báo cáo trong năm đã chọn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                       return;
                   }
                   TongDoanhThu = (int)tong;
                   TieuDeBieuDo = "Tổng doanh thu: " + TongDoanhThu.ToString("N0");
                   ListDoanhThuThang = new ObservableCollection<ThongTinBaoCao>();

                   for (int i = 1; i <= ListThang.Count(); i++)
                   {
                       var thang = (from hd in DataProvider.Ins.model.HOADON
                                    where (hd.THOIGIANLAP_HD.Value.Year == Nam) && (hd.THOIGIANLAP_HD.Value.Month == i) && (hd.TINHTRANG_HD == true)
                                    select hd.TRIGIA_HD).Sum();
                       if (thang == null)
                       {
                           ListThang[i - 1] = 0;
                       }
                       else
                       {
                           ListThang[i - 1] = (int)thang;
                       }
                       ListDoanhThuThang.Add(new ThongTinBaoCao() { Item = "Tháng " + i, DoanhThu = ListThang[i - 1], TiLe=(double)ListThang[i - 1]/TongDoanhThu });
                   }
               });

            SaveCommand = new RelayCommand<Object>((p) =>
              {
                  if (ListDoanhThuThang != null)
                      return true;
                  MessageBox.Show("Không có báo cáo, vui lòng xuất báo cáo trước khi lưu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                  return false;
              }, (p) =>
              {
                  var baocao = new BAOCAONAM();
                  baocao.TONGDOANHTHU_BCN = TongDoanhThu;
                  baocao.THOIGIANLAP_BCN = DateTime.Now;
                  baocao.NAM_BCN = Nam;
                  baocao.DOANHTHUTHANG1_BCN = ListDoanhThuThang[0].DoanhThu;
                  baocao.DOANHTHUTHANG2_BCN = ListDoanhThuThang[1].DoanhThu;
                  baocao.DOANHTHUTHANG3_BCN = ListDoanhThuThang[2].DoanhThu;
                  baocao.DOANHTHUTHANG4_BCN = ListDoanhThuThang[3].DoanhThu;
                  baocao.DOANHTHUTHANG5_BCN = ListDoanhThuThang[4].DoanhThu;
                  baocao.DOANHTHUTHANG6_BCN = ListDoanhThuThang[5].DoanhThu;
                  baocao.DOANHTHUTHANG7_BCN = ListDoanhThuThang[6].DoanhThu;
                  baocao.DOANHTHUTHANG8_BCN = ListDoanhThuThang[7].DoanhThu;
                  baocao.DOANHTHUTHANG9_BCN = ListDoanhThuThang[8].DoanhThu;
                  baocao.DOANHTHUTHANG10_BCN = ListDoanhThuThang[9].DoanhThu;
                  baocao.DOANHTHUTHANG11_BCN = ListDoanhThuThang[10].DoanhThu;
                  baocao.DOANHTHUTHANG12_BCN = ListDoanhThuThang[11].DoanhThu;
                  DataProvider.Ins.model.BAOCAONAM.Add(baocao);
                  DataProvider.Ins.model.SaveChanges();
                  MessageBox.Show("Lưu báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
              });

            PrintCommand = new RelayCommand<Window>((p) =>
            {
                if (p == null || p.DataContext == null)
                    return false;

                if (ListDoanhThuThang != null)
                    return true;
                MessageBox.Show("Không có báo cáo, vui lòng xuất báo cáo trước khi in!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }, (p) =>
            {
                var MainVM = p.DataContext as MainViewModel;
                NhanVien = MainVM.NhanVien;
                NamReport rp = new NamReport();
                rp.ShowDialog();
            });

            LoadReportCommand = new RelayCommand<CrystalReportsViewer>((p) =>
            {
                if (p == null)
                    return false;
                return true;
            }, (p) =>
            {
                DoanhThuNamReport rp = new DoanhThuNamReport();
                rp.SetDataSource(ListDoanhThuThang);
                rp.SetParameterValue("txtNam", Nam);
                rp.SetParameterValue("txtTongDoanhThu", TongDoanhThu);
                rp.SetParameterValue("txtNhanVien", NhanVien.HOTEN_NV);
                p.ViewerCore.ReportSource = rp;
            });
        }
    }
}
