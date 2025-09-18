# UC-15 Phong quyền Admin

- **Actor**: Workspace Owner
- **Mục đích**: Cấp quyền admin cho thành viên cụ thể.

## Điều kiện tiên quyết
- Actor là Owner của workspace.
- Thành viên mục tiêu đang ở vai trò member hoặc viewer.

## Luồng chính
1. Owner chọn thành viên và thao tác "Cấp quyền admin".
2. Hệ thống xác nhận lựa chọn và hiển thị điều khoản trách nhiệm.
3. Hệ thống cập nhật `WorkspaceMembers.Role = 'Admin'`.
4. Hệ thống gửi thông báo tới thành viên được phong quyền.

## Luồng thay thế
- **A1: Thành viên đã là Admin**
  - Hệ thống thông báo không cần thay đổi.
- **A2: Thành viên không hoạt động**
  - Hệ thống yêu cầu kích hoạt lại tài khoản trước khi phong quyền.

## Hậu điều kiện
- Thành viên mục tiêu trở thành Admin và có quyền quản trị.

## Bảng dữ liệu liên quan
- `WorkspaceMembers`
- `Notifications` (nếu có)
