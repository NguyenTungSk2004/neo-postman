# Kế hoạch Migration CSDL

## Mục tiêu
- Đảm bảo thay đổi schema được triển khai an toàn, có thể rollback.
- Đồng bộ giữa môi trường Dev, Staging và Production.

## Nguyên tắc chung
- Mọi thay đổi CSDL phải đi kèm migration EF Core và rollback rõ ràng.
- Ưu tiên sử dụng `dotnet ef` để tạo và quản lý migration, không viết script SQL thủ công (trừ trường hợp đặc biệt).
- Migration phải idempotent, có thể chạy nhiều lần mà không phá vỡ dữ liệu.
- Không chỉnh sửa trực tiếp schema trong Production bằng tay.

## Quy trình triển khai
1. **Phân tích**: BA/SA xác nhận yêu cầu thay đổi dữ liệu từ `docs/srs/SRS.md`.
2. **Thiết kế**: DB engineer cập nhật tài liệu [entity-models.md](entity-models.md) và tạo migration mới bằng `dotnet ef migrations add`.
3. **Review**: Tối thiểu 1 reviewer kiểm tra script và tác động dữ liệu.
4. **Kiểm thử**: Chạy migration trên môi trường Dev + dữ liệu mẫu, ghi nhận kết quả.
5. **Staging**: Triển khai trên Staging, chạy regression liên quan.
6. **Production**: Chạy theo cửa sổ bảo trì, giám sát metric và log.

## Chu kỳ phát hành
| Môi trường | Tần suất | Ghi chú |
| --- | --- | --- |
| Dev | Hàng ngày | Chạy tự động khi merge vào nhánh develop/main. |
| Staging | 2 lần/tuần | Trước mỗi đợt QA regression. |
| Production | Theo sprint (2 tuần) | Yêu cầu checklist phê duyệt. |

## Đặt tên file migration
```
YYYYMMDDHHMM__mota_ngan.sql
Ví dụ: 20250918T1600__add_request_history_table.sql
```

## Checklist trước khi chạy Production
- [ ] Đã sao lưu dữ liệu mới nhất.
- [ ] Đã xác nhận thời gian downtime/thực thi.
- [ ] Đã cập nhật chỉ số giám sát liên quan.
- [ ] Đã chuẩn bị script rollback tương ứng.

## Rollback
- Sử dụng script `down`/`rollback` được viết sẵn.
- Nếu migration làm mất dữ liệu, cần có bản sao lưu để restore.
- Ghi lại sự cố trong nhật ký triển khai.

## Công cụ đề xuất
- `dotnet ef migrations` (Entity Framework Core) hoặc Flyway/Liquibase tùy stack.
- `psql` hoặc `sqlcmd` cho việc thực thi script thủ công khi cần.



