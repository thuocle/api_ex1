namespace api_ex1.Entities
{
    public class KhachHang
    {
        public int KhachHangID { get; set; }
        public string TenKhachHang { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SDT { get; set; }
        public IEnumerable<HoaDon> HoaDon { get; set; }    
    }
}
