$YourProjectName = "neo-postman"
dotnet new sln -n $YourProjectName --force

dotnet sln add "BlazorWebApp/BlazorWebApp.csproj"
dotnet sln add "WebApi/WebApi.csproj"
dotnet sln add "Application/Application.csproj"
dotnet sln add "Domain/Domain.csproj"
dotnet sln add "Infrastructure/Infrastructure.csproj"

