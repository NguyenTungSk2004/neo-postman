# Để tạo 1 migartion mới

dotnet ef migrations add <Tên migration> --context AppDbContext -p ./Infrastructure/Infrastructure.csproj -s ./BlazorWebApp/BlazorWebApp.csproj --output-dir Persistences/Migrations

# Để gỡ migration gần nhất

dotnet ef migrations remove --context AppDbContext -p ./Infrastructure/Infrastructure.csproj -s ./BlazorWebApp/BlazorWebApp.csproj

# Để xem danh sách migration

dotnet ef migrations list --context AppDbContext -p ./Infrastructure/Infrastructure.csproj -s ./BlazorWebApp/BlazorWebApp.csproj

# Để cập nhật migration lên Database 

dotnet ef database update --context AppDbContext -p ./Infrastructure/Infrastructure.csproj -s ./BlazorWebApp/BlazorWebApp.csproj

# Để cập nhật migration cụ thể lên Database 

dotnet ef database update <Tên migration> --context AppDbContext -p ./Infrastructure/Infrastructure.csproj -s ./BlazorWebApp/BlazorWebApp.csproj

# Xóa database
dotnet ef database drop --context AppDbContext -p ./Infrastructure/Infrastructure.csproj -s ./BlazorWebApp/BlazorWebApp.csproj