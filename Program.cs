using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ExArbete.Data;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
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
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });
builder.Services.AddTransient<FirestoreDb>((_) => FirestoreDb.Create(firestoreProject));

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
