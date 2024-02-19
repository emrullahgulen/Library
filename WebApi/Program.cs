using Microsoft.Extensions.FileProviders;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Autofac;
using Core.Logger;
using Autofac.Core;
using Business.Absract;
using Business.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookService, BookService>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new AutofacBusinessModule());
            });

builder.Host.UseSeriLogging();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();

var tmpFilesPath = Path.Combine(Directory.GetCurrentDirectory(), @"tmpfiles");
if (!Directory.Exists(tmpFilesPath))
    Directory.CreateDirectory(tmpFilesPath);

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(tmpFilesPath),
    RequestPath = new PathString("/tmpfiles")
});

app.Run();
