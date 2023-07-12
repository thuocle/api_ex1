using api_ex1.Context;
using api_ex1.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_ex1.Helper
{
    public enum inputType
    {
        Them
    }
    public class InputHelper
    {
        public static bool CheckHoaDon(HoaDon hd)
        {
            object khid = hd.KhachHangID;
            object ghichu = hd.GhiChu;
            object tenhd = hd.TenHoaDon;

            if(khid is not int || ghichu is not string || tenhd is not string)
                return false;
            return true ;
        }
        public static bool CheckChiTietHD(ChiTietHoaDon ct)
        {
            object spid = ct.SanPhamID;
            object sl = ct.SoLuong;
            object dvt = ct.DVT;

            if(spid is not int || sl is not int || dvt is not string)
                return false;
            return true;
        }
    }
}
