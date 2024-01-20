using APIS.Web.Services;
using APIS.Web.Services.IServices;
using APIS.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ICouponServices, CouponServices>();
builder.Services.AddHttpClient<IAuthServices, AuthServices>();
SD.APICouponBase=builder.Configuration["ServicesUrls:CouponApi"];
SD.APIAuthBase=builder.Configuration["ServicesUrls:AuthApi"];

builder.Services.AddScoped<IBaseServices, BaseServices>();
builder.Services.AddScoped<ICouponServices, CouponServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(10);
    options.LoginPath = "/auth/login";
    options.AccessDeniedPath = "/Auth/AcssesDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
