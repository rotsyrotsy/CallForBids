using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using CallForBids.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<CallForBidsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CallForBidsContext") ?? throw new InvalidOperationException("Connection string 'CallForBidsContext' not found.")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/AccountAdmin/Login";
                    options.LogoutPath = "/AccountAdmin/Logout";
                });
builder.Services.AddAuthorization();

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddRazorPages(
    options =>
    {
        options.Conventions.AuthorizeAreaFolder("RPBids", "/");
        options.Conventions.AuthorizeAreaFolder("RPOffices", "/");
        options.Conventions.AuthorizeAreaFolder("RPProjects", "/");
        options.Conventions.AuthorizeAreaFolder("RPSubmissions", "/");
        options.Conventions.AuthorizeAreaFolder("RPSuppliers", "/");
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRequestLocalization("en-MG");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
