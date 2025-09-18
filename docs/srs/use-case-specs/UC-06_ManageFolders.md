# UC-06 Quản lý folder

- **Actor**: Thành viên xác thực
- **Mục đích**: Sắp xếp request thông qua folder trong collection.

## Điều kiện tiên quyết
- Người dùng có quyền truy cập collection.
- Collection tồn tại.

## Luồng chính
1. Người dùng thêm, đổi tên, di chuyển hoặc xóa folder.
2. Hệ thống kiểm tra cấu trúc cây không tạo vòng lặp.
3. Hệ thống ghi nhận thay đổi vào `Folders`.
4. Hệ thống trả cây folder mới cho client.

## Luồng thay thế
- **A1: Di chuyển tạo vòng lặp**
  - Hệ thống từ chối và giữ trạng thái cũ.
- **A2: Xóa folder chứa request**
  - Hệ thống yêu cầu xác nhận trước khi cascade xóa.

## Hậu điều kiện
- Cấu trúc folder cập nhật theo yêu cầu người dùng.

## Bảng dữ liệu liên quan
- `Folders`
- `Requests`
