using LinkShortening.Business.Implementations;
using LinkShortening.Business.Interfaces;
using LinkShortening.Business.Profiles;
using LinkShortening.Data;
using LinkShortening.Data.Repositories.Implementations;
using LinkShortening.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddDbContext<ApplicationDbContext>();


builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddAutoMapper(typeof(ModelProfile));


builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Url}/{action=Index}/{id?}");

app.Run();
