# UC-13 Loại thành viên

- **Actor**: Workspace Admin
- **Mục đích**: Gỡ thành viên ra khỏi workspace.

## Điều kiện tiên quyết
- Actor có vai trò Admin hoặc Owner.
- Thành viên cần gỡ không phải Owner duy nhất.

## Luồng chính
1. Admin chọn thành viên cần gỡ và xác nhận hành động.
2. Hệ thống kiểm tra vai trò của thành viên đó.
3. Hệ thống xóa bản ghi tương ứng trong `WorkspaceMembers`.
4. Hệ thống (tuỳ chọn) thông báo qua email cho thành viên bị gỡ.

## Luồng thay thế
- **A1: Gỡ Owner duy nhất**
  - Hệ thống từ chối và yêu cầu chuyển quyền trước.
- **A2: Thành viên không tồn tại**
  - Hệ thống thông báo lỗi đồng bộ dữ liệu.

## Hậu điều kiện
- Thành viên bị gỡ không còn truy cập workspace.

## Bảng dữ liệu liên quan
- `WorkspaceMembers`
