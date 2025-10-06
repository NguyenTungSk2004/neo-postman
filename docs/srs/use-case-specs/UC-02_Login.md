# UC-02 Đăng nhập

- **Actor**: Người dùng đã đăng ký
- **Mục đích**: Xác thực và khởi tạo phiên làm việc an toàn.

## Điều kiện tiên quyết
- Tài khoản tồn tại trong `Users`.
- Có bản ghi `UserAuthProviders` tương ứng với `Provider = Local` (TODO: liệt kê đầy đủ giá trị enum).

## Luồng chính
1. Người dùng nhập `Email`, `Password`, thông tin thiết bị (nếu thu thập) và gửi yêu cầu.
2. Hệ thống tìm `Users` theo email và kiểm tra:
   - `IsDisabled = false`.
   - `EmailVerifiedAt` khác null nếu chính sách yêu cầu xác minh trước khi đăng nhập.
3. Hệ thống lấy bản ghi `UserAuthProviders` theo `UserId` và so khớp mật khẩu băm.
4. Hệ thống sinh access token và refresh token (raw); refresh token chỉ được lưu bản sao băm trong cơ sở dữ liệu.
5. Hệ thống tạo bản ghi mới trong `UserSessions` gồm:
   - `UserId`.
   - `RefreshTokenHash`.
   - `DeviceInfo`, `IpAddress` từ yêu cầu.
   - `CreatedAt`, `UpdatedAt`, `ExpiresAt` theo chính sách phiên.
6. Hệ thống trả về thông tin đăng nhập thành công (access token, refresh token raw, thông tin phiên) cho client.

## Luồng thay thế / lỗi
- **A1: Email không tồn tại hoặc mật khẩu sai**
  - Hệ thống từ chối bước 4 và không tiết lộ thông tin chi tiết.
- **A2: Tài khoản bị khóa (`IsDisabled = true`)**
  - Hệ thống từ chối và thông báo người dùng liên hệ hỗ trợ.
- **A3: Email chưa xác minh**
  - Hệ thống yêu cầu người dùng hoàn tất xác minh, có thể gửi lại token qua `UserVerificationTokens` (cập nhật `ExpiresAt`).
- **A4: Chính sách một phiên duy nhất**
  - Nếu hệ thống chỉ cho phép một phiên, bước 5 cập nhật `UserSessions` cũ với `UpdatedAt` mới thay vì tạo bản ghi mới.

## Hậu điều kiện
- Tồn tại hoặc được cập nhật một bản ghi hợp lệ trong `UserSessions` với thời gian hết hạn rõ ràng.
- Lịch sử truy cập có thể được theo dõi thông qua `UserSessions.UpdatedAt` và IP gần nhất.

## Bảng dữ liệu liên quan
- `Users`
- `UserAuthProviders`
- `UserSessions`
- `UserVerificationTokens` (chỉ khi cần gửi lại token)
---
[← Trang trước: UC-01 Đăng ký](UC-01_SignUp.md) | [Trang sau: UC-03 Cập nhật hồ sơ →](UC-03_UpdateProfile.md)
