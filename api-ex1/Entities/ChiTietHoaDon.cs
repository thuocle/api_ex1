using System.Text.Json.Serialization;

namespace api_ex1.Entities
{
    public class ChiTietHoaDon
    {
        public int ChiTietHoaDonID { get; set; }
        [JsonIgnore]
        public int HoaDonID { get; set; }
        [JsonIgnore]
        public HoaDon? HoaDon { get; set; }
        public int SanPhamID { get; set; }
        [JsonIgnore]
        public SanPham? SanPham { get; set; }
        public int SoLuong { get; set; }
        public string DVT { get; set; }
        [JsonIgnore]
        public double? ThanhTien { get; set; } = 0;

    }
}
