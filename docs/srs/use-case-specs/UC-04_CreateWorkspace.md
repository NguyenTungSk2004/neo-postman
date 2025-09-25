# UC-04 Tạo Workspace

- **Actor**: Người dùng đã xác thực
- **Mục đích**: Khởi tạo workspace mới và gán quyền Owner.

## Điều kiện tiên quyết
- Người dùng đang đăng nhập và không bị khóa.
- Chính sách quota (nếu có) cho phép tạo thêm workspace.

## Luồng chính
1. Actor chọn `New Workspace` và nhập `Name` (bắt buộc), `Description` (nullable) cho workspace mới.
2. Hệ thống kiểm tra quyền tạo và xác nhận tên không để trống.
3. Hệ thống tạo bản ghi `Workspaces` với thông tin nhập và thời điểm `CreatedAt`, `UpdatedAt` hiện tại.
4. Hệ thống tạo bản ghi `WorkspaceMembers` liên kết người dùng hiện tại, đặt `Role = Owner` (TODO: danh sách enum role) và ghi nhận thời điểm tạo.
5. Hệ thống trả về workspace vừa tạo trong danh sách của người dùng.

## Luồng thay thế / lỗi
- **A1: Thiếu tên**
  - Hệ thống yêu cầu nhập `Name` hợp lệ trước khi lưu.
- **A2: Vượt quota**
  - Hệ thống từ chối và đề xuất nâng cấp gói nếu có.

## Hậu điều kiện
- Workspace mới tồn tại trong `Workspaces`.
- Actor có bản ghi `WorkspaceMembers` với vai trò Owner cho workspace này.

## Bảng dữ liệu liên quan
- `Workspaces`
- `WorkspaceMembers`
---
[← Trang trước: UC-03 Cập nhật hồ sơ](UC-03_UpdateProfile.md) | [Trang sau: UC-05 Quản lý Collection →](UC-05_ManageCollections.md)
