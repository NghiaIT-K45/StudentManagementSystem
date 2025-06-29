using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class SinhVien
    {
        [Key] // Khóa chính
        [Display(Name = "Mã Sinh viên")]
        public int MaSV { get; set; }

        [Required(ErrorMessage = "Tên sinh viên là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
        [Display(Name = "Tên Sinh viên")]
        public string? TenSV { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public string? GioiTinh { get; set; } // Ví dụ: "Nam", "Nữ"

        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [Display(Name = "Số điện thoại")]
        public string? SoDienThoai { get; set; }

        // Mối quan hệ: Một sinh viên có nhiều điểm
        public ICollection<Diem>? Diems { get; set; }
    }
}