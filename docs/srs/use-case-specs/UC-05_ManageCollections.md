# UC-05 Quản lý collection

- **Actor**: Thành viên xác thực
- **Mục đích**: Thao tác CRUD với collection trong workspace.

## Điều kiện tiên quyết
- Người dùng có quyền member trở lên trong workspace.
- Workspace và collection liên quan đang hoạt động.

## Luồng chính
1. Người dùng chọn thao tác (tạo, chỉnh sửa, nhân bản, xóa).
2. Hệ thống kiểm tra quyền và dữ liệu nhập (tên, mô tả).
3. Hệ thống cập nhật bảng `Collections` tương ứng.
4. Hệ thống trả lại cây collection đã cập nhật.

## Luồng thay thế
- **A1: Không đủ quyền**
  - Hệ thống chặn thao tác và hiển thị thông báo.
- **A2: Xóa collection có dữ liệu con**
  - Hệ thống yêu cầu xác nhận và cascade folder/request.

## Hậu điều kiện
- Danh sách collection phản ánh thao tác mới nhất.

## Bảng dữ liệu liên quan
- `Collections`
- `Folders`
- `Requests`
