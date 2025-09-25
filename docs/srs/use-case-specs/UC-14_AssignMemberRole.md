# UC-14 Phân quyền thành viên

- **Actor**: Workspace Admin (bao gồm Owner)
- **Mục đích**: Điều chỉnh vai trò `Role` của thành viên trong bảng `WorkspaceMembers`.

## Điều kiện tiên quyết
- Actor có `WorkspaceMembers.Role` đủ quyền thay đổi vai trò người khác (TODO: định nghĩa ma trận quyền cho từng role).
- Thành viên mục tiêu đang là thành viên hoạt động của workspace.

## Kích hoạt
- Actor mở danh sách thành viên và chọn `Change Role`.

## Luồng chính
1. Actor chọn thành viên và role mới (`Viewer`, `Member`, `Admin`, `Owner` - TODO: xác nhận đầy đủ giá trị enum `Role`).
2. Hệ thống kiểm tra quyền của actor so với role mục tiêu (ví dụ: chỉ Owner được gán Owner mới).
3. Hệ thống cập nhật cột `Role` trong `WorkspaceMembers` và ghi nhận `UpdatedAt` (nếu áp dụng audit thời gian).
4. Hệ thống có thể ghi nhật ký thay đổi hoặc gửi thông báo theo chính sách ngoài phạm vi schema hiện tại.

## Luồng thay thế / lỗi
- **A1: Actor không đủ quyền**: Hệ thống từ chối và trả về thông báo quyền hạn.
- **A2: Role không hợp lệ**: Hệ thống báo lỗi cấu hình để đội phát triển bổ sung enum.
- **A3: Thành viên mục tiêu không còn trong workspace**: Hệ thống thông báo lỗi dữ liệu và hủy thao tác.

## Hậu điều kiện
- Vai trò thành viên được cập nhật ngay; quyền trong workspace thay đổi tương ứng.

## Bảng dữ liệu liên quan
- `WorkspaceMembers`
- `Users`
---
[← Trang trước: UC-13 Loại thành viên](UC-13_RemoveMember.md) | [Trang sau: UC-15 Phong quyền Admin →](UC-15_PromoteWorkspaceAdmin.md)
