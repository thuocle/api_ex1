using api_ex1.Constant;
using api_ex1.Entities;

namespace api_ex1.IServices
{
    public interface IHoaDonServices
    {
        ErrorMesssage ThemHoaDon(HoaDon hd);
        ErrorMesssage ThemChiTietHD(List<ChiTietHoaDon> lstCT, HoaDon hd);
        ErrorMesssage CapNhatHoaDon(HoaDon hd, IEnumerable<ChiTietHoaDon> lstCTNew);
        ErrorMesssage XoaHoaDon(int hdID);
        public IEnumerable<HoaDon> GetListHoaDon(string? key, int pageSize, int pageNumber);
    }
}
