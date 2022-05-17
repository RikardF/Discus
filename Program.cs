using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ExArbete.Data;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExArbete.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ExArbete.Services;
using ExArbete.Interfaces;
using ExArbete.Models;

IUserService userService = new UserService();
userService.User = new();
var cookieExpirationTime = TimeSpan.FromDays(1);
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ExArbeteIdentityDbContextConnection");
builder.Services.AddDbContext<ExArbeteIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));string firestoreProject = builder.Configuration.GetValue<string>("FirestoreProject");
string firestoreAuthFile = builder.Configuration.GetValue<string>("FirebaseAuthFile");

if (!Path.IsPathRooted(firestoreAuthFile))
{
    firestoreAuthFile = Path.Combine(builder.Environment.ContentRootPath, firestoreAuthFile);
}
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firestoreAuthFile);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.ConfigureApplicationCookie(options => {
    options.ExpireTimeSpan = cookieExpirationTime;
});
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "229437423853-8l20s99tb07qlfm4ls16nmqb41r4rvuf.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
        googleOptions.CorrelationCookie.Expiration = cookieExpirationTime;
        googleOptions.Events.OnCreatingTicket = (context) =>
                {
                    string? picture = context.User.GetProperty("picture").GetString();
                    string? name = context.User.GetProperty("name").GetString();
                    string? email = context.User.GetProperty("email").GetString();
                    userService.User.ProfileImage = picture;
                    userService.User.GoogleName = name;
                    userService.User.Email = email;
                    return Task.CompletedTask;
                };
    });
builder.Services.AddTransient<FirestoreDb>((_) => FirestoreDb.Create(firestoreProject));
builder.Services.AddDbContext<ExArbeteIdentityDbContext>(options => {
    options.UseSqlServer(connectionString);
    }); 
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ExArbeteIdentityDbContext>();
builder.Services.AddSingleton<IUserService>(userService);
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
