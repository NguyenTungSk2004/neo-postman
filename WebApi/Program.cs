using WebApi.API;
using WebApi.Configurations;
using Infrastructure.DI;
using Yitter.IdGenerator;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Snowflake Id Generator
var options = new IdGeneratorOptions(1); // WorkerId = 1, bạn có thể đổi theo server
YitIdHelper.SetIdGenerator(options);

builder.Services.AddServices();
builder.Services.AddInfrastructure();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Đăng ký API groups
app.MapUserApi();

app.Run();