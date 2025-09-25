# UC-13 Loại thành viên

- **Actor**: Workspace Admin hoặc Owner
- **Mục đích**: Gỡ quyền truy cập của thành viên khỏi workspace.

## Điều kiện tiên quyết
- Actor có bản ghi `WorkspaceMembers` với `Role` có quyền quản trị (Owner/Admin).
- Thành viên mục tiêu đang tồn tại trong `WorkspaceMembers` và không phải Owner duy nhất.

## Kích hoạt
- Actor mở danh sách thành viên và chọn `Remove`.

## Luồng chính
1. Actor xác nhận thao tác kèm cảnh báo mất quyền.
2. Hệ thống kiểm tra vai trò hiện tại của thành viên (không cho phép xóa Owner duy nhất).
3. Hệ thống xóa bản ghi khỏi `WorkspaceMembers` tương ứng với `WorkspaceId`, `UserId`.
4. Hệ thống (tùy chọn) gửi thông báo email cho người bị loại và ghi nhận sự kiện audit.

## Luồng thay thế / lỗi
- **A1: Người bị chọn là Owner duy nhất**: Hệ thống từ chối, đề nghị chuyển quyền Owner trước.
- **A2: Thành viên không tồn tại**: Hệ thống thông báo lỗi dữ liệu và dừng thao tác.

## Hậu điều kiện
- Thành viên bị loại không còn quyền truy cập workspace; mọi quyền liên quan đến collection, request trong workspace cũng mất hiệu lực.

## Bảng dữ liệu liên quan
- `WorkspaceMembers`
- `Users`
---
[← Trang trước: UC-12 Mời thành viên](UC-12_InviteMember.md) | [Trang sau: UC-14 Phân quyền thành viên →](UC-14_AssignMemberRole.md)
