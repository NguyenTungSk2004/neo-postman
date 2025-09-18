# UC-09 Xem lịch sử request

- **Actor**: Thành viên xác thực
- **Mục đích**: Tra cứu và phân tích các lần gửi request trước đó.

## Điều kiện tiên quyết
- Người dùng có quyền xem lịch sử của request.
- Trong `RequestHistory` đã có dữ liệu tương ứng.

## Luồng chính
1. Người dùng mở tab lịch sử hoặc màn hình riêng.
2. Người dùng lọc theo request, status code, thời gian hoặc người thực hiện.
3. Hệ thống truy vấn `RequestHistory` và trả danh sách phù hợp.
4. Người dùng chọn bản ghi để xem chi tiết request/response.

## Luồng thay thế
- **A1: Không có dữ liệu phù hợp**
  - Hệ thống thông báo không tìm thấy kết quả.
- **A2: Người dùng không đủ quyền**
  - Hệ thống chặn truy cập và ghi nhận audit.

## Hậu điều kiện
- Người dùng nắm thông tin lịch sử request cần thiết.

## Bảng dữ liệu liên quan
- `RequestHistory`
