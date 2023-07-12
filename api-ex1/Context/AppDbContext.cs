using api_ex1.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_ex1.Context
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = THUOCLE\\THUOCLE; Database = HoaDon; Trusted_Connection = True;TrustServerCertificate=True");
        }

    }
}
