using System.ComponentModel.DataAnnotations;

namespace RecipeShare.Models.ViewModels
{
    public class RecipeFormViewModel
    {
        public int? Id { get; set; } // null = tạo mới, có giá trị = đang sửa

        [Required(ErrorMessage = "Vui lòng nhập tên món ăn")]
        [StringLength(200, ErrorMessage = "Tên món tối đa 200 ký tự")]
        [Display(Name = "Tên món ăn")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        [Display(Name = "Mô tả ngắn")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập nguyên liệu")]
        [Display(Name = "Nguyên liệu (mỗi dòng 1 nguyên liệu)")]
        public string Ingredients { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập cách làm")]
        [Display(Name = "Các bước thực hiện")]
        public string Instructions { get; set; } = string.Empty;

        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Hình ảnh món ăn")]
        public IFormFile? ImageFile { get; set; } // File ảnh upload từ máy người dùng
    }
}