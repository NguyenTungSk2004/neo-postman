# UC-06 Quản lý Folder

- **Actor**: Thành viên có quyền ghi trên collection
- **Mục đích**: Tổ chức request bằng cây folder trong một collection.

## Điều kiện tiên quyết
- Collection tồn tại trong workspace và actor là thành viên được phép ghi.

## Kích hoạt
- Actor thao tác trên giao diện cây collection (tạo, đổi tên, di chuyển, xóa folder).

## Luồng chính
### Tạo folder
1. Actor chọn `New Folder`, nhập `Name` và chọn `ParentId` (nullable) thuộc cùng collection.
2. Hệ thống xác minh quyền và đảm bảo `ParentId` (nếu có) liên kết đến `Folders.Id` cùng `CollectionId`.
3. Hệ thống tạo bản ghi `Folders` với `CollectionId`, `ParentId`, `CreatedAt`, `UpdatedAt`.

### Cập nhật hoặc di chuyển folder
1. Actor đổi `Name` hoặc chọn lại `ParentId`.
2. Hệ thống đảm bảo không tạo vòng lặp (Parent không là chính nó hoặc con của nó).
3. Hệ thống cập nhật bản ghi `Folders`, bao gồm `UpdatedAt`.

### Xóa folder
1. Actor chọn `Delete` và xác nhận.
2. Hệ thống xóa folder, các folder con và toàn bộ `Requests` nằm trong cây đó; các bản ghi `RequestParams`, `RequestHeaders`, `RequestTests`, `RequestHistory` liên quan cũng được xóa.

## Luồng thay thế / lỗi
- **A1: Thiếu tên**: Hệ thống yêu cầu nhập `Name` hợp lệ.
- **A2: Vòng lặp cây**: Hệ thống từ chối nếu `ParentId` tạo quan hệ không hợp lệ.
- **A3: Không đủ quyền**: Hệ thống từ chối thao tác.

## Hậu điều kiện
- Cấu trúc `Folders` và `Requests` trong collection được cập nhật đúng với thao tác.

## Bảng dữ liệu liên quan
- `Folders`
- `Requests`
- `RequestParams`
- `RequestHeaders`
- `RequestTests`
- `RequestHistory`
---
[← Trang trước: UC-05 Quản lý Collection](UC-05_ManageCollections.md) | [Trang sau: UC-07 Quản lý Request →](UC-07_ManageRequests.md)
