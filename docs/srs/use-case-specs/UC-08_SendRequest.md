# UC-08 Gửi request

- **Actor**: Thành viên xác thực
- **Mục đích**: Thực thi request và xem phản hồi trả về.

## Điều kiện tiên quyết
- Request đã được cấu hình hợp lệ.
- Người dùng có quyền truy cập request.

## Luồng chính
1. Người dùng chọn request và nhấn **Send**.
2. Hệ thống tải environment và thay thế biến từ `EnvironmentVariables` (nếu chọn).
3. Engine thực hiện HTTP call ra ngoài.
4. Hệ thống nhận phản hồi, đo thời gian, ghi log.
5. Hệ thống lưu bản ghi vào `RequestHistory` và hiển thị response cho người dùng.

## Luồng thay thế
- **A1: Thiếu biến**
  - Hệ thống phát hiện biến chưa định nghĩa và thông báo lỗi.
- **A2: Timeout**
  - Hệ thống dừng request, trả thông báo timeout và đề xuất thử lại.
- **A3: SSL/TLS lỗi**
  - Hệ thống cảnh báo lỗi chứng chỉ và cho phép bỏ qua (nếu chính sách cho phép).

## Hậu điều kiện
- Lịch sử request có thêm bản ghi mới.
- Người dùng nhìn thấy response và thông tin meta (status, thời gian, kích thước).

## Bảng dữ liệu liên quan
- `Requests`
- `EnvironmentVariables`
- `RequestHistory`
