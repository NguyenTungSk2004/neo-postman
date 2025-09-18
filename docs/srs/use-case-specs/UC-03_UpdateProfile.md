# UC-03 Cập nhật hồ sơ

- **Actor**: Thành viên xác thực
- **Mục đích**: Thay đổi thông tin cá nhân (tên hiển thị, avatar, múi giờ...).

## Điều kiện tiên quyết
- Người dùng đã đăng nhập.
- Form hồ sơ hiển thị dữ liệu mới nhất.

## Luồng chính
1. Người dùng chỉnh sửa các trường hồ sơ.
2. Hệ thống kiểm tra tính hợp lệ (độ dài, URL avatar...).
3. Hệ thống cập nhật bản ghi trong `Users`.
4. Hệ thống trả dữ liệu mới cho client hiển thị.

## Luồng thay thế
- **A1: Avatar URL không hợp lệ**
  - Hệ thống thông báo lỗi và yêu cầu nhập lại.
- **A2: Cập nhật xung đột**
  - Hệ thống phát hiện version khác và yêu cầu tải lại dữ liệu.

## Hậu điều kiện
- Hồ sơ người dùng được cập nhật và đồng bộ trên mọi thiết bị.

## Bảng dữ liệu liên quan
- `Users`
