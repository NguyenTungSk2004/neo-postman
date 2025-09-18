# UC-07 Quản lý request

- **Actor**: Thành viên xác thực
- **Mục đích**: Tạo và duy trì request HTTP trong collection/folder.

## Điều kiện tiên quyết
- Người dùng có quyền ghi trên collection hoặc folder.
- Các tham số hỗ trợ (method, environment) sẵn sàng.

## Luồng chính
1. Người dùng thực hiện hành động (tạo, chỉnh sửa, nhân bản, xóa).
2. Hệ thống kiểm tra quyền và dữ liệu bắt buộc (tên, method, URL).
3. Hệ thống cập nhật `Requests` và các bảng con `RequestHeaders`, `RequestParams`, `RequestTests`.
4. Hệ thống trả thông tin request cập nhật.

## Luồng thay thế
- **A1: Thiếu dữ liệu bắt buộc**
  - Hệ thống trả cảnh báo và yêu cầu bổ sung.
- **A2: Bị khóa do người khác đang chỉnh sửa**
  - Hệ thống trả thông báo chỉ cho phép xem.

## Hậu điều kiện
- Danh mục request được cập nhật và (nếu có) lưu version mới.

## Bảng dữ liệu liên quan
- `Requests`
- `RequestHeaders`
- `RequestParams`
- `RequestTests`
