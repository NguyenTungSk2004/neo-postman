# UC-10 Quản lý environment

- **Actor**: Thành viên xác thực
- **Mục đích**: Tạo, chỉnh sửa, xóa environment phục vụ request.

## Điều kiện tiên quyết
- Người dùng là thành viên của workspace.

## Luồng chính
1. Người dùng mở danh sách environment và chọn thao tác (tạo/sửa/clone/xóa).
2. Hệ thống kiểm tra quyền và ràng buộc tên trùng.
3. Hệ thống cập nhật bảng `Environments`.
4. Hệ thống trả danh sách environment mới nhất.

## Luồng thay thế
- **A1: Tên environment trùng**
  - Hệ thống thông báo lỗi và yêu cầu đổi tên.
- **A2: Environment đang được scheduler sử dụng**
  - Hệ thống cảnh báo và không cho phép xóa (tuỳ tính năng tương lai).

## Hậu điều kiện
- Danh sách environment phản ánh thay đổi vừa thực hiện.

## Bảng dữ liệu liên quan
- `Environments`
