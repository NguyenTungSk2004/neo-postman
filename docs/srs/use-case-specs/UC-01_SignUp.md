# UC-01 Đăng ký

- **Actor**: Người dùng chưa xác thực
- **Mục đích**: Tạo tài khoản Neo Postman mới.

## Điều kiện tiên quyết
- Người dùng chưa đăng nhập hệ thống.
- Email cung cấp chưa tồn tại trong cơ sở dữ liệu.

## Luồng chính
1. Người dùng mở form đăng ký và nhập email, mật khẩu, tên hiển thị.
2. Hệ thống kiểm tra định dạng email và độ mạnh mật khẩu.
3. Hệ thống băm mật khẩu và tạo bản ghi mới trong `Users`.
4. Hệ thống thêm thông tin xác thực vào `UserAuthProviders`.
5. Hệ thống gửi phản hồi thành công và (tuỳ chọn) email xác minh.

## Luồng thay thế
- **A1: Email đã tồn tại**
  - Hệ thống phát hiện email trùng và trả thông báo lỗi.
- **A2: Dữ liệu không hợp lệ**
  - Hệ thống highlight trường sai và yêu cầu nhập lại.

## Hậu điều kiện
- Người dùng mới được tạo và sẵn sàng đăng nhập.
- (Tuỳ chọn) Token xác minh email được sinh ra.

## Bảng dữ liệu liên quan
- `Users`
- `UserAuthProviders`
- `UserVerificationTokens` (nếu bật xác minh email)
