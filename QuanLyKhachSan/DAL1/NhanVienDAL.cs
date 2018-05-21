using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL1
{
    public class NhanVienDAL
    {
        private QLKS_IS201Entities model;
        public int Ma_NV, Ma_TK;
        public DateTime NgaySinh_NV, NgayVaoLam_NV;
        public bool GioiTinh_NV;
        public string HoTen_NV, SoDienThoai_NV, ChucVu_NV, DiaChi_NV;
        

        public NhanVienDAL()
        {
            model = new QLKS_IS201Entities();
        }

        // Lấy danh sách nhân viên
        public List<NHANVIEN> layNhanVien()
        {            
            try
            {
                return model.NHANVIEN.ToList();
            }
            catch
            {
                return null;
            }
        }

        // Thêm nhân viên
        public bool themNhanVien(NHANVIEN nv)
        {
            try
            {
                model.NHANVIEN.Add(nv);
                model.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Xóa nhân viên
        public bool xoaNhanVien()
        {
            try
            {
                NHANVIEN nv = model.NHANVIEN.Find(Ma_NV);
                model.NHANVIEN.Remove(nv);
                model.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Sửa thông tin nhân viên
        public bool suaNhanVien()
        {
            try
            {
                NHANVIEN nv = model.NHANVIEN.Find(Ma_NV);
                nv.HOTEN_NV = HoTen_NV;
                nv.MA_TK = Ma_TK;
                nv.GIOITINH_NV = GioiTinh_NV;
                nv.NGAYSINH_NV = NgaySinh_NV;
                nv.SODIENTHOAI_NV = SoDienThoai_NV;
                nv.CHUCVU_NV = ChucVu_NV;
                nv.DIACHI_NV = DiaChi_NV;
                nv.NGAYVAOLAM_NV = NgayVaoLam_NV;
                model.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
