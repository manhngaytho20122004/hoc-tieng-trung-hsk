using Microsoft.EntityFrameworkCore;
using OnTapTuVung.Data;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Render dùng PORT environment
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

// Lấy connection string local
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Nếu có DATABASE_URL (deploy trên Render) thì dùng database server
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(databaseUrl))
{
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

// Add DbContext
builder.Services.AddDbContext<Connect>(options =>
    options.UseNpgsql(connectionString));

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔥 Auto migrate + seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var db = services.GetRequiredService<Connect>();

        Console.WriteLine("Migrating database...");
        db.Database.Migrate();

        Console.WriteLine("Seeding database...");
        await SeedData.InitializeAsync(services);

        Console.WriteLine("Database ready!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error database: " + ex.Message);
    }
}

// Middleware
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