# Git Workflow Neo Postman

## Chiến lược nhánh
- `main`: luôn ở trạng thái ổn định, phản ánh Production.
- `develop`: tích hợp tính năng đã qua QA, chuẩn bị release.
- `feature/*`: cho từng user story hoặc task.
- `hotfix/*`: sửa lỗi khẩn cấp trên Production.

## Quy trình phát triển
1. Tạo nhánh `feature/xxx` từ `develop`.
2. Commit nhỏ, rõ ràng theo Conventional Commits.
3. Đẩy nhánh và tạo Pull Request vào `develop`.
4. Yêu cầu tối thiểu 1 reviewer (BE/FE tuỳ phạm vi) + QA nếu có test ảnh hưởng.
5. Merge dùng squash hoặc rebase để giữ lịch sử gọn.

## Chu kỳ release
- Cuối mỗi sprint, merge `develop` → `main` sau khi QA thông qua.
- Tag phiên bản dạng `vX.Y.Z` (SemVer).
- Tạo Release Note tóm tắt tính năng, bugfix, thay đổi DB.

## Hotfix
- Tạo nhánh `hotfix/issue-id` từ `main`.
- Sau khi fix và kiểm thử, merge vào `main`, tạo tag patch.
- Đồng thời merge ngược vào `develop` để đồng bộ.

## Chính sách review
- Không tự merge PR của chính mình.
- CI/CD phải xanh trước khi merge.
- Giải quyết comment trước khi yêu cầu approve lần nữa.

## Clean up nhánh
- Xóa nhánh feature/hotfix sau khi merge để tránh tồn đọng.
- Sử dụng automation (GitHub Action) để nhắc nhở nhánh cũ.
