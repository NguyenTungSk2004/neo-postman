# UC-11 Quản lý biến environment

- **Actor**: Thành viên có quyền ghi trên environment
- **Mục đích**: Thêm, cập nhật, nhân bản, xóa biến key-value trong `EnvironmentVariables`.

## Điều kiện tiên quyết
- Environment tồn tại và thuộc workspace của actor.

## Luồng chính
1. Actor quản lý danh sách biến: thêm bản ghi mới, sửa `Key`, `Value`, đổi trạng thái `IsActive`, hoặc xóa.
2. Hệ thống kiểm tra `Key` không để trống và, nếu chính sách áp dụng, không trùng lặp trong cùng environment.
3. Mọi thay đổi được lưu vào `EnvironmentVariables` với các trường: `EnvironmentId`, `Key`, `Value`, `IsActive`.
4. Hệ thống trả về danh sách biến mới nhất để đồng bộ với client.

## Luồng thay thế / lỗi
- **A1: Key trùng lặp**: Hệ thống từ chối thêm/sửa và thông báo (TODO: cân nhắc unique index trên `EnvironmentId`, `Key`).
- **A2: Không đủ quyền**: Hệ thống từ chối thao tác ghi.

## Hậu điều kiện
- Tập biến environment phù hợp với trạng thái `IsActive` và sẵn sàng sử dụng tại UC-08.

## Bảng dữ liệu liên quan
- `EnvironmentVariables`
- `Environments`
---
[← Trang trước: UC-10 Quản lý environment](UC-10_ManageEnvironments.md) | [Trang sau: UC-12 Mời thành viên →](UC-12_InviteMember.md)
