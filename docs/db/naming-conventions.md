# Quy ước đặt tên CSDL

## Quy tắc chung
- Sử dụng tiếng Anh, chữ thường, phân tách bằng dấu gạch dưới `_`.
- Tên phải ngắn gọn nhưng phản ánh rõ chức năng.
- Không dùng ký tự đặc biệt, khoảng trắng hoặc chữ in hoa.

## Bảng
| Loại | Quy tắc | Ví dụ |
| --- | --- | --- |
| Bảng chuẩn | `danh_tu_so_nhieu` | `users`, `workspace_members` |
| Bảng liên kết | `ten1_ten2` theo alphabet | `collection_folders` |
| Bảng lịch sử/log | Thêm hậu tố `_history` hoặc `_logs` | `request_history`, `audit_logs` |

## Cột
| Loại | Quy tắc | Ví dụ |
| --- | --- | --- |
| Khóa chính | `id` (`bigint` sinh bởi `YitIdHelper.NextId()`) | `id` |
| Khoá ngoại | `{ten_bang_tham_chieu}_id` | `workspace_id`, `user_id` |
| Cột boolean | Tiền tố `is_`, `has_`, `can_` | `is_active`, `has_secret` |
| Cột ngày giờ | Hậu tố `_at` theo UTC | `created_at`, `updated_at`, `last_used_at` |
| Soft delete | `is_deleted` + `deleted_at` |  |

## Index
- Tên index: `ix_{ten_bang}_{cot1}_{cot2}`.
- Index unique: thêm tiền tố `ux_`.
- Ví dụ: `ix_requests_workspace_id`, `ux_workspace_members_workspace_id_user_id`.

## Khóa ngoại
- Tên constraint: `fk_{bang_nguon}_{cot_fk}__{bang_dich}_{cot_dich}`.
- Ví dụ: `fk_requests_collection_id__collections_id`.

## Check constraint
- Tên: `ck_{ten_bang}_{mo_ta}`.
- Ví dụ: `ck_workspace_members_role_valid`.

## Enum/Lookup
- Ưu tiên lưu trong bảng lookup (`roles`, `auth_providers`).
- Nếu dùng enum PostgreSQL: đặt tên `enum_{nghe_nghiep}` ví dụ `enum_workspace_role`.

## Migration script
- Trong script, comment rõ lý do thay đổi và tác động dữ liệu.


