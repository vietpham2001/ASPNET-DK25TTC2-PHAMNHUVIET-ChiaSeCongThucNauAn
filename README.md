# 🍜 RecipeShare — Website chia sẻ công thức nấu ăn

Đồ án môn học **Chuyên đề ASP.NET** — Lớp DK25TTC2

## 📑 Mục lục

- [Giới thiệu đề tài](#-giới-thiệu-đề-tài)
- [Công nghệ sử dụng](#-công-nghệ-sử-dụng)
- [Chức năng chính](#-chức-năng-chính)
- [Cấu trúc thư mục](#-cấu-trúc-thư-mục)
- [Cơ sở dữ liệu](#-cơ-sở-dữ-liệu)
- [Hướng dẫn cài đặt và chạy](#-hướng-dẫn-cài-đặt-và-chạy)
- [Tiến độ thực hiện](#-tiến-độ-thực-hiện)

## 📖 Giới thiệu đề tài

RecipeShare là website cho phép người dùng chia sẻ và khám phá các công thức nấu ăn. Người dùng có thể đăng công thức kèm hình ảnh, tìm kiếm theo danh mục, bình luận trao đổi và đánh giá công thức bằng hệ thống upvote/downvote tương tự Reddit.

## 🛠 Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Backend | ASP.NET Core MVC (.NET 8.0) |
| ORM | Entity Framework Core 8.0.11 (Code-First) |
| Cơ sở dữ liệu | SQL Server |
| Frontend | Razor Views, Bootstrap 5 |
| IDE | Visual Studio 2022 |
| Quản lý mã nguồn | Git + GitHub |

## ⚙️ Chức năng chính

- [x] Thiết kế cơ sở dữ liệu (5 bảng: Users, Categories, Recipes, Comments, Votes)
- [x] Đăng ký / Đăng nhập / Phân quyền người dùng
- [ ] Đăng, sửa, xóa công thức nấu ăn (CRUD) kèm upload hình ảnh
- [ ] Tìm kiếm và lọc công thức theo danh mục
- [ ] Bình luận trên công thức
- [ ] Upvote / Downvote công thức, xếp hạng công thức nổi bật

*(Các ô tick sẽ được cập nhật theo tiến độ từng tuần)*

## 📂 Cấu trúc thư mục

```
├── src/                  # Mã nguồn project ASP.NET Core MVC
│   └── RecipeShare/
│       ├── Controllers/  # Xử lý request
│       ├── Models/       # Các lớp thực thể (Entity)
│       ├── Views/        # Giao diện Razor
│       ├── Data/         # AppDbContext — kết nối CSDL
│       ├── Migrations/   # Lịch sử thay đổi CSDL (EF Core)
│       └── wwwroot/      # File tĩnh (CSS, JS, hình ảnh)
├── progress-report/      # Báo cáo tiến độ hàng tuần
└── thesis/               # Tài liệu báo cáo đồ án (cập nhật ở tuần cuối)
```

## 🗄 Cơ sở dữ liệu

CSDL `RecipeShareDB` được tạo tự động bằng EF Core Code-First, gồm 5 bảng:

| Bảng | Mô tả | Quan hệ |
|---|---|---|
| **Users** | Thông tin người dùng (username, email, mật khẩu đã mã hóa) | 1 user → nhiều Recipes, Comments, Votes |
| **Categories** | Danh mục món ăn (Món chính, Tráng miệng, Đồ uống...) | 1 category → nhiều Recipes |
| **Recipes** | Công thức nấu ăn (tiêu đề, nguyên liệu, cách làm, thời gian, hình ảnh) | Thuộc 1 User và 1 Category |
| **Comments** | Bình luận của người dùng trên công thức | Thuộc 1 User và 1 Recipe |
| **Votes** | Đánh giá upvote/downvote (cột `IsUpvote`: 1 = upvote, 0 = downvote) | Thuộc 1 User và 1 Recipe |

Ràng buộc đặc biệt: mỗi user chỉ được vote **1 lần** cho mỗi công thức (unique index trên cặp `UserId + RecipeId`).

## 👤 Tài khoản test

| Tên đăng nhập | Mật khẩu | Ghi chú |
|---|---|---|
| user1 | 123456 | Tài khoản người dùng thường |

## 🚀 Hướng dẫn cài đặt và chạy


### Yêu cầu môi trường

- Visual Studio 2022 (workload ASP.NET and web development)
- .NET 8.0 SDK
- SQL Server (Express hoặc Developer Edition)

### Các bước chạy project

1. Clone repository về máy:
   ```bash
   git clone https://github.com/vietpham2001/ASPNET-DK25TTC2-PHAMNHUVIET-ChiaSeCongThucNauAn.git
   ```
2. Mở file `src/RecipeShare/RecipeShare.sln` bằng Visual Studio 2022.
3. Kiểm tra chuỗi kết nối trong `appsettings.json`, sửa `Server=` cho khớp với SQL Server trên máy (mặc định: `localhost`).
4. Mở **Tools → NuGet Package Manager → Package Manager Console**, chạy lệnh:
   ```
   Update-Database
   ```
   Lệnh này sẽ tự động tạo cơ sở dữ liệu `RecipeShareDB` cùng toàn bộ các bảng.
5. Nhấn `Ctrl + F5` để chạy website.

## 📅 Tiến độ thực hiện

| Tuần | Nội dung | Trạng thái |
|---|---|---|
| 1 | Khởi tạo project, thiết kế CSDL, EF Core, Migration | ✅ Hoàn thành |
| 2 | Đăng ký / Đăng nhập / Phân quyền | ✅ Hoàn thành |
| 3 | CRUD công thức + Tìm kiếm & lọc | 🔄 Đang thực hiện |
| 4 | Bình luận + Upvote/Downvote | ⬜ Chưa bắt đầu |
| 5 | Hoàn thiện giao diện + Báo cáo | ⬜ Chưa bắt đầu |
