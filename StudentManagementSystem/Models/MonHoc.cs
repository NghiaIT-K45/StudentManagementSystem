using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class MonHoc
    {
        [Key] // Khóa chính
        [Display(Name = "Mã Môn học")]
        public int MaMonHoc { get; set; }

        [Required(ErrorMessage = "Tên môn học là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên môn học không được vượt quá 100 ký tự.")]
        [Display(Name = "Tên Môn học")]
        public string? TenMonHoc { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        // Khóa ngoại đến KhoaHoc (Nếu một môn học thuộc về một khóa học cụ thể)
        [Display(Name = "Mã Khóa học")]
        public int? MaKhoaHoc { get; set; } // Dùng int? để cho phép môn học không thuộc khóa học nào (optional)
        [ForeignKey("MaKhoaHoc")]
        [Display(Name = "Khóa học")]
        public KhoaHoc? KhoaHoc { get; set; }

        // Mối quan hệ: Một môn học có nhiều điểm
        public ICollection<Diem>? Diems { get; set; }
    }
}