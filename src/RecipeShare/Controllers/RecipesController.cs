using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Data;
using Microsoft.AspNetCore.Authorization;
using RecipeShare.Models;
using RecipeShare.Models.ViewModels;
using System.Security.Claims;

namespace RecipeShare.Controllers
{
    public class RecipesController : Controller
    {
        private readonly AppDbContext _context;

        public RecipesController(AppDbContext context)
        {
            _context = context;
        }

        // Trang danh sách công thức: /Recipes
        public async Task<IActionResult> Index(string? search, int? categoryId)
        {
            // Bắt đầu bằng "câu truy vấn gốc": lấy công thức kèm thông tin người đăng + danh mục
            var query = _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Category)
                .AsQueryable();

            // Nếu có từ khóa tìm kiếm → lọc theo tiêu đề
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(r => r.Title.Contains(search));
            }

            // Nếu có chọn danh mục → lọc theo danh mục
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(r => r.CategoryId == categoryId);
            }

            // Mới nhất lên đầu
            var recipes = await query.OrderByDescending(r => r.CreatedAt).ToListAsync();

            // Gửi kèm dữ liệu cho View: danh sách danh mục (để vẽ dropdown lọc) + giá trị đang lọc
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;

            return View(recipes);
        }

        // Trang chi tiết 1 công thức: /Recipes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound(); // Không có công thức này → trang 404
            }

            return View(recipe);
        }

        // ============ ĐĂNG CÔNG THỨC MỚI ============

        [Authorize] // Chưa đăng nhập → tự đá về trang Login
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(new RecipeFormViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(model);
            }

            var recipe = new Recipe
            {
                Title = model.Title,
                Description = model.Description,
                Ingredients = model.Ingredients,
                Instructions = model.Instructions,
                CategoryId = model.CategoryId,
                // Lấy Id người đang đăng nhập từ cookie (claim đã nhét hồi Tuần 2)
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            };

            // Xử lý upload ảnh (nếu có chọn)
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                recipe.ImageUrl = await SaveImageAsync(model.ImageFile);
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đăng công thức thành công!";
            return RedirectToAction("Details", new { id = recipe.Id });
        }

        // Hàm dùng chung: lưu ảnh vào wwwroot/uploads, trả về đường dẫn
        private async Task<string> SaveImageAsync(IFormFile file)
        {
            // Tạo thư mục uploads nếu chưa có
            var uploadsFolder = Path.Combine("wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);

            // Đặt tên file ngẫu nhiên để tránh trùng + tránh tên file độc hại
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}"; // Đường dẫn web để hiện ảnh
        }

        // ============ SỬA CÔNG THỨC ============

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            // Chỉ chủ công thức mới được sửa
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (recipe.UserId != currentUserId) return Forbid();

            // Đổ dữ liệu cũ vào form
            var model = new RecipeFormViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                CategoryId = recipe.CategoryId
            };

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.CurrentImage = recipe.ImageUrl;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RecipeFormViewModel model)
        {
            if (model.Id == null) return NotFound();

            var recipe = await _context.Recipes.FindAsync(model.Id);
            if (recipe == null) return NotFound();

            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (recipe.UserId != currentUserId) return Forbid();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.CurrentImage = recipe.ImageUrl;
                return View(model);
            }

            // Cập nhật dữ liệu
            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.Ingredients = model.Ingredients;
            recipe.Instructions = model.Instructions;
            recipe.CategoryId = model.CategoryId;
            recipe.UpdatedAt = DateTime.Now;

            // Có chọn ảnh mới thì thay, không thì giữ ảnh cũ
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                recipe.ImageUrl = await SaveImageAsync(model.ImageFile);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cập nhật công thức thành công!";
            return RedirectToAction("Details", new { id = recipe.Id });
        }

        // ============ XÓA CÔNG THỨC ============

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (recipe.UserId != currentUserId) return Forbid();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đã xóa công thức.";
            return RedirectToAction("Index");
        }

    }
}