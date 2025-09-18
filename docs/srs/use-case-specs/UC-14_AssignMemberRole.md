# UC-14 Phân quyền thành viên

- **Actor**: Workspace Admin
- **Mục đích**: Thay đổi vai trò giữa viewer, member, admin.

## Điều kiện tiên quyết
- Actor có vai trò Admin hoặc Owner.
- Thành viên mục tiêu không phải Owner (trừ khi Actor là Owner).

## Luồng chính
1. Admin chọn thành viên và vai trò mới.
2. Hệ thống kiểm tra quyền của actor đối với vai trò mong muốn.
3. Hệ thống cập nhật cột `Role` trong `WorkspaceMembers`.
4. Hệ thống ghi audit log cho sự kiện phân quyền.

## Luồng thay thế
- **A1: Không đủ quyền**
  - Hệ thống từ chối khi Admin muốn gán Owner.
- **A2: Vai trò không hợp lệ**
  - Hệ thống thông báo cấu hình vai trò chưa được hỗ trợ.

## Hậu điều kiện
- Vai trò mới có hiệu lực ngay lập tức.

## Bảng dữ liệu liên quan
- `WorkspaceMembers`
- `AuditLogs` (nếu có)
