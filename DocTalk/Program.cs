// This file configures the application's services and HTTP request pipeline.
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles; // Add this using directive for StaticFileOptions

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews(); // Add this for serving files

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// This ensures all static files are served from the wwwroot folder.
app.UseStaticFiles();

// This single line replaces the multiple MapFallbackToFile calls.
// It serves the correct HTML file based on the URL path, resolving the ambiguity.
app.MapFallback(async context =>
{
    var path = context.Request.Path.Value.Trim('/').ToLowerInvariant();
    if (path == "doctor")
    {
        context.Response.Redirect("/doctor.html");
    }
    else if (path == "patient")
    {
        context.Response.Redirect("/patient.html");
    }
    else
    {
        context.Response.Redirect("/index.html");
    }
});

// Map the SignalR hub to a specific URL.
app.MapHub<ChatHub>("/chathub");

app.Run();
