using api_ex1.Constant;
using api_ex1.Context;
using api_ex1.Entities;
using api_ex1.IServices;
using api_ex1.Helper;

namespace api_ex1.Services
{
    public class HoaDonServices : IHoaDonServices
    {
        private readonly AppDbContext dbContext;

        public HoaDonServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private method
        private bool isKhachHang(HoaDon hd)
        {
            return dbContext.KhachHang.Any(x=>x.KhachHangID == hd.KhachHangID);
        }
        private SanPham isSanPham(ChiTietHoaDon ct)
        {
            return dbContext.SanPham.FirstOrDefault(x=>x.SanPhamID == ct.SanPhamID);
        }
        private string CreateMaGD()
        {
            int x = dbContext.HoaDon.Count(x=>x.ThoiGianTao.Value.Date == DateTime.Now.Date);
            string mgd = $"{DateTime.Now:yyyyMMdd}_{x+1:D3}";
            return mgd;
        }
        private void UpdateTongTien(HoaDon hd, double? tt) 
        {
            hd.TongTien += tt;
            dbContext.Update(hd);
            dbContext.SaveChanges();
        }
        #endregion
        public ErrorMesssage ThemChiTietHD(ChiTietHoaDon ct, HoaDon hd)
        {
                    if (!InputHelper.CheckChiTietHD(ct))
                    {
                        return ErrorMesssage.DuLieuSai;
                    }
                    if (isSanPham(ct)==null)
                    {
                        return ErrorMesssage.ChuaTonTaiSanPham;
                    }
                    ct.HoaDonID = hd.HoaDonID;
                    ct.ThanhTien = ct.SoLuong * isSanPham(ct).GiaThanh;
                    dbContext.Add(ct);
                    dbContext.SaveChanges();
                    UpdateTongTien(hd, ct.ThanhTien);

            // Commit transaction
            return ErrorMesssage.ThanhCong;
        }

        public ErrorMesssage ThemHoaDon(HoaDon hd)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (!InputHelper.CheckHoaDon(hd))
                    {
                        return ErrorMesssage.DuLieuSai;
                    }
                    if (!isKhachHang(hd))
                    {
                        return ErrorMesssage.ChuaTonTaiKH;
                    }
                    hd.MaGiaoDich = CreateMaGD();
                    dbContext.Add(hd);
                    dbContext.SaveChanges();
                    trans.Commit();
                    return ErrorMesssage.ThanhCong;
                    // Commit transaction
                }
                catch (Exception)
                {
                    // Nếu có lỗi xảy ra, rollback transaction và ném ra ngoại lệ
                    trans.Rollback();
                    throw;
                }
            }

        }

        public ErrorMesssage ThemLoai(LoaiSanPham loai)
        {
            dbContext.Add(loai);
            dbContext.SaveChanges();
            return ErrorMesssage.ThanhCong;
        }
    }
}
