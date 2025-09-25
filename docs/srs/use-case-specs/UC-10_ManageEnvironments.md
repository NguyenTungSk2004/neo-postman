# UC-10 Quản lý environment

- **Actor**: Thành viên có quyền cấu hình workspace (Member trở lên)
- **Mục đích**: Tạo, cập nhật, nhân bản, xóa environment thuộc một workspace.

## Điều kiện tiên quyết
- Actor thuộc `WorkspaceMembers` của workspace và được cấp quyền ghi (TODO: mapping Role -> quyền).

## Luồng chính
1. Actor mở danh sách environment của workspace và chọn thao tác (create/update/duplicate/delete).
2. Hệ thống đảm bảo mọi bản ghi `Environments` luôn gắn với `WorkspaceId` hiện tại.
3. Khi tạo mới: hệ thống ghi `Name`, `WorkspaceId`, `CreatedAt`, `UpdatedAt` vào `Environments`.
4. Khi cập nhật: hệ thống kiểm tra tên không để trống, cập nhật trường thay đổi và `UpdatedAt`.
5. Khi nhân bản: hệ thống tạo bản ghi `Environments` mới và sao chép các `EnvironmentVariables` (chỉ những bản ghi có `IsActive = true` nếu chính sách yêu cầu).
6. Khi xóa: hệ thống xóa `Environments` và cascade toàn bộ `EnvironmentVariables` thuộc environment đó.

## Luồng thay thế / lỗi
- **A1: Tên để trống hoặc trùng lặp**: Hệ thống từ chối lưu và thông báo người dùng (TODO: xem xét unique index trên `WorkspaceId`, `Name`).
- **A2: Không đủ quyền**: Hệ thống không cho phép thao tác.

## Hậu điều kiện
- Bảng `Environments` phản ánh chính xác danh sách environment của workspace.

## Bảng dữ liệu liên quan
- `Environments`
- `EnvironmentVariables`
---
[← Trang trước: UC-09 Xem lịch sử request](UC-09_ViewRequestHistory.md) | [Trang sau: UC-11 Quản lý biến environment →](UC-11_ManageEnvironmentVariables.md)
