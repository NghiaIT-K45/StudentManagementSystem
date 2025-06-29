using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Diem
    {
        [Key] // Khóa chính
        [Display(Name = "Mã Điểm")]
        public int MaDiem { get; set; }

        [Required(ErrorMessage = "Mã sinh viên là bắt buộc.")]
        [Display(Name = "Mã Sinh viên")]
        public int MaSV { get; set; }
        [ForeignKey("MaSV")]
        public SinhVien? SinhVien { get; set; } // Navigation property

        [Required(ErrorMessage = "Mã môn học là bắt buộc.")]
        [Display(Name = "Mã Môn học")]
        public int MaMonHoc { get; set; }
        [ForeignKey("MaMonHoc")]
        public MonHoc? MonHoc { get; set; } // Navigation property

        [Required(ErrorMessage = "Điểm là bắt buộc.")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10.")]
        [Display(Name = "Điểm")]
        public double SoDiem { get; set; }

        [Display(Name = "Ngày cập nhật")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
    }
}