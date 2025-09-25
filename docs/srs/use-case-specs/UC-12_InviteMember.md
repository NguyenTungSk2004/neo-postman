# UC-12 Mời thành viên

- **Actor**: Workspace Admin hoặc Owner
- **Mục đích**: Tạo lời mời tham gia workspace và quản lý trạng thái lời mời.

## Điều kiện tiên quyết
- Actor có bản ghi `WorkspaceMembers` với `Role` được phép mời người khác (Owner/Admin).
- Email người được mời hợp lệ.

## Kích hoạt
- Actor mở danh sách members và chọn `Invite`.

## Luồng chính
1. Actor nhập `InviteeEmail`, chọn `Role` mong muốn (TODO: danh sách enum role), có thể thêm ghi chú gửi email.
2. Hệ thống kiểm tra email không nằm trong `WorkspaceMembers` hiện tại và không có lời mời `WorkspaceInvites` đang `Status = Pending` còn hiệu lực.
3. Hệ thống tạo bản ghi `WorkspaceInvites` với các trường: `WorkspaceId`, `InviterId`, `InviteeEmail`, `Role`, `Token`, `Status = Pending`, `CreatedAt`, `ExpiresAt`.
4. Hệ thống gửi email kèm token cho người được mời.
5. Khi người được mời chấp nhận:
   - Hệ thống xác thực token, đảm bảo `ExpiresAt` chưa quá hạn và `Status = Pending`.
   - Nếu người đó đã có `Users.Id`: tạo bản ghi `WorkspaceMembers` mới với `UserId`, `Role` chọn ở bước 1.
   - Nếu chưa có tài khoản: lưu token đến khi hoàn tất UC-01 và liên kết `UserId` sau khi xác minh email.
   - Cập nhật `WorkspaceInvites.Status = Accepted` và, nếu cần, `ExpiresAt`.

## Luồng thay thế / lỗi
- **A1: Email đã là thành viên**: Hệ thống từ chối tạo lời mời.
- **A2: Lời mời hết hạn**: Khi người được mời truy cập, hệ thống cập nhật `Status = Expired` và yêu cầu actor gửi lời mời mới.
- **A3: Tài khoản bị khóa**: Nếu người dùng tồn tại nhưng `Users.IsDisabled = true`, hệ thống từ chối thêm thành viên và thông báo hỗ trợ.

## Hậu điều kiện
- Lời mời hợp lệ tồn tại trong `WorkspaceInvites` và có thể được theo dõi trạng thái.
- Khi chấp nhận thành công, có bản ghi tương ứng trong `WorkspaceMembers`.

## Bảng dữ liệu liên quan
- `WorkspaceInvites`
- `WorkspaceMembers`
- `Users`
---
[← Trang trước: UC-11 Quản lý biến environment](UC-11_ManageEnvironmentVariables.md) | [Trang sau: UC-13 Loại thành viên →](UC-13_RemoveMember.md)
