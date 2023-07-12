namespace api_ex1.Entities
{
    public class LoaiSanPham
    {
        public int LoaiSanPhamID { get; set; }
        public string TenLoaiSanPham { get; set; }
        public IEnumerable<SanPham> SanPham { get; set; }
    }
}
