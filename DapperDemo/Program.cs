global using System.Data.SqlClient;
global using Dapper;
global using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<DapperRepository<Customer>>(s =>
    new DapperRepository<Customer>(
        builder.Configuration.GetConnectionString("ChinnokConnectionString")));

builder.Services.AddSingleton<DapperRepository<Instrument>>(s =>
    new DapperRepository<Instrument>(
        builder.Configuration.GetConnectionString("BandBookerConnectionString")));

builder.Services.AddSingleton<DapperRepository<Customers>>(s =>
    new DapperRepository<Customers>(
        builder.Configuration.GetConnectionString("NorthwindConnectionString")));

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
