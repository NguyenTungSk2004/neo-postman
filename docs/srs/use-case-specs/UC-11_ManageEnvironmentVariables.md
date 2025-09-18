# UC-11 Quản lý biến environment

- **Actor**: Thành viên xác thực
- **Mục đích**: Cấu hình các biến key-value cho environment.

## Điều kiện tiên quyết
- Environment tồn tại và người dùng có quyền chỉnh sửa.

## Luồng chính
1. Người dùng thêm, chỉnh sửa, nhân bản hoặc xóa biến.
2. Hệ thống kiểm tra định dạng key, trạng thái secret.
3. Hệ thống cập nhật `EnvironmentVariables`.
4. Hệ thống trả danh sách biến mới nhất cho client.

## Luồng thay thế
- **A1: Key trùng**
  - Hệ thống từ chối và yêu cầu sử dụng key khác.
- **A2: Giá trị secret trống**
  - Hệ thống cảnh báo và không cho phép lưu nếu chính sách yêu cầu.

## Hậu điều kiện
- Environment chứa tập biến chính xác, sẵn sàng khi gửi request.

## Bảng dữ liệu liên quan
- `EnvironmentVariables`
