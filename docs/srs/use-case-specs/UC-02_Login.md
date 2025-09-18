# UC-02 Đăng nhập

- **Actor**: Người dùng chưa xác thực
- **Mục đích**: Xác thực để truy cập workspace.

## Điều kiện tiên quyết
- Người dùng đã đăng ký và (nếu cần) xác minh email.

## Luồng chính
1. Người dùng nhập email và mật khẩu trên form đăng nhập.
2. Hệ thống tìm bản ghi trong `UserAuthProviders` và so khớp mật khẩu băm.
3. Hệ thống sinh access token và refresh token.
4. Hệ thống tạo bản ghi phiên trong `UserSessions` (bao gồm thông tin thiết bị).
5. Hệ thống trả token và thông tin hồ sơ về client.

## Luồng thay thế
- **A1: Sai thông tin đăng nhập**
  - Hệ thống tăng bộ đếm thất bại và thông báo lỗi chung.
- **A2: Tài khoản bị khóa**
  - Hệ thống từ chối đăng nhập, hướng dẫn reset mật khẩu hoặc liên hệ hỗ trợ.

## Hậu điều kiện
- Phiên đăng nhập hợp lệ được kích hoạt.
- Refresh token được lưu để gia hạn phiên.

## Bảng dữ liệu liên quan
- `UserAuthProviders`
- `UserSessions`
