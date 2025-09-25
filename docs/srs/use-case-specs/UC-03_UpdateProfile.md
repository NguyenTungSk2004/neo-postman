# UC-03 Cập nhật hồ sơ

- **Actor**: Người dùng đã đăng nhập
- **Mục đích**: Cập nhật thông tin cá nhân được lưu trong bảng `Users`.

## Điều kiện tiên quyết
- Tài khoản hợp lệ và không bị khóa (`IsDisabled = false`).
- Form hồ sơ tại client đang tải dữ liệu mới nhất.

## Luồng chính
1. Người dùng thay đổi các trường `Name`, `AvatarUrl` hoặc `Email` (nếu được phép).
2. Hệ thống kiểm tra định dạng URL avatar, định dạng email và so sánh với các email tồn tại khác trong `Users`.
3. Hệ thống cập nhật bản ghi `Users`:
   - Ghi giá trị mới cho các trường được phép chỉnh sửa.
   - Nếu đổi email: đặt `EmailVerifiedAt = null`, tạo bản ghi `UserVerificationTokens` mới (`Type = EmailConfirmation`, `UsedAt = null`, `ExpiresAt` mới) và gửi email xác minh lại.
4. Hệ thống cập nhật `UpdatedAt` và trả về thông tin đã đồng bộ.

## Luồng thay thế / lỗi
- **A1: Email trùng lặp**
  - Hệ thống từ chối đổi email, thông báo người dùng chọn email khác.
- **A2: Avatar URL không hợp lệ**
  - Hệ thống yêu cầu nhập lại trước khi cập nhật.
- **A3: Tài khoản đang bị khóa**
  - Hệ thống từ chối thao tác và thông báo trạng thái `IsDisabled`.

## Hậu điều kiện
- Bản ghi `Users` phản ánh thông tin mới nhất.
- Nếu đổi email, trạng thái xác minh mới được thể hiện thông qua token chưa sử dụng trong `UserVerificationTokens`.

## Bảng dữ liệu liên quan
- `Users`
- `UserVerificationTokens` (chỉ khi đổi email)
---
[← Trang trước: UC-02 Đăng nhập](UC-02_Login.md) | [Trang sau: UC-04 Tạo Workspace →](UC-04_CreateWorkspace.md)
