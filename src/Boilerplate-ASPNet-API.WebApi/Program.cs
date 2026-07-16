using Microsoft.EntityFrameworkCore;
using Boilerplate_ASPNet_API.Infrastructure.Data;
using Boilerplate_ASPNet_API.Application.Interfaces;
using Boilerplate_ASPNet_API.Infrastructure.Repositories;
using Boilerplate_ASPNet_API.Application.Services;
using Boilerplate_ASPNet_API.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// 1. Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=boilerplate.db"));

// 2. Dependency Injection
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

// 3. Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Auto-migrate database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated(); 
}

// 5. Middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// ✅ GANTI DENGAN INI (Swagger selalu aktif)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boilerplate API V1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 

app.Run();