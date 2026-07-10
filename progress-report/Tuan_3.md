# BÁO CÁO TIẾN ĐỘ ĐỒ ÁN - TUẦN 3

- **Đề tài:** Website chia sẻ công thức nấu ăn (RecipeShare)
- **Sinh viên thực hiện:** Phạm Như Việt — Lớp: DK25TTC2
- **Khoảng thời gian:** 06/07/2026 - 12/07/2026
- **Trọng tâm giai đoạn:** Chức năng quản lý công thức (CRUD), upload hình ảnh, tìm kiếm và lọc

---

## 1. Nội dung công việc

- Bổ sung dữ liệu khởi tạo (seed data) cho bảng Categories bằng cơ chế HasData của EF Core: 6 danh mục món ăn có sẵn, tạo Migration SeedCategories.
- Xây dựng RecipesController với đầy đủ chức năng CRUD:
  - Danh sách công thức (Index) với Include để JOIN thông tin người đăng và danh mục, sắp xếp mới nhất trước.
  - Chi tiết công thức (Details) hiển thị đầy đủ nguyên liệu và cách làm.
  - Đăng công thức (Create) kèm upload hình ảnh: file lưu vào wwwroot/uploads với tên GUID ngẫu nhiên để tránh trùng lặp và tên file độc hại, database chỉ lưu đường dẫn.
  - Sửa công thức (Edit): đổ dữ liệu cũ vào form, cho phép giữ hoặc thay ảnh.
  - Xóa công thức (Delete) với hộp thoại xác nhận trước khi xóa.
- Áp dụng phân quyền 3 lớp:
  - Lớp 1 — [Authorize]: chưa đăng nhập không thể truy cập trang đăng/sửa/xóa, tự chuyển về trang Login.
  - Lớp 2 — kiểm tra chủ sở hữu trong controller: so sánh Id người đăng nhập (từ Claims) với UserId của công thức, không khớp trả về Forbid (trang AccessDenied).
  - Lớp 3 — giao diện: nút Sửa/Xóa chỉ hiển thị với chủ công thức.
- Xây dựng chức năng tìm kiếm theo tên món và lọc theo danh mục bằng kỹ thuật truy vấn động (đắp điều kiện Where theo tham số, thực thi một câu SQL duy nhất).
- Tạo giao diện: trang danh sách dạng lưới card có ảnh, thanh tìm kiếm + dropdown lọc; form đăng/sửa công thức; trang chi tiết; trang AccessDenied.
- Thêm liên kết "Công thức" vào thanh điều hướng.
- Tinh giản mô hình dữ liệu: loại bỏ 3 trường thời gian chuẩn bị, thời gian nấu, khẩu phần bằng Migration RemoveRecipeTimeAndServings theo định hướng nền tảng chia sẻ tối giản thao tác đăng bài.
- Khởi tạo cấu trúc thư mục thesis (doc, pdf, html, abs, refs) theo quy định tổ chức repository.

## 2. Khó khăn gặp phải

- Khi xây dựng chức năng upload hình ảnh, gặp tình huống file ảnh không được gửi lên server 
dù giao diện vẫn cho phép chọn ảnh và không xuất hiện thông báo lỗi nào. Nguyên nhân: thẻ 
form thiếu thuộc tính enctype="multipart/form-data" nên trình duyệt chỉ gửi dữ liệu văn bản. 
Cách khắc phục: bổ sung thuộc tính này trên cả hai form Đăng và Sửa công thức, sau đó kiểm 
thử lại toàn bộ luồng upload và thay ảnh.

## 3. Kết quả đạt được

- Người dùng đăng, xem, sửa, xóa công thức với hình ảnh minh họa upload từ máy.
- Phân quyền hoạt động đúng qua kiểm thử 4 kịch bản: chủ công thức thấy và dùng được nút Sửa/Xóa; người chưa đăng nhập bị chuyển về Login; người dùng khác truy cập thẳng URL sửa bị chặn bởi trang AccessDenied và không thấy nút Sửa/Xóa trên giao diện.
- Tìm kiếm theo từ khóa, lọc theo danh mục và kết hợp cả hai đều trả về kết quả chính xác.
- Form đăng bài tinh gọn: tên món, mô tả, danh mục, nguyên liệu, cách làm, hình ảnh.

## 4. Kế hoạch tuần tiếp theo (Tuần 4)

- Xây dựng chức năng bình luận trên công thức.
- Hệ thống Upvote/Downvote kiểu Reddit với ràng buộc mỗi người một lượt vote cho mỗi công thức.
- Sắp xếp công thức theo điểm vote và trang công thức nổi bật.
