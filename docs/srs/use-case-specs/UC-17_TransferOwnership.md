# UC-17 Chuyển quyền Owner

- **Actor**: Workspace Owner
- **Mục đích**: Chuyển quyền sở hữu workspace cho thành viên khác.

## Điều kiện tiên quyết
- Actor hiện là Owner của workspace.
- Thành viên nhận quyền đã ở trạng thái active trong workspace.

## Luồng chính
1. Owner mở danh sách thành viên và chọn "Chuyển quyền sở hữu".
2. Hệ thống yêu cầu xác nhận (OTP/email) và hiển thị cảnh báo về mất quyền.
3. Hệ thống cập nhật `WorkspaceMembers` để gán role Owner cho người nhận.
4. Hệ thống hạ vai trò của Owner cũ xuống Admin hoặc Member (tùy lựa chọn).
5. Hệ thống gửi thông báo cho cả hai bên và ghi log audit.

## Luồng thay thế
- **A1: Thành viên mục tiêu không đủ điều kiện**
  - Hệ thống từ chối nếu thành viên bị suspended hoặc chưa chấp nhận lời mời.
- **A2: Xác nhận thất bại**
  - Quy trình dừng và không thay đổi vai trò.

## Hậu điều kiện
- Thành viên mới trở thành Owner và có toàn quyền quản trị.
- Owner cũ bị hạ quyền theo cấu hình lựa chọn.

## Bảng dữ liệu liên quan
- `WorkspaceMembers`
- `AuditLogs`
- `Notifications`
