# UC-01 Đăng ký

- **Actor**: Khách chưa đăng nhập
- **Mục đích**: Tạo tài khoản Neo Postman mới và khởi tạo thông tin xác minh email.

## Điều kiện tiên quyết
- Email cung cấp chưa tồn tại trong `Users.Email`.
- Cơ sở dữ liệu sẵn sàng ghi nhận bản ghi mới.

## Luồng chính
1. Khách nhập `Name`, `Email`, mật khẩu và thông tin thiết bị ban đầu (nếu có).
2. Hệ thống kiểm tra tính hợp lệ của email và độ mạnh mật khẩu theo chính sách hiện hành.
3. Hệ thống tạo bản ghi `Users` với các giá trị mặc định:
   - `IsDisabled = false`.
   - `EmailVerifiedAt = null`.
   - Ghi nhận `CreatedAt`, `UpdatedAt` theo thời điểm tạo.
4. Hệ thống băm mật khẩu và lưu vào `UserAuthProviders` (trường `Provider`, `PasswordHash`, `PasswordSalt`, `CreatedAt`, `UpdatedAt`).
5. Hệ thống tạo bản ghi `UserVerificationTokens` với `Type = EmailConfirmation` (TODO: liệt kê đầy đủ giá trị enum), `Token`, `ExpiresAt`, `UsedAt = null` và liên kết `UserId` vừa tạo.
6. Hệ thống gửi email xác minh chứa liên kết token tới địa chỉ người dùng cung cấp.
7. Hệ thống trả về thông điệp thành công, thông báo người dùng cần xác minh email trước khi đăng nhập (nếu chính sách bắt buộc).

## Luồng thay thế
- **A1: Email đã tồn tại**
  - Hệ thống từ chối bước 3 và thông báo email đã được sử dụng.
- **A2: Mật khẩu không đạt chuẩn**
  - Hệ thống yêu cầu nhập lại mật khẩu thỏa điều kiện.
- **A3: Gửi email xác minh thất bại**
  - Hệ thống vẫn giữ lại bản ghi dữ liệu nhưng thông báo người dùng thử lại hoặc liên hệ hỗ trợ.

## Hậu điều kiện
- Tài khoản được lưu trong `Users` với trạng thái chưa xác minh email.
- Tồn tại token xác minh hợp lệ trong `UserVerificationTokens` cho tài khoản mới.

## Bảng dữ liệu liên quan
- `Users`
- `UserAuthProviders`
- `UserVerificationTokens`
---
[← Trang trước: UseCaseChanges](../UseCaseChanges.md) | [Trang sau: UC-02 Đăng nhập →](UC-02_Login.md)
