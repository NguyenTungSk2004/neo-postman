# Đặc Tả Yêu Cầu Phần Mềm (SRS) - Neo Postman

## 1. Giới thiệu
### 1.1 Mục đích
Tài liệu SRS mô tả mục tiêu kinh doanh, phạm vi chức năng và các yêu cầu phi chức năng của dự án Neo Postman. Đây là nguồn tham chiếu chính cho đội ngũ sản phẩm, phát triển, QA và vận hành.

### 1.2 Phạm vi
Neo Postman là nền tảng SaaS hỗ trợ người dùng và nhóm làm việc tạo, gửi, kiểm thử và chia sẻ yêu cầu HTTP trong các workspace. Phạm vi tài liệu bao gồm ứng dụng web, API backend, dịch vụ nền và các thành phần hạ tầng liên quan.

### 1.3 Tài liệu tham chiếu
- README tổng quan: ../README.md
- Use Case Diagram (placeholder): [use-case-diagram.png](use-case-diagram.png)
- Database ERD (placeholder): [database-erd.png](database-erd.png)

### 1.4 Thuật ngữ và viết tắt
| Thuật ngữ | Định nghĩa |
| --- | --- |
| Workspace | Không gian làm việc chứa collection, environment và thành viên. |
| Collection | Tập hợp có thứ tự các request thuộc một workspace. |
| Environment | Bộ key-value dùng thay thế biến khi chạy request. |
| Request History | Lịch sử ghi nhận các lần gửi request. |
| Owner | Thành viên có toàn quyền trong workspace. |
| Admin | Thành viên được phép quản trị nhưng không được xóa workspace. |

## 2. Mô tả tổng thể
### 2.1 Bối cảnh hệ thống
Neo Postman hoạt động như một hệ thống độc lập gồm SPA front-end và API backend theo REST. Hệ thống tích hợp dịch vụ email để gửi lời mời và có thể kết nối với công cụ giám sát khi vận hành.

### 2.2 Chức năng chính
- Đăng ký, đăng nhập và quản lý phiên người dùng.
- Tạo và tổ chức workspace, quản lý thành viên theo vai trò.
- Quản lý collection, folder, request với khả năng version hóa.
- Quản lý environment và biến, thay thế dữ liệu động khi gửi request.
- Thực thi request, lưu lịch sử và xem kết quả dưới nhiều chế độ.
- Hỗ trợ tác vụ cộng tác: mời thành viên, chuyển quyền, phân quyền.

### 2.3 Phân loại người dùng
| Lớp người dùng | Mô tả | Mục tiêu chính |
| --- | --- | --- |
| Người dùng chưa xác thực | Khách truy cập chưa có phiên đăng nhập. | Đăng ký hoặc đăng nhập. |
| Thành viên xác thực | Người dùng thuộc một hoặc nhiều workspace. | Quản lý tài sản và gửi request. |
| Workspace Admin | Thành viên có quyền quản trị workspace. | Quản lý thành viên, cấu trúc dữ liệu. |
| Workspace Owner | Quyền cao nhất trong workspace. | Chuyển quyền, xóa workspace, thiết lập admin. |
| Hệ thống | Dịch vụ nền tự động. | Lưu lịch sử, gửi email, dọn dẹp dữ liệu. |

### 2.4 Môi trường vận hành
- Front-end: SPA hỗ trợ Chrome, Edge, Firefox, Safari (2 bản gần nhất).
- Backend: .NET 8 API chạy dạng container.
- CSDL: PostgreSQL 15+ (ưu tiên) hoặc SQL Server 2019+.
- Hạ tầng: Kubernetes/Azure App Service, hỗ trợ auto scaling.

### 2.5 Ràng buộc
- Tuân thủ chuẩn bảo mật nội bộ và OWASP ASVS Level 2.
- Mọi secrets lưu trong kho bí mật (Azure Key Vault/AWS Secrets Manager).
- Chỉ hỗ trợ request HTTP/HTTPS ở giai đoạn hiện tại.

### 2.6 Giả định và phụ thuộc
- Dịch vụ email sẵn sàng để gửi thông báo và lời mời.
- Có thể tích hợp OAuth (Google, GitHub) ở giai đoạn sau.
- API outbound được phép truy cập Internet để thực thi request.

## 3. Yêu cầu chức năng
### 3.1 Xác thực và danh tính
| ID | Mô tả |
| --- | --- |
| FR-A-01 | Hệ thống cho phép đăng ký bằng email, mật khẩu và tên hiển thị. |
| FR-A-02 | Mật khẩu phải được băm bằng PBKDF2 hoặc Argon2 trước khi lưu. |
| FR-A-03 | Hệ thống xác thực bằng email/mật khẩu và trả về access token + refresh token. |
| FR-A-04 | Refresh token bị vô hiệu khi người dùng đăng xuất hoặc đổi mật khẩu. |
| FR-A-05 | Cho phép tích hợp OAuth (Google, GitHub) ở phiên bản tương lai. |

### 3.2 Workspace và thành viên
| ID | Mô tả |
| --- | --- |
| FR-W-01 | Người dùng xác thực được tạo workspace với tên và mô tả. |
| FR-W-02 | Người tạo workspace được gán vai trò Owner mặc định. |
| FR-W-03 | Owner/Admin mời thành viên mới qua email và gán vai trò viewer/member. |
| FR-W-04 | Owner chuyển quyền sở hữu cho thành viên khác. |
| FR-W-05 | Owner xóa workspace và toàn bộ dữ liệu liên quan. |
| FR-W-06 | Owner/Admin loại bỏ thành viên khỏi workspace. |
| FR-W-07 | Owner/Admin thay đổi vai trò thành viên giữa viewer/member/admin. |

### 3.3 Collection, folder, request
| ID | Mô tả |
| --- | --- |
| FR-C-01 | Thành viên tạo, cập nhật, nhân bản, xóa collection. |
| FR-C-02 | Thành viên tạo folder cha/con trong collection. |
| FR-C-03 | Thành viên tạo, cấu hình, nhân bản, xóa request HTTP. |
| FR-C-04 | Hệ thống hỗ trợ cấu hình method, URL, header, param, body, script test. |
| FR-C-05 | Hệ thống lưu lịch sử thay đổi request (version hoặc history). |

### 3.4 Environment và biến
| ID | Mô tả |
| --- | --- |
| FR-E-01 | Thành viên tạo, chỉnh sửa, xóa environment. |
| FR-E-02 | Thành viên quản lý biến (key, value, secret flag). |
| FR-E-03 | Engine thay thế biến environment khi gửi request. |
| FR-E-04 | Ngăn xóa environment đang được schedule (nếu có tính năng trong tương lai). |

### 3.5 Thực thi request và lịch sử
| ID | Mô tả |
| --- | --- |
| FR-R-01 | Thành viên gửi request đã cấu hình. |
| FR-R-02 | Hệ thống ghi log request/response vào RequestHistory. |
| FR-R-03 | Thành viên lọc, xem lịch sử theo request, status code hoặc thời gian. |
| FR-R-04 | Hệ thống xử lý timeout và trả lỗi phù hợp. |

### 3.6 Audit và cộng tác
| ID | Mô tả |
| --- | --- |
| FR-AC-01 | Ghi nhận mọi thay đổi thành viên trong workspace. |
| FR-AC-02 | Áp dụng kiểm soát truy cập theo vai trò trên mọi thao tác workspace. |
| FR-AC-03 | Gửi email thông báo khi có lời mời tham gia workspace. |

## 4. Danh mục Use Case
| ID | Tên | Actor chính | Mô tả | Tài liệu |
| --- | --- | --- | --- | --- |
| UC-01 | Đăng ký | Người dùng chưa xác thực | Tạo tài khoản mới. | [UC-01_SignUp](use-case-specs/UC-01_SignUp.md) |
| UC-02 | Đăng nhập | Người dùng chưa xác thực | Xác thực và tạo phiên. | [UC-02_Login](use-case-specs/UC-02_Login.md) |
| UC-03 | Cập nhật hồ sơ | Thành viên xác thực | Sửa thông tin cá nhân. | [UC-03_UpdateProfile](use-case-specs/UC-03_UpdateProfile.md) |
| UC-04 | Tạo workspace | Thành viên xác thực | Tạo workspace mới và làm Owner. | [UC-04_CreateWorkspace](use-case-specs/UC-04_CreateWorkspace.md) |
| UC-05 | Quản lý collection | Thành viên xác thực | Thao tác CRUD với collection. | [UC-05_ManageCollections](use-case-specs/UC-05_ManageCollections.md) |
| UC-06 | Quản lý folder | Thành viên xác thực | Thao tác CRUD với folder. | [UC-06_ManageFolders](use-case-specs/UC-06_ManageFolders.md) |
| UC-07 | Quản lý request | Thành viên xác thực | Thao tác CRUD với request. | [UC-07_ManageRequests](use-case-specs/UC-07_ManageRequests.md) |
| UC-08 | Gửi request | Thành viên xác thực | Thực thi request và xem phản hồi. | [UC-08_SendRequest](use-case-specs/UC-08_SendRequest.md) |
| UC-09 | Xem lịch sử request | Thành viên xác thực | Tra cứu lịch sử thực thi. | [UC-09_ViewRequestHistory](use-case-specs/UC-09_ViewRequestHistory.md) |
| UC-10 | Quản lý environment | Thành viên xác thực | CRUD environment. | [UC-10_ManageEnvironments](use-case-specs/UC-10_ManageEnvironments.md) |
| UC-11 | Quản lý biến environment | Thành viên xác thực | CRUD biến của environment. | [UC-11_ManageEnvironmentVariables](use-case-specs/UC-11_ManageEnvironmentVariables.md) |
| UC-12 | Mời thành viên | Workspace Admin | Thêm người dùng vào workspace. | [UC-12_InviteMember](use-case-specs/UC-12_InviteMember.md) |
| UC-13 | Loại thành viên | Workspace Admin | Gỡ thành viên khỏi workspace. | [UC-13_RemoveMember](use-case-specs/UC-13_RemoveMember.md) |
| UC-14 | Phân quyền thành viên | Workspace Admin | Đổi vai trò viewer/member/admin. | [UC-14_AssignMemberRole](use-case-specs/UC-14_AssignMemberRole.md) |
| UC-15 | Phong admin | Workspace Owner | Cấp quyền admin cho thành viên. | [UC-15_PromoteWorkspaceAdmin](use-case-specs/UC-15_PromoteWorkspaceAdmin.md) |
| UC-16 | Xóa workspace | Workspace Owner | Xóa vĩnh viễn workspace. | [UC-16_DeleteWorkspace](use-case-specs/UC-16_DeleteWorkspace.md) |
| UC-17 | Chuyển quyền Owner | Workspace Owner | Trao quyền sở hữu cho người khác. | [UC-17_TransferOwnership](use-case-specs/UC-17_TransferOwnership.md) |
| UC-18 | Ghi log lịch sử | Hệ thống | Lưu kết quả mỗi lần gửi request. | [UC-18_LogRequestHistory](use-case-specs/UC-18_LogRequestHistory.md) |

## 5. Yêu cầu phi chức năng
### 5.1 Hiệu năng
- NFR-P-01: API cốt lõi phản hồi < 500 ms cho 95% request ở tải 500 người dùng đồng thời.
- NFR-P-02: Engine gửi request outbound trong vòng 200 ms sau thao tác người dùng.
- NFR-P-03: Phân trang giới hạn 100 bản ghi mỗi trang.

### 5.2 Bảo mật
- NFR-S-01: Toàn bộ lưu lượng sử dụng HTTPS (TLS 1.2 trở lên).
- NFR-S-02: Không lưu hoặc log mật khẩu dạng plain text.
- NFR-S-03: Áp dụng RBAC cho mọi tài nguyên workspace.
- NFR-S-04: Audit log ghi nhận đăng nhập, lời mời, chuyển quyền với timestamp và actor.

### 5.3 Sẵn sàng và tin cậy
- NFR-A-01: Uptime môi trường Production đạt 99.5%/tháng.
- NFR-A-02: Tự động backup CSDL tối thiểu mỗi 6 giờ.
- NFR-A-03: RPO 30 phút, RTO 2 giờ.

### 5.4 Bảo trì và mở rộng
- NFR-M-01: Codebase tuân theo kiến trúc nhiều tầng (API, Application, Domain, Infrastructure).
- NFR-M-02: API node phải stateless để scale ngang.
- NFR-M-03: Tính năng thử nghiệm bật/tắt qua feature flag.

### 5.5 Tuân thủ
- NFR-C-01: Dữ liệu PII tuân thủ nguyên tắc tối thiểu hóa theo GDPR.
- NFR-C-02: Thư viện bên thứ ba phải được quét lỗ hổng (OWASP Dependency Check).

## 6. Yêu cầu giao diện bên ngoài
### 6.1 Giao diện người dùng
- SPA responsive hỗ trợ dark/light mode.
- Sidebar workspace để điều hướng collection, environment, history.
- Request builder có tab Params, Headers, Body, Tests.
- Response viewer hỗ trợ raw, preview, cây JSON.

### 6.2 Giao diện API
- Endpoint REST chuẩn JSON dưới `/api/v1`.
- Xác thực bằng bearer token OAuth 2.0.
- Trả header rate limit: `X-Rate-Limit-Remaining`, `Retry-After`.

### 6.3 Giao diện dữ liệu
- Tích hợp SMTP hoặc dịch vụ email transactional cho lời mời.
- Hỗ trợ webhook thông báo kết quả request (định hướng tương lai).

## 7. Yêu cầu dữ liệu
- Thực thể lõi: Users, UserAuthProviders, UserSessions, Workspaces, WorkspaceMembers, Collections, Folders, Requests, RequestHeaders, RequestParams, RequestTests, Environments, EnvironmentVariables, RequestHistory.
- Trường nhạy cảm (mật khẩu, secret) phải được băm hoặc mã hóa.
- Chi tiết entity và quan hệ: [../db/entity-models.md](../db/entity-models.md).
- Tham khảo [database-erd.png](database-erd.png) cho quan hệ (placeholder).

## 8. Báo cáo và phân tích
- Cung cấp thống kê số lượng request theo status code ở cấp workspace (kế hoạch tương lai).
- Cho phép export lịch sử request dạng CSV trong 30 ngày gần nhất.

## 9. Tiêu chí nghiệm thu
- Mọi yêu cầu chức năng được map với test case và pass QA.
- Không còn defect mức nghiêm trọng (sev 1/2) trước khi release Production.
- Pipeline triển khai phải có bước test unit, integration và E2E tự động.

## 10. Ma trận truy vết
| Module | Yêu cầu chính | Use Case |
| --- | --- | --- |
| Authentication | FR-A-01 → FR-A-05 | UC-01, UC-02 |
| Hồ sơ | FR-A-03, FR-A-04 | UC-03 |
| Workspace | FR-W-01 → FR-W-07 | UC-04, UC-12, UC-13, UC-14, UC-15, UC-16, UC-17 |
| Collection & Request | FR-C-01 → FR-C-05 | UC-05, UC-06, UC-07 |
| Thực thi | FR-R-01 → FR-R-04 | UC-08, UC-09, UC-18 |
| Environment | FR-E-01 → FR-E-04 | UC-10, UC-11 |

## 11. Vấn đề mở
- Hỗ trợ GraphQL và WebSocket đang nằm ngoài phạm vi.
- API công khai để quản lý workspace qua automation đang được cân nhắc.

