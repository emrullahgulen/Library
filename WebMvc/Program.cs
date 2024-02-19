using Microsoft.Extensions.FileProviders;
using Core.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Host.UseSeriLogging();


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

//app.UseAuthorization();

app.UseStaticFiles();

var tmpFilesPath = Path.Combine(Directory.GetCurrentDirectory(), @"tmpfiles");
if (!Directory.Exists(tmpFilesPath))
    Directory.CreateDirectory(tmpFilesPath);

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(tmpFilesPath),
    RequestPath = new PathString("/tmpfiles")
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
