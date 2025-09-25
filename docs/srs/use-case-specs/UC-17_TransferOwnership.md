# UC-17 Chuyển quyền Owner

- **Actor**: Workspace Owner hiện tại
- **Mục đích**: Giao quyền sở hữu workspace cho thành viên khác.

## Điều kiện tiên quyết
- Thành viên nhận quyền đang tồn tại trong `WorkspaceMembers` với trạng thái hoạt động.
- Actor đã xác thực đa yếu tố nếu chính sách yêu cầu.

## Kích hoạt
- Owner mở danh sách thành viên và chọn `Transfer Ownership`.

## Luồng chính
1. Owner chọn thành viên nhận quyền và xác nhận cảnh báo mất quyền Owner.
2. Hệ thống yêu cầu xác minh bổ sung (OTP/email) và kiểm tra thành viên mục tiêu không bị khóa (`Users.IsDisabled = false`).
3. Hệ thống cập nhật `WorkspaceMembers`:
   - Gán `Role = Owner` cho thành viên nhận quyền.
   - Hạ role của Owner cũ xuống `Admin` (hoặc `Member` nếu owner chọn) để đảm bảo vẫn còn bản ghi thành viên.
4. Hệ thống ghi nhận sự kiện chuyển giao (ghi chú nội bộ hoặc thông báo theo cơ chế ngoài phạm vi schema hiện tại).

## Luồng thay thế / lỗi
- **A1: Thành viên chưa chấp nhận lời mời**: Hệ thống không tìm thấy bản ghi `WorkspaceMembers`, yêu cầu actor mời và chờ thành viên chấp nhận trước.
- **A2: Xác minh thất bại**: Quy trình dừng, các role giữ nguyên.
- **A3: Thành viên mục tiêu bị khóa**: Nếu `Users.IsDisabled = true`, hệ thống từ chối và yêu cầu kích hoạt lại tài khoản.

## Hậu điều kiện
- Thành viên mới giữ vai trò Owner.
- Owner cũ mất quyền Owner nhưng vẫn là thành viên workspace.

## Bảng dữ liệu liên quan
- `WorkspaceMembers`
- `Users`
- `WorkspaceInvites` (dùng để xác minh trạng thái nếu thành viên chưa hiện diện)
---
[← Trang trước: UC-16 Xóa Workspace](UC-16_DeleteWorkspace.md) | [Trang sau: UC-18 Ghi log lịch sử request →](UC-18_LogRequestHistory.md)
