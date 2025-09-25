# Neo Postman – Bộ tài liệu kỹ thuật

## Giới thiệu
Neo Postman là nền tảng cộng tác giúp nhóm xây dựng, kiểm thử và chia sẻ request HTTP theo phong cách Postman. Bộ tài liệu mô tả yêu cầu nghiệp vụ, thiết kế hệ thống, mô hình dữ liệu và các chuẩn triển khai.

## Sơ đồ tài liệu
| Phạm vi | Nội dung chính | Liên kết |
| --- | --- | --- |
| Yêu cầu | Đặc tả chức năng, actor, Use Case | [SRS](docs/srs/SRS.md) |
| Use Case | Chi tiết UC-01 → UC-18 | [Thư viện Use Case](docs/srs/use-case-specs/UC-01_SignUp.md) |
| Thực thể | Mô tả bảng, quan hệ, index | [Entity Model](docs/srs/entity-models.md) |

## Hướng dẫn sử dụng
- Bắt đầu với SRS để nắm phạm vi, actor và yêu cầu.
- Xem từng Use Case để hiểu luồng nghiệp vụ chi tiết.
- Tham chiếu Entity Model và Naming Conventions trước khi thiết kế migration hoặc cập nhật domain model.

## Tài liệu hình ảnh
- Use Case Diagram: [docs/imgs/use-case-diagram.png](docs/imgs/use-case-diagram.png)
- Database ERD: [docs/imgs/database-erd.png](docs/imgs/database-erd.png)

## Lịch sử cập nhật
| Ngày | Phiên bản | Ghi chú | Phụ trách |
| --- | --- | --- | --- |
| 2025-09-18 | 0.1.0 | Khởi tạo bộ tài liệu Neo Postman | Nhóm BA/SA |
| 2025-09-21 | 0.2.0 | Chuẩn hóa Use Case, Entity Model, quy ước đặt tên | Nhóm BA/SA |