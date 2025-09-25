# UC-15 Phong quyền Admin

- **Actor**: Workspace Owner
- **Mục đích**: Cấp quyền admin cho thành viên cụ thể trong `WorkspaceMembers`.

## Điều kiện tiên quyết
- Actor là Owner của workspace.
- Thành viên mục tiêu đang ở vai trò `Viewer` hoặc `Member` và tồn tại trong `WorkspaceMembers`.

## Kích hoạt
- Owner mở danh sách thành viên và chọn `Promote to Admin`.

## Luồng chính
1. Owner chọn thành viên, xác nhận trách nhiệm của vai trò Admin.
2. Hệ thống cập nhật `WorkspaceMembers.Role = Admin` và ghi nhận `UpdatedAt`.
3. Hệ thống có thể gửi thông báo cho thành viên được phong quyền theo cơ chế thông báo chung (ngoài phạm vi schema hiện tại).

## Luồng thay thế / lỗi
- **A1: Thành viên đã là Admin hoặc Owner**: Hệ thống thông báo không cần thay đổi.
- **A2: Thành viên không hoạt động hoặc bị khóa tài khoản (`Users.IsDisabled = true`)**: Hệ thống từ chối và yêu cầu kích hoạt tài khoản trước.

## Hậu điều kiện
- Thành viên mục tiêu sở hữu quyền admin trong workspace.

## Bảng dữ liệu liên quan
- `WorkspaceMembers`
- `Users`
---
[← Trang trước: UC-14 Phân quyền thành viên](UC-14_AssignMemberRole.md) | [Trang sau: UC-16 Xóa Workspace →](UC-16_DeleteWorkspace.md)
