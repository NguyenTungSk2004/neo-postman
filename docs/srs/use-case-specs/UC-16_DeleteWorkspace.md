# UC-16 Xóa workspace

- **Actor**: Workspace Owner
- **Mục đích**: Xóa vĩnh viễn workspace và dữ liệu liên quan.

## Điều kiện tiên quyết
- Actor là Owner hợp lệ của workspace.
- Không còn nghĩa vụ lưu trữ (theo chính sách doanh nghiệp).

## Luồng chính
1. Owner mở màn hình cài đặt workspace và chọn "Xóa".
2. Hệ thống yêu cầu xác nhận hai bước (gõ tên workspace, xác nhận email/OTP).
3. Hệ thống đặt cờ xóa mềm (nếu áp dụng) hoặc tiến hành xóa vật lý.
4. Hệ thống lần lượt xóa `Collections`, `Folders`, `Requests`, `Environments`, `WorkspaceMembers`, `RequestHistory`.
5. Hệ thống gửi thông báo hoàn tất và chuyển hướng người dùng ra trang tổng quan.

## Luồng thay thế
- **A1: Workspace đang bị khóa**
  - Hệ thống thông báo đang có deployment hoặc kiểm thử, tạm hoãn xóa.
- **A2: Xác nhận thất bại**
  - Hệ thống dừng quy trình nếu OTP sai hoặc người dùng hủy.

## Hậu điều kiện
- Workspace không còn tồn tại (hoặc chuyển sang trạng thái archived nếu xóa mềm).
- Mọi thành viên mất quyền truy cập dữ liệu liên quan.

## Bảng dữ liệu liên quan
- `Workspaces`
- `Collections`
- `Folders`
- `Requests`
- `Environments`
- `WorkspaceMembers`
- `RequestHistory`
