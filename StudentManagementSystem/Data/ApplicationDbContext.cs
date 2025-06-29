using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models; // Đảm bảo bạn đã thêm namespace này

namespace StudentManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Định nghĩa các DbSet cho các Model của bạn
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<Diem> Diems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Luôn gọi base.OnModelCreating cho IdentityDbContext

            // Ví dụ cấu hình mối quan hệ nhiều-nhiều cho SinhVien và MonHoc thông qua Diem
            // (Nếu không có bảng Diem, bạn sẽ cần cấu hình bảng trung gian)
            // Cấu hình ràng buộc duy nhất cho Diem (một sinh viên chỉ có một điểm cho một môn học)
            modelBuilder.Entity<Diem>()
                .HasIndex(d => new { d.MaSV, d.MaMonHoc })
                .IsUnique();

            // Cấu hình DELETE behavior cho Foreign Keys
            // Khi xóa một SinhVien, các bản ghi Diem liên quan cũng sẽ bị xóa
            modelBuilder.Entity<SinhVien>()
                .HasMany(sv => sv.Diems)
                .WithOne(d => d.SinhVien)
                .HasForeignKey(d => d.MaSV)
                .OnDelete(DeleteBehavior.Cascade); // Ví dụ: Cascade delete

            // Khi xóa một MonHoc, các bản ghi Diem liên quan cũng sẽ bị xóa
            modelBuilder.Entity<MonHoc>()
                .HasMany(mh => mh.Diems)
                .WithOne(d => d.MonHoc)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.Cascade); // Ví dụ: Cascade delete

            // Khi xóa một KhoaHoc, các MonHoc thuộc về nó có thể được xử lý
            // Mặc định là ClientSetNull (NULLify ForeignKey), bạn có thể thay đổi thành Cascade nếu muốn
            modelBuilder.Entity<KhoaHoc>()
                .HasMany(kh => kh.MonHocs)
                .WithOne(mh => mh.KhoaHoc)
                .HasForeignKey(mh => mh.MaKhoaHoc)
                .OnDelete(DeleteBehavior.SetNull); // Ví dụ: Đặt MaKhoaHoc về NULL khi xóa KhoaHoc
        }
    }
}