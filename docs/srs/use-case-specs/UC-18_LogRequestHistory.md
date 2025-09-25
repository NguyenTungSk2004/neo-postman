# UC-18 Ghi log lịch sử request

- **Actor**: Hệ thống
- **Mục đích**: Ghi nhận kết quả sau mỗi lần gửi request để phục vụ truy vết.

## Điều kiện tiên quyết
- Việc gửi request đã hoàn tất (thành công hoặc thất bại).
- Engine nhận được metadata phản hồi.

## Luồng chính
1. Engine nhận kết quả từ thao tác gửi request.
2. Hệ thống tổng hợp thông tin (`RequestId`, `ResponseCode`, `ResponseTime`, `ResponseBody`).
3. Hệ thống insert bản ghi vào `RequestHistory` với `ExecutedAt` là thời điểm hoàn tất.
4. Hệ thống phát sự kiện nội bộ để cập nhật UI thời gian thực (nếu có cơ chế push).

## Luồng thay thế
- **A1: Ghi log thất bại**
  - Hệ thống retry theo chính sách, nếu vẫn thất bại thì ghi log lỗi nội bộ.

## Hậu điều kiện
- Bản ghi lịch sử mới xuất hiện và sẵn sàng cho người dùng xem.

## Bảng dữ liệu liên quan
- `RequestHistory`
- `Requests`
---
[← Trang trước: UC-17 Chuyển quyền Owner](UC-17_TransferOwnership.md) | [Trang sau: Mô hình thực thể →](../entity-models.md)
