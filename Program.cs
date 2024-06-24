using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using CallForBids.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using DinkToPdf.Contracts;
using DinkToPdf;
using CallForBids.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<CallForBidsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CallForBidsContext") ?? throw new InvalidOperationException("Connection string 'CallForBidsContext' not found.")));


builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();


var context = new CustomAssemblyLoadContext();
var libPath = Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox", "libwkhtmltox.dll");
context.LoadUnmanagedLibrary(libPath);

// Register DinkToPdf
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Add ProductRepository to the dependency injection container
builder.Services.AddTransient<BidsRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<SubmissionRepository>();
//builder.Services.AddTransient<BasketRepository>();
builder.Services.AddTransient<PdfService>();
builder.Services.AddTransient<RazorViewToStringRenderer>();

builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Adjust as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // make the session cookie essential
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
