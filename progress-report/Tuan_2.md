# BÁO CÁO TIẾN ĐỘ ĐỒ ÁN - TUẦN 2

- **Đề tài:** Website chia sẻ công thức nấu ăn (RecipeShare)
- **Sinh viên thực hiện:** Phạm Như Việt — Lớp: DK25TTC2
- **Khoảng thời gian:** 29/06/2026 - 05/07/2026
- **Trọng tâm giai đoạn:** Xây dựng hệ thống xác thực và phân quyền người dùng

---

## 1. Nội dung công việc

- Cài đặt thư viện BCrypt.Net-Next để mã hóa mật khẩu một chiều, đảm bảo không lưu mật khẩu gốc trong cơ sở dữ liệu.
- Cấu hình Cookie Authentication trong Program.cs: thiết lập đường dẫn đăng nhập, trang từ chối truy cập, thời gian sống cookie 7 ngày.
- Xây dựng các lớp ViewModel (RegisterViewModel, LoginViewModel) với Data Annotations để kiểm tra dữ liệu đầu vào tự động (bắt buộc nhập, định dạng email, độ dài mật khẩu, xác nhận mật khẩu khớp).
- Xây dựng AccountController với đầy đủ chức năng: đăng ký (kiểm tra trùng username/email, mã hóa mật khẩu bằng BCrypt), đăng nhập (xác thực bằng BCrypt.Verify, tạo Claims và ghi cookie), đăng xuất (xóa cookie), trang AccessDenied.
- Áp dụng các biện pháp bảo mật: chống tấn công CSRF bằng ValidateAntiForgeryToken, thông báo lỗi đăng nhập chung chung để tránh lộ thông tin tài khoản, đăng xuất chỉ nhận phương thức POST.
- Tạo giao diện trang Đăng ký và Đăng nhập bằng Razor View với Tag Helper và Bootstrap 5, hỗ trợ hiển thị lỗi kiểm tra dữ liệu bằng tiếng Việt.
- Cập nhật Layout: thanh điều hướng hiển thị lời chào kèm họ tên khi đã đăng nhập, nút Đăng xuất, hoặc 2 liên kết Đăng nhập / Đăng ký khi chưa đăng nhập.

## 2. Khó khăn gặp phải

- Trang /Account/Register báo lỗi ERR_EMPTY_RESPONSE dù đã build lại code. Nguyên nhân: chỉ đóng cửa sổ trình duyệt chứ chưa dừng hẳn tiến trình server, nên server vẫn chạy phiên bản code cũ. Giải quyết: dừng server bằng nút Stop trong Visual Studio rồi chạy lại — rút ra quy trình chuẩn khi sửa code C#: Stop → chạy lại.
- Lệnh Install-Package báo lỗi "Project 'Default' is not found" do Visual Studio đang mở ở chế độ Folder View. Giải quyết: mở project qua file .sln.

## 3. Kết quả đạt được

- Người dùng đăng ký tài khoản thành công, dữ liệu kiểm tra chặt chẽ ở cả client và server.
- Mật khẩu được mã hóa BCrypt trước khi lưu vào cơ sở dữ liệu.
- Đăng nhập / đăng xuất hoạt động ổn định bằng Cookie Authentication, hỗ trợ tùy chọn "Ghi nhớ đăng nhập".
- Thanh điều hướng phản ánh đúng trạng thái đăng nhập của người dùng.

## 4. Kế hoạch tuần tiếp theo (Tuần 3)

- Xây dựng chức năng CRUD công thức nấu ăn: đăng, xem, sửa, xóa.
- Upload hình ảnh minh họa cho công thức.
- Quản lý danh mục món ăn và chức năng tìm kiếm, lọc theo danh mục.
