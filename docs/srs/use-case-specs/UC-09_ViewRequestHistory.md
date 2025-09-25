# UC-09 Xem lịch sử request

- **Actor**: Thành viên có quyền xem request
- **Mục đích**: Tra cứu các bản ghi trong `RequestHistory` để theo dõi kết quả thực thi.

## Điều kiện tiên quyết
- Request tồn tại và actor có quyền xem trong workspace.
- Đã có bản ghi trong `RequestHistory` với `RequestId` tương ứng.

## Luồng chính
1. Actor mở tab lịch sử của request hoặc màn hình tổng hợp.
2. Actor lọc theo `ResponseCode`, khoảng thời gian `ExecutedAt`, `ResponseTime` hoặc từ khóa trong `ResponseBody` (nếu hỗ trợ).
3. Hệ thống truy vấn `RequestHistory` theo `RequestId` và các tiêu chí lọc, sắp xếp theo `ExecutedAt` giảm dần.
4. Actor chọn bản ghi để xem chi tiết response và thông tin kèm theo.

## Luồng thay thế / lỗi
- **A1: Không có bản ghi phù hợp**: Hệ thống thông báo và đề nghị điều chỉnh bộ lọc.
- **A2: Không đủ quyền xem**: Hệ thống từ chối truy cập và ghi lại sự kiện audit (nếu có module phụ).

## Hậu điều kiện
- Actor nắm được thông tin lịch sử và có thể tái sử dụng kết quả cho các phân tích tiếp theo.

## Bảng dữ liệu liên quan
- `RequestHistory`
- `Requests`
---
[← Trang trước: UC-08 Gửi request](UC-08_SendRequest.md) | [Trang sau: UC-10 Quản lý environment →](UC-10_ManageEnvironments.md)
