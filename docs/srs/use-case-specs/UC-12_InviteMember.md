# UC-12 Mời thành viên

- **Actor**: Workspace Admin
- **Mục đích**: Thêm người dùng mới vào workspace.

## Điều kiện tiên quyết
- Actor có vai trò Admin hoặc Owner trong workspace.
- Người được mời có email hợp lệ.

## Luồng chính
1. Admin nhập email và vai trò mong muốn.
2. Hệ thống kiểm tra email đã tồn tại trong `Users` hay chưa.
3. Hệ thống tạo bản ghi trong `WorkspaceMembers` ở trạng thái "pending" (nếu cần).
4. Hệ thống gửi email lời mời kèm liên kết tham gia.
5. Người được mời chấp nhận, hệ thống cập nhật trạng thái thành "active".

## Luồng thay thế
- **A1: Email đã trong workspace**
  - Hệ thống thông báo người dùng đã là thành viên.
- **A2: Email chưa đăng ký**
  - Hệ thống gửi lời mời đăng ký và join sau khi tạo tài khoản.

## Hậu điều kiện
- Workspace có thêm thành viên mới với vai trò mong muốn.

## Bảng dữ liệu liên quan
- `WorkspaceMembers`
- `Users`
- `WorkspaceInvitations` (nếu tách bảng lời mời)
