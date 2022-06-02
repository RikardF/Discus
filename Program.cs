using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExArbete.Areas.Identity.Data;
using ExArbete.Services;
using ExArbete.Interfaces;
using Append.Blazor.Notifications;

IUserService userService = new UserService();
userService.User = new();
var cookieExpirationTime = TimeSpan.FromDays(1);
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ExArbeteIdentityDbContextConnection");
string firestoreProject = builder.Configuration.GetValue<string>("FirestoreProject");
string firestoreAuthFile = builder.Configuration.GetValue<string>("FirebaseAuthFile");

if (!Path.IsPathRooted(firestoreAuthFile))
{
    firestoreAuthFile = Path.Combine(builder.Environment.ContentRootPath, firestoreAuthFile);
}
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firestoreAuthFile);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.ConfigureApplicationCookie(options =>
{
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
builder.Services.AddDbContext<ExArbeteIdentityDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ExArbeteIdentityDbContext>();
builder.Services.AddSingleton<IUserService>(userService);
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();
builder.Services.AddNotifications();
builder.Services.AddScoped<IAdminService, AdminService>();

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
