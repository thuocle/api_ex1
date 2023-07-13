using api_ex1.Constant;
using api_ex1.Context;
using api_ex1.Entities;
using api_ex1.IServices;
using api_ex1.Helper;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;

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
        private HoaDon GetHoaDon(int hdID)
        {
            return dbContext.HoaDon.FirstOrDefault(x => x.HoaDonID == hdID);
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
        private void DeleteHD(HoaDon hd) 
        {
            dbContext.Remove(hd);
            dbContext.SaveChanges();
        }
        private void UpdateTongTien(HoaDon hd) 
        {
            hd.TongTien = dbContext.ChiTietHoaDon.Where(x=>x.HoaDonID == hd.HoaDonID).Sum(x=>x.ThanhTien);
            dbContext.Update(hd);
            dbContext.SaveChanges();
        }
        private void ThemCTHoaDonSingle(ChiTietHoaDon ct)
        {

        }
        #endregion
        public ErrorMesssage ThemChiTietHD(List<ChiTietHoaDon> lstCT, HoaDon hd)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var ct in lstCT)
                    {
                        if (!InputHelper.CheckChiTietHD(ct))
                        {
                            return ErrorMesssage.DuLieuSai;
                        }
                        if (isSanPham(ct) == null)
                        {
                            DeleteHD(hd);
                            Environment.Exit(400);
                            return ErrorMesssage.ChuaTonTaiSanPham;
                        }
                        ct.HoaDonID = hd.HoaDonID;
                        ct.ThanhTien = ct.SoLuong * isSanPham(ct).GiaThanh;
                        dbContext.Add(ct);
                        dbContext.SaveChanges();
                        UpdateTongTien(hd);
                    }
                    trans.Commit();
                    return ErrorMesssage.ThanhCong;
                }
                catch (Exception)
                {
                    // Nếu có lỗi xảy ra, rollback transaction và ném ra ngoại lệ
                    trans.Rollback();
                    throw;
                }
            }
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

        public ErrorMesssage CapNhatHoaDon(HoaDon hd, IEnumerable<ChiTietHoaDon> lstCTNew)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var hdNow = GetHoaDon(hd.HoaDonID);
                    hdNow.ThoiGianCapNhat = DateTime.Now;
                    var lstCTNow = dbContext.ChiTietHoaDon.Where(x=>x.HoaDonID == hdNow.HoaDonID);
                    if(hdNow == null)
                    {
                        return ErrorMesssage.ChuaTonTaiHD;
                    }
                    //bot san pham trong hoa don
                    foreach (var item in lstCTNow)
                    {
                        if(!lstCTNew.Any(x=>x.SanPhamID == item.SanPhamID))
                        {
                            dbContext.Remove(item);
                        }
                    }
                    //them san pham trong hoa don hoac sua
                    foreach (var item in lstCTNew)
                    {
                            var ctNow = lstCTNow.FirstOrDefault(x => x.SanPhamID == item.SanPhamID);
                            //them
                            if (ctNow == null)
                            {
                                if (isSanPham(item) == null)
                                {
                                    return ErrorMesssage.ChuaTonTaiSanPham;
                                }
                                item.HoaDonID = hdNow.HoaDonID;
                                item.ThanhTien = item.SoLuong * isSanPham(item).GiaThanh;
                                dbContext.ChiTietHoaDon.Add(item);
                                dbContext.SaveChanges();
                            }
                            else
                            {
                                //sua
                                ctNow.SoLuong = item.SoLuong;
                                ctNow.DVT = item.DVT;
                                ctNow.ThanhTien = item.SoLuong * isSanPham(item).GiaThanh;
                                dbContext.Update(ctNow);
                                dbContext.SaveChanges();
                            }
                    }
                    //
                    UpdateTongTien(hdNow);
                    dbContext.Update(hdNow);
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
    }
}
