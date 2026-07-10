using Microsoft.EntityFrameworkCore;
using RecipeShare.Models;

namespace RecipeShare.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mỗi user chỉ được vote 1 lần cho mỗi công thức
            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.UserId, v.RecipeId })
                .IsUnique();

            // Tránh lỗi xóa dây chuyền (cascade delete conflict)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .OnDelete(DeleteBehavior.Restrict);
            // Seed dữ liệu danh mục có sẵn
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Món chính", Description = "Các món ăn chính trong bữa cơm" },
                new Category { Id = 2, Name = "Món khai vị", Description = "Gỏi, salad, súp khai vị" },
                new Category { Id = 3, Name = "Tráng miệng", Description = "Chè, bánh ngọt, trái cây" },
                new Category { Id = 4, Name = "Đồ uống", Description = "Sinh tố, nước ép, trà, cà phê" },
                new Category { Id = 5, Name = "Ăn vặt", Description = "Món ăn chơi, ăn nhẹ" },
                new Category { Id = 6, Name = "Món chay", Description = "Các món thuần chay" }
            );

        }
    }
}
