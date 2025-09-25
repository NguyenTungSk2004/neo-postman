# UC-16 Xóa Workspace

- **Actor**: Workspace Owner
- **Mục đích**: Xóa vĩnh viễn workspace và dữ liệu kèm theo.

## Điều kiện tiên quyết
- Actor là Owner hợp lệ của workspace.
- Không còn yêu cầu lưu trữ theo chính sách tổ chức.

## Kích hoạt
- Owner truy cập trang cài đặt workspace và chọn `Delete Workspace`.

## Luồng chính
1. Owner nhập lại tên workspace và xác nhận OTP/email tùy chính sách.
2. Hệ thống khóa workspace, ngăn thao tác mới trong khi xử lý.
3. Hệ thống lần lượt xóa dữ liệu liên quan theo thứ tự an toàn: `EnvironmentVariables`, `Environments`, `RequestParams`, `RequestHeaders`, `RequestTests`, `RequestHistory`, `Requests`, `Folders`, `Collections`, `WorkspaceMembers`, `WorkspaceInvites`.
4. Hệ thống xóa bản ghi `Workspaces`.
5. Hệ thống gửi thông báo hoàn tất và chuyển hướng actor ra trang danh sách workspace.

## Luồng thay thế / lỗi
- **A1: Xác nhận thất bại**: Hệ thống dừng quy trình nếu OTP sai hoặc người dùng hủy.
- **A2: Workspace đang bị khóa bởi tác vụ khác**: Hệ thống báo lỗi và yêu cầu thử lại khi tác vụ kết thúc.

## Hậu điều kiện
- Workspace và dữ liệu con không còn tồn tại (hoặc chuyển sang trạng thái archived nếu áp dụng soft delete cấp hệ thống).
- Thành viên mất quyền truy cập workspace.

## Bảng dữ liệu liên quan
- `Workspaces`
- `WorkspaceMembers`
- `WorkspaceInvites`
- `Collections`
- `Folders`
- `Requests`
- `RequestParams`
- `RequestHeaders`
- `RequestTests`
- `RequestHistory`
- `Environments`
- `EnvironmentVariables`
---
[← Trang trước: UC-15 Phong quyền Admin](UC-15_PromoteWorkspaceAdmin.md) | [Trang sau: UC-17 Chuyển quyền Owner →](UC-17_TransferOwnership.md)
