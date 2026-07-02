# BÁO CÁO TIẾN ĐỘ ĐỒ ÁN - TUẦN 1

- **Đề tài:** Website chia sẻ công thức nấu ăn (RecipeShare)
- **Sinh viên thực hiện:** Phạm Như Việt — Lớp: DK25TTC2
- **Khoảng thời gian:** 22/06/2026 - 28/06/2026
- **Trọng tâm giai đoạn:** Khởi tạo dự án và xây dựng nền tảng cơ sở dữ liệu

---

## 1. Nội dung công việc

- Tạo GitHub repository theo đúng quy định đặt tên, tổ chức cây thư mục gồm: `src` (mã nguồn), `progress-report` (báo cáo tiến độ).
- Khởi tạo project ASP.NET Core MVC (.NET 8.0) bằng Visual Studio 2022, kiểm tra chạy thành công trên trình duyệt.
- Thiết kế mô hình cơ sở dữ liệu gồm 5 bảng: Users (người dùng), Categories (danh mục món ăn), Recipes (công thức), Comments (bình luận), Votes (đánh giá upvote/downvote).
- Xây dựng các lớp Model bằng C# tương ứng với 5 bảng, thiết lập quan hệ khóa ngoại một-nhiều thông qua navigation property.
- Cài đặt Entity Framework Core 8.0.11 (các gói SqlServer, Tools, Design) và tạo lớp AppDbContext quản lý kết nối cơ sở dữ liệu.
- Cấu hình chuỗi kết nối SQL Server trong appsettings.json và đăng ký DbContext trong Program.cs.
- Chạy Migration (Add-Migration, Update-Database) tạo thành công cơ sở dữ liệu RecipeShareDB trên SQL Server theo phương pháp Code-First.

## 2. Khó khăn gặp phải

- Lỗi cài đặt NuGet: phiên bản EF Core 10 mặc định không tương thích với .NET 8. Giải quyết bằng cách chỉ định rõ phiên bản 8.0.11 khi cài đặt.
- Lỗi Update-Database báo bảng đã tồn tại do lệnh chạy dở trước đó. Giải quyết bằng Drop-Database rồi chạy lại Update-Database.

## 3. Kết quả đạt được

- Project ASP.NET Core MVC chạy thành công trên môi trường local.
- Cơ sở dữ liệu RecipeShareDB với 5 bảng được tạo tự động bằng EF Core Code-First.
- Toàn bộ mã nguồn đã được commit và push lên GitHub repository.

## 4. Kế hoạch tuần tiếp theo (Tuần 2)

- Xây dựng chức năng đăng ký, đăng nhập, đăng xuất người dùng.
- Phân quyền: chỉ người dùng đã đăng nhập mới được đăng, sửa, xóa công thức.
- Cập nhật giao diện Layout hiển thị trạng thái đăng nhập.
