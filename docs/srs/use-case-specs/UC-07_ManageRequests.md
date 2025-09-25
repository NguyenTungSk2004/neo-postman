# UC-07 Quản lý Request

- **Actor**: Thành viên có quyền ghi trên collection hoặc folder
- **Mục đích**: Tạo, cập nhật, nhân bản và xóa request HTTP cùng các metadata đi kèm.

## Điều kiện tiên quyết
- Collection tồn tại và thuộc workspace của actor.
- Nếu chọn folder thì folder thuộc cùng collection.

## Kích hoạt
- Actor mở cây request và chọn thao tác tương ứng (create/update/duplicate/delete).

## Luồng chính
### Tạo request
1. Actor chọn `New Request` tại collection hoặc folder, nhập `Name`, `Method`, `Url`; có thể nhập `Body` (nullable) và cấu hình `Auth` (json, TODO: chuẩn hóa cấu trúc {"type","credentials"}).
2. Hệ thống xác minh folder (nếu có) thuộc cùng collection.
3. Hệ thống tạo bản ghi `Requests` với các trường trên, `CollectionId`, `FolderId` (nullable), `CreatedAt`, `UpdatedAt`.
4. Hệ thống lưu metadata nếu actor cung cấp:
   - Thêm bản ghi `RequestParams` (`Key`, `Value`, `Type`).
   - Thêm bản ghi `RequestHeaders` (`Key`, `Value`, `IsActive`).
   - Thêm bản ghi `RequestTests` (`Script`).

### Cập nhật request
1. Actor sửa nội dung request hoặc di chuyển sang folder khác cùng collection.
2. Hệ thống xác minh ràng buộc folder/collection như bước tạo.
3. Hệ thống cập nhật `Requests` và thay thế các bản ghi metadata (params/headers/tests) theo thay đổi cần thiết; cập nhật `UpdatedAt`.

### Nhân bản request
1. Actor chọn `Duplicate`.
2. Hệ thống tạo bản ghi `Requests` mới trong collection đích; giữ nguyên `FolderId` nếu hợp lệ.
3. Hệ thống sao chép toàn bộ `RequestParams`, `RequestHeaders`, `RequestTests` sang request mới.

### Xóa request
1. Actor chọn `Delete` và xác nhận.
2. Hệ thống xóa bản ghi `Requests` và cascade tất cả metadata (`RequestParams`, `RequestHeaders`, `RequestTests`, `RequestHistory`).

## Luồng thay thế / lỗi
- **A1: Thiếu trường bắt buộc**: Hệ thống không lưu và thông báo bổ sung `Name`, `Method`, `Url`.
- **A2: Folder không thuộc cùng collection**: Hệ thống từ chối, yêu cầu chọn lại folder hợp lệ.
- **A3: Không đủ quyền**: Hệ thống từ chối thao tác ghi.
- **A4: Cập nhật xung đột**: Nếu `UpdatedAt` hiện tại không khớp, hệ thống yêu cầu tải lại request trước khi lưu.

## Hậu điều kiện
- Request và metadata được lưu đúng bảng dữ liệu chuẩn hóa, sẵn sàng cho việc gửi theo UC-08.

## Bảng dữ liệu liên quan
- `Requests`
- `RequestParams`
- `RequestHeaders`
- `RequestTests`
- `RequestHistory`
- `Folders`
- `Collections`
---
[← Trang trước: UC-06 Quản lý Folder](UC-06_ManageFolders.md) | [Trang sau: UC-08 Gửi request →](UC-08_SendRequest.md)
