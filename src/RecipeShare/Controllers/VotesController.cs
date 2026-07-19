using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Data;
using RecipeShare.Models;

namespace RecipeShare.Controllers
{
    public class VotesController : Controller
    {
        private readonly AppDbContext _context;

        public VotesController(AppDbContext context)
        {
            _context = context;
        }

        // Xử lý vote kiểu Reddit (AJAX)
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int recipeId, bool isUpvote)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Tìm vote hiện có của user này cho công thức này
            var existingVote = await _context.Votes
                .FirstOrDefaultAsync(v => v.RecipeId == recipeId && v.UserId == userId);

            string userVote; // trạng thái sau khi xử lý: "up", "down", hoặc "none"

            if (existingVote == null)
            {
                // Chưa vote → tạo mới
                _context.Votes.Add(new Vote
                {
                    RecipeId = recipeId,
                    UserId = userId,
                    IsUpvote = isUpvote
                });
                userVote = isUpvote ? "up" : "down";
            }
            else if (existingVote.IsUpvote == isUpvote)
            {
                // Bấm lại đúng nút đang chọn → rút vote
                _context.Votes.Remove(existingVote);
                userVote = "none";
            }
            else
            {
                // Bấm nút ngược lại → lật chiều vote
                existingVote.IsUpvote = isUpvote;
                userVote = isUpvote ? "up" : "down";
            }

            await _context.SaveChangesAsync();

            // Tính lại điểm: số upvote - số downvote
            var upCount = await _context.Votes.CountAsync(v => v.RecipeId == recipeId && v.IsUpvote);
            var downCount = await _context.Votes.CountAsync(v => v.RecipeId == recipeId && !v.IsUpvote);

            return Json(new { success = true, score = upCount - downCount, userVote });
        }
    }
}