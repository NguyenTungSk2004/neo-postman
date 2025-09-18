# UC-04 Tạo workspace

- **Actor**: Thành viên xác thực
- **Mục đích**: Tạo workspace mới và trở thành Owner.

## Điều kiện tiên quyết
- Người dùng đã đăng nhập.
- Workspace mới không vượt quá hạn mức gói dịch vụ.

## Luồng chính
1. Người dùng chọn "Tạo workspace" và nhập tên, mô tả, chế độ hiển thị.
2. Hệ thống kiểm tra tên, quota và quyền của người dùng.
3. Hệ thống tạo bản ghi `Workspaces`.
4. Hệ thống thêm bản ghi `WorkspaceMembers` với vai trò Owner.
5. Hệ thống trả thông tin workspace và chuyển hướng đến dashboard.

## Luồng thay thế
- **A1: Tên trùng**
  - Hệ thống thông báo tên đã tồn tại và yêu cầu đổi tên.
- **A2: Vượt hạn mức**
  - Hệ thống từ chối và đề xuất nâng cấp gói.

## Hậu điều kiện
- Workspace mới được tạo và gắn với người dùng hiện tại.

## Bảng dữ liệu liên quan
- `Workspaces`
- `WorkspaceMembers`
