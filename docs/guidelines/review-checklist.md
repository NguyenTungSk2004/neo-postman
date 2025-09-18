# Checklist Review Pull Request

## 1. Tổng quan
- [ ] PR mô tả rõ mục tiêu và liên kết ticket.
- [ ] Phạm vi thay đổi phù hợp (không quá lớn, tránh chồng lấn tính năng khác).

## 2. Chức năng
- [ ] Yêu cầu nghiệp vụ được đáp ứng, không bỏ sót edge case.
- [ ] Có test manual hoặc automation chứng minh hành vi.
- [ ] Không phá vỡ tính năng hiện có (regression).

## 3. Code quality
- [ ] Tên biến/hàm/class dễ hiểu, theo guideline.
- [ ] Không còn mã chết (dead code), TODO chưa xử lý, console log thừa.
- [ ] Logic phức tạp có giải thích hoặc tách nhỏ hợp lý.
- [ ] Áp dụng dependency injection đúng chuẩn (nếu có).

## 4. Bảo mật & hiệu năng
- [ ] Kiểm tra quyền truy cập (Authorization) ở handler/API.
- [ ] Xử lý input an toàn, tránh SQL/Script injection.
- [ ] Không load dữ liệu thừa; các truy vấn có index phù hợp.

## 5. Kiểm thử
- [ ] Đã cập nhật hoặc thêm unit test liên quan.
- [ ] Đã cập nhật integration/E2E test (nếu cần).
- [ ] Pipeline CI/CD đang xanh.

## 6. Tài liệu
- [ ] Cập nhật README/SRS/Use Case nếu nghiệp vụ thay đổi.
- [ ] Cập nhật docs API hoặc comment kỹ thuật nếu thêm endpoint mới.

## 7. Deployment
- [ ] Không quên script migration hoặc cấu hình hạ tầng.
- [ ] Có kế hoạch rollback nếu sự cố xảy ra.
