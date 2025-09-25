# UC-08 Gửi request

- **Actor**: Thành viên đã xác thực có quyền truy cập request
- **Mục đích**: Thực thi request và lưu kết quả vào lịch sử.

## Điều kiện tiên quyết
- Bản ghi `Requests` tồn tại và được cấu hình hợp lệ.
- Nếu sử dụng environment, `Environments.WorkspaceId` trùng với workspace của request và tồn tại các `EnvironmentVariables` cần thiết.

## Luồng chính
1. Actor chọn request và (nếu cần) chọn environment, hệ thống nạp các `EnvironmentVariables` có `IsActive = true` làm giá trị thay thế.
2. Hệ thống áp dụng params và headers tương ứng: chỉ bao gồm `RequestHeaders` có `IsActive = true` và kết hợp `RequestParams` vào URL hoặc query string theo `Type`.
3. Engine thực thi HTTP call theo cấu hình `Method`, `Url`, `Body`, `Auth` (json, TODO: chuẩn hóa cấu trúc credentials).
4. Hệ thống ghi nhận kết quả (mã phản hồi, thời gian, body) và tạo bản ghi `RequestHistory` với `RequestId`, `ResponseCode`, `ResponseTime`, `ResponseBody` (nullable), `ExecutedAt`.
5. Hệ thống trả về response cho client và cập nhật UI (lịch sử, console, v.v.).

## Luồng thay thế / lỗi
- **A1: Biến thiếu giá trị**
  - Hệ thống thông báo trước khi gửi nếu phát hiện placeholder không thể thay thế.
- **A2: Gọi thất bại (timeout, kết nối, SSL, …)**
  - Hệ thống vẫn ghi `RequestHistory` với thông tin lỗi phù hợp và thông báo người dùng.

## Hậu điều kiện
- Lịch sử thực thi request được bổ sung bản ghi mới trong `RequestHistory`.
- Client có thể tiếp tục phân tích response vừa nhận.

## Bảng dữ liệu liên quan
- `Requests`
- `RequestHeaders`
- `RequestParams`
- `RequestTests`
- `RequestHistory`
- `Environments`
- `EnvironmentVariables`
---
[← Trang trước: UC-07 Quản lý Request](UC-07_ManageRequests.md) | [Trang sau: UC-09 Xem lịch sử request →](UC-09_ViewRequestHistory.md)
