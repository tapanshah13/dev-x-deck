using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient<StackOverflowService>();
builder.Services.AddHttpClient<StackOverflowSearchService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowLocalHost3000");
app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Application started and listening on: http://localhost:5002 and https://localhost:5003");

app.Run();
