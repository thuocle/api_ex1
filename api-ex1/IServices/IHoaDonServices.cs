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
        PageInfo<HoaDon> GetListHoaDonByTime( Pagination page);
        PageInfo<HoaDon> SearchHoaDonByKey(string? name, Pagination page);
        PageInfo<HoaDon> FilterHoaDonByMonthYear(int? month, int? year, Pagination page);
        PageInfo<HoaDon> FilterHoaDonByDateRange(DateTime? dateMin, DateTime? dateMax, Pagination page);
        PageInfo<HoaDon> FilterHoaDonByTotalRange(double? totalMin, double? totalMax, Pagination page);
    }
}
