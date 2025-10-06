$YourProjectName = "neo-postman"
dotnet new sln -n $YourProjectName --force

# New-Item -ItemType Directory -Path "BlazorWebApp"
# # Tạo dự án Blazor Web App
# dotnet new blazor -n BlazorWebApp

# # Add references between projects
# dotnet add "BlazorWebApp/BlazorWebApp.csproj" reference "Application/Application.csproj"
# dotnet add "BlazorWebApp/BlazorWebApp.csproj" reference "Infrastructure/Infrastructure.csproj"

# Thêm reference đến project Application (giả sử Application nằm cùng solution)
dotnet sln add "BlazorWebApp/BlazorWebApp.csproj"
dotnet sln add "Application/Application.csproj"
dotnet sln add "Domain/Domain.csproj"
dotnet sln add "Infrastructure/Infrastructure.csproj"

