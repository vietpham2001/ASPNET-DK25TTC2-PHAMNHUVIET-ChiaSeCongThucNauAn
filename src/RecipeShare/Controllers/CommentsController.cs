using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeShare.Data;
using RecipeShare.Models;

namespace RecipeShare.Controllers
{
    public class CommentsController : Controller
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        // Thêm bình luận (AJAX - trả về JSON thay vì chuyển trang)
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int recipeId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return Json(new { success = false, message = "Nội dung bình luận không được để trống." });
            }

            var comment = new Comment
            {
                Content = content.Trim(),
                RecipeId = recipeId,
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Trả về dữ liệu bình luận vừa tạo để JavaScript vẽ lên trang
            return Json(new
            {
                success = true,
                id = comment.Id,
                fullName = User.FindFirstValue("FullName"),
                content = comment.Content,
                createdAt = comment.CreatedAt.ToString("dd/MM/yyyy HH:mm")
            });
        }

        // Xóa bình luận (chỉ chủ bình luận)
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (comment.UserId != currentUserId) return Forbid();

            int recipeId = comment.RecipeId;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Recipes", new { id = recipeId });
        }
    }
}
