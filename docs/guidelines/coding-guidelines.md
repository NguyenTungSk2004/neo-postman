# Coding Guidelines Neo Postman

## Nguyên tắc chung
- Ưu tiên code sạch, dễ đọc, có test đi kèm.
- Áp dụng SOLID, Clean Architecture và pattern phù hợp.
- Hạn chế lặp lại logic, refactor thành service/tiện ích dùng chung.

## Backend (.NET)
- Sử dụng C# 12, .NET 8.
- Cấu trúc solution theo tầng: `Api` → `Application` → `Domain` → `Infrastructure`.
- Controller chỉ xử lý mapping HTTP; business nằm ở Application layer.
- Sử dụng MediatR hoặc Command/Query pattern để tách xử lý.
- Validation dùng FluentValidation, trả lỗi dạng ProblemDetails.
- Logging chuẩn Microsoft.Extensions.Logging, không viết log thủ công.
- Tuân thủ async/await, không block `Task.Result`.

## Frontend (Blazor/Web)
- Tách component nhỏ, đặt tên PascalCase.
- State quản lý qua pattern (Flux/Redux) hoặc dịch vụ inject.
- Không gọi API trực tiếp trong component nếu có thể tách ra service.
- CSS sử dụng BEM hoặc CSS module; đặt biến rõ nghĩa.

## Đặt tên
- Class/Interface: PascalCase (`WorkspaceService`, `IWorkspaceRepository`).
- Method: PascalCase (`SendRequestAsync`).
- Biến private: camelCase với tiền tố `_` (`_requestRepository`).
- Hằng số: `SCREAMING_SNAKE_CASE`.
- Đường dẫn API: `kebab-case`, ví dụ `/api/v1/workspaces/{id}`.

## Comment & tài liệu hóa
- Chỉ comment khi cần giải thích nghiệp vụ phức tạp.
- Dùng XML Doc cho public API.
- Update doc khi có thay đổi behaviour.

## Xử lý lỗi
- Throw exception có ý nghĩa, tránh catch trống.
- Chuẩn hóa lỗi bằng ProblemDetails/ApiResponse.
- Không lộ thông tin nhạy cảm trong log hoặc response.

## Bảo mật
- Kiểm tra quyền (Authorization) tại API layer.
- Sanitize input (đặc biệt script test) để tránh injection.
- Tuân thủ guideline OWASP Top 10.

## Convention commit
- Sử dụng chuẩn [Conventional Commits](https://www.conventionalcommits.org/).
- Cú pháp: `<type>(scope?): <message>`.
- Type thường dùng: `feat`, `fix`, `docs`, `refactor`, `test`, `chore`.
- Scope ví dụ: `api`, `ui`, `db`.
- Ví dụ: `feat(api): support request history filter`.

## Kiểm tra & build
- Mỗi PR phải chạy `dotnet test` và lint (StyleCop/ESLint).
- Không merge khi pipeline thất bại.
- Test mới phải có assertion rõ ràng, tránh false positive.
