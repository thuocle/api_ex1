using System.Text.Json.Serialization;

namespace api_ex1.Entities
{
    public class ChiTietHoaDon
    {
        public int ChiTietHoaDonID { get; set; }
        public int HoaDonID { get; set; }
        public HoaDon HoaDon { get; set; }
        public int SanPhamID { get; set; }
        public SanPham SanPham { get; set; }
        public int SoLuong { get; set; }
        public string DVT { get; set; }
        [JsonIgnore]
        public double? ThanhTien { get; set; } = 0;

    }
}
