﻿using api_ex1.Helper;
using System.Text.Json.Serialization;

namespace api_ex1.Entities
{
    public class HoaDon
    {
        public int HoaDonID { get; set; }
        public int KhachHangID { get; set; }
        [JsonIgnore]
        public KhachHang? KhachHang { get; set; }
        [JsonIgnore]
        public string? TenHoaDon { get; set; }
        [JsonIgnore]
        public string? MaGiaoDich { get; set; } = null;
        [JsonIgnore]
        public DateTime? ThoiGianTao { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? ThoiGianCapNhat { get; set; } = DateTime.Now;
        [JsonIgnore]
        public string? GhiChu { get; set; }
        [JsonIgnore]
        public double? TongTien { get; set; } = 0;

        public IEnumerable<ChiTietHoaDon>? ChiTietHoaDon { get; set; }
    }
}
