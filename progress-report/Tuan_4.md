# BÁO CÁO TIẾN ĐỘ ĐỒ ÁN - TUẦN 4

- **Đề tài:** Website chia sẻ công thức nấu ăn (RecipeShare)
- **Sinh viên thực hiện:** Phạm Như Việt — Lớp: DK25TTC2
- **Khoảng thời gian:** 13/07/2026 - 19/07/2026
- **Trọng tâm giai đoạn:** Bình luận và hệ thống đánh giá Upvote/Downvote kiểu Reddit

---

## 1. Nội dung công việc

- Xây dựng chức năng bình luận trên trang chi tiết công thức:
  - CommentsController với action thêm và xóa bình luận; kiểm tra nội dung rỗng phía server; phân quyền xóa chỉ dành cho chủ bình luận (mô hình kiểm tra chủ sở hữu như Tuần 3).
  - Nạp bình luận kèm thông tin người viết bằng Include lồng nhau (Include Comments, ThenInclude User).
- Nâng cấp gửi bình luận sang kỹ thuật AJAX với Fetch API:
  - Controller trả về JSON thay vì Redirect; JavaScript chặn hành vi gửi form truyền thống (preventDefault), gửi request ngầm và vẽ bình luận mới lên trang tức thì mà không tải lại trang.
  - Chèn nội dung bằng textContent thay vì innerHTML để phòng chống tấn công XSS qua nội dung bình luận.
  - Tách mã JavaScript ra file riêng trong wwwroot/js, nạp qua thẻ script với asp-append-version để tránh trình duyệt dùng bản cache cũ khi file thay đổi.
- Xây dựng hệ thống Upvote/Downvote kiểu Reddit:
  - VotesController với action Toggle xử lý 3 tình huống: chưa vote thì tạo mới; bấm lại nút đang chọn thì rút vote; bấm nút ngược lại thì lật chiều vote. Ràng buộc mỗi người một vote cho mỗi công thức được đảm bảo bằng unique index (UserId, RecipeId) đã thiết kế từ Tuần 1.
  - Điểm hiển thị = số upvote trừ số downvote, cho phép điểm âm — nhất quán với cơ chế của Reddit.
  - Nút vote hoạt động bằng AJAX ở cả trang chi tiết và trang danh sách (nhận diện nút theo data-recipe-id); màu nút phản ánh trạng thái vote của người xem và được render từ server nên giữ nguyên sau khi tải lại trang.
- Bổ sung sắp xếp và trang nổi bật: tab "Mới nhất" (theo thời gian đăng) và "Nổi bật" (theo điểm vote, do database sắp xếp trực tiếp qua LINQ); chuyển tab giữ nguyên điều kiện tìm kiếm và lọc danh mục đang áp dụng.

## 2. Khó khăn gặp phải

- Sau khi gửi bình luận bằng form truyền thống, bấm nút Back của trình duyệt bị "kẹt" lại trạng thái form vừa gửi, phải bấm Back hai lần mới thoát. Nguyên nhân: trình duyệt ghi bước POST vào lịch sử điều hướng. Đã tìm hiểu và giải quyết triệt để bằng cách chuyển sang gửi bình luận qua AJAX — không điều hướng trang nên không phát sinh mục lịch sử, đồng thời cải thiện trải nghiệm người dùng.
- Trang danh sách hiển thị điểm vote bằng 0 cho mọi công thức dù dữ liệu vote vẫn còn trong database. Nguyên nhân: truy vấn ở action Index thiếu Include(r => r.Votes) nên danh sách vote rỗng và phép đếm luôn ra 0 — dạng lỗi "sai âm thầm" không văng ngoại lệ nên khó phát hiện hơn lỗi sập trang. Khắc phục bằng cách bổ sung Include và kiểm tra lại toàn bộ các truy vấn có dùng navigation property.

## 3. Kết quả đạt được

- Người dùng bình luận và xóa bình luận của mình; bình luận mới hiển thị tức thì không tải lại trang.
- Vote hoạt động mượt ở cả trang danh sách và trang chi tiết: điểm cập nhật tức thì, màu nút thể hiện đúng trạng thái vote của từng người và giữ nguyên sau khi tải lại trang.
- Tab "Nổi bật" xếp công thức theo điểm cộng đồng đánh giá, kết hợp được với tìm kiếm và lọc danh mục.

## 4. Kế hoạch tuần tiếp theo (Tuần 5)

- Hoàn thiện giao diện tổng thể: trang chủ, điều hướng, chi tiết hiển thị.
- Viết báo cáo đồ án theo mẫu (bố cục 5 chương, quy cách trình bày theo quy định) và slide thuyết trình.
- Hoàn thiện README.md: bổ sung mục các lỗi thường gặp, cập nhật đầy đủ chức năng; rà soát bug toàn hệ thống.
