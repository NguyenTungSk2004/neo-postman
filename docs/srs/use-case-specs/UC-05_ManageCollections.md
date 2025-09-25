# UC-05 Quản lý Collection

- **Actor**: Thành viên có quyền ghi (Member trở lên)
- **Mục đích**: Tạo, cập nhật, nhân bản và xóa collection trong workspace.

## Điều kiện tiên quyết
- Actor thuộc `WorkspaceMembers` của workspace được chọn và có `Role` được phép ghi (TODO: mapping Role -> quyền).
- Workspace đang hoạt động.

## Kích hoạt
- Actor mở màn hình quản lý collection và chọn thao tác tương ứng.

## Luồng chính
### Tạo collection
1. Actor chọn `New Collection`, nhập `Name` (bắt buộc) và `Description` (nullable).
2. Hệ thống xác minh quyền và tên không để trống.
3. Hệ thống tạo bản ghi `Collections` với `WorkspaceId` của context hiện tại và thời gian `CreatedAt`, `UpdatedAt`.

### Cập nhật collection
1. Actor chỉnh sửa `Name` hoặc `Description`.
2. Hệ thống kiểm tra quyền ghi.
3. Hệ thống cập nhật trường thay đổi và `UpdatedAt` trong `Collections`.

### Nhân bản collection
1. Actor chọn `Duplicate`.
2. Hệ thống tạo bản ghi `Collections` mới trong cùng workspace.
3. Hệ thống sao chép cây `Folders`, `Requests` và các bản ghi con (`RequestParams`, `RequestHeaders`, `RequestTests`) nếu actor chọn sao chép nội dung.

### Xóa collection
1. Actor chọn `Delete` và xác nhận thông báo cascade.
2. Hệ thống xóa bản ghi `Collections` và toàn bộ dữ liệu liên kết: `Folders`, `Requests`, `RequestParams`, `RequestHeaders`, `RequestTests`, `RequestHistory`.

## Luồng thay thế / lỗi
- **A1: Thiếu tên**: Hệ thống yêu cầu nhập `Name`.
- **A2: Không đủ quyền**: Hệ thống từ chối thao tác và thông báo role không hợp lệ.

## Hậu điều kiện
- Cây collection của workspace phản ánh chính xác thao tác vừa thực hiện.

## Bảng dữ liệu liên quan
- `Collections`
- `Folders`
- `Requests`
- `RequestParams`
- `RequestHeaders`
- `RequestTests`
- `RequestHistory`
---
[← Trang trước: UC-04 Tạo Workspace](UC-04_CreateWorkspace.md) | [Trang sau: UC-06 Quản lý Folder →](UC-06_ManageFolders.md)
