﻿using api_ex1.Constant;
using api_ex1.Entities;

namespace api_ex1.IServices
{
    public interface IHoaDonServices
    {
        ErrorMesssage ThemHoaDon(HoaDon hd);
        ErrorMesssage ThemChiTietHD(ChiTietHoaDon ct, HoaDon hd);
        ErrorMesssage ThemLoai(LoaiSanPham loai);
    }
}
