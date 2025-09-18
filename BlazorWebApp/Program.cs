using BlazorWebApp.Components;
using BlazorWebApp.Configurations;
using MudBlazor.Services;
using Infrastructure.DI;
using Yitter.IdGenerator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddServices();

// Cấu hình Snowflake Id Generator
var options = new IdGeneratorOptions(1); // WorkerId = 1, bạn có thể đổi theo server
YitIdHelper.SetIdGenerator(options);

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Thêm MudBlazor services
builder.Services.AddMudServices();

// ⚡ Cấu hình logging console
builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.SingleLine = true;      // mỗi log một dòng
    options.TimestampFormat = "HH:mm:ss "; // thêm giờ phút giây
});
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
