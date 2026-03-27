using Microsoft.EntityFrameworkCore;
using OnTapTuVung.Data;

var builder = WebApplication.CreateBuilder(args);
// 🔥 Thêm đoạn này cho Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

// Cấu hình SQLite
var connectionString = builder.Configuration.GetConnectionString("SQLiteConnection")
    ?? "Data Source=chinese_vocab.db";

builder.Services.AddDbContext<Connect>(options =>
    options.UseSqlite(connectionString)
    .EnableSensitiveDataLogging());

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services);
        Console.WriteLine("Database seeded successfully!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
        Console.WriteLine($"Error seeding database: {ex.Message}");
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