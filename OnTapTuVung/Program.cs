using Microsoft.EntityFrameworkCore;
using OnTapTuVung.Data;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// 🔥 Thêm đoạn này cho Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

// 🔥 Cấu hình database với PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Kiểm tra nếu chạy trên Render
if (Environment.GetEnvironmentVariable("RENDER") == "true")
{
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (!string.IsNullOrEmpty(databaseUrl))
    {
        // Chuyển đổi DATABASE_URL sang connection string PostgreSQL
        var databaseUri = new Uri(databaseUrl);
        var userInfo = databaseUri.UserInfo.Split(':');

        var npgsqlBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Database = databaseUri.AbsolutePath.Trim('/'),
            Username = userInfo[0],
            Password = userInfo[1],
            SslMode = SslMode.Require,
            TrustServerCertificate = true,
            Pooling = true,
            MaxPoolSize = 20,
            MinPoolSize = 5
        };

        connectionString = npgsqlBuilder.ConnectionString;
    }
}

// Đăng ký DbContext với PostgreSQL
builder.Services.AddDbContext<Connect>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
    })
    .EnableSensitiveDataLogging()); // Giữ lại để debug

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed data (cập nhật cho PostgreSQL)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Đảm bảo database được tạo
        var dbContext = services.GetRequiredService<Connect>();

        // Tự động migrate database
        dbContext.Database.Migrate();
        Console.WriteLine("Database migrated successfully!");

        // Seed dữ liệu
        await SeedData.InitializeAsync(services);
        Console.WriteLine("Database seeded successfully!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
        Console.WriteLine($"Error seeding database: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();