using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class KhoaHoc
    {
        [Key] // Khóa chính
        [Display(Name = "Mã Khóa học")]
        public int MaKhoaHoc { get; set; }

        [Required(ErrorMessage = "Tên khóa học là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên khóa học không được vượt quá 100 ký tự.")]
        [Display(Name = "Tên Khóa học")]
        public string? TenKhoaHoc { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        // Mối quan hệ: Một khóa học có nhiều môn học (nhiều-nhiều với MonHoc thông qua một bảng trung gian hoặc bỏ qua nếu đơn giản)
        // Hiện tại, để đơn giản, giả sử một MonHoc thuộc về một KhoaHoc
        public ICollection<MonHoc>? MonHocs { get; set; }
    }
}