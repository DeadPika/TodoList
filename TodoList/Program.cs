using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Сервис, позволяющий вносить изменения во время работы запущенного кода.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Получение строки подключения.
var connectionString = builder.Configuration.GetConnectionString("Default");
// Сервис, добавляющий контекст базы данных.
builder.Services.AddDbContext<TodoListContext>(option => option.UseSqlServer(connectionString));

// Добавление аутентификации через Cookie браузера.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/account");
builder.Services.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/NotFound";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
