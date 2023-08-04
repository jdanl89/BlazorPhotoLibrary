// <copyright file="Program.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

using BlazorPhotoLibrary.Services;

WebApplicationBuilder _builder = WebApplication.CreateBuilder(args);

// Add services to the container.
_builder.Services.AddRazorPages();
_builder.Services.AddServerSideBlazor();

_builder.Services.AddHttpClient("PhotoClient", httpClient => httpClient.BaseAddress = new ("https://jsonplaceholder.typicode.com/"));
_builder.Services.AddScoped<IPhotoService, PhotoService>();

WebApplication _app = _builder.Build();

// Configure the HTTP request pipeline.
if (!_app.Environment.IsDevelopment())
{
    _ = _app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = _app.UseHsts();
}

_app.UseHttpsRedirection();

_app.UseStaticFiles();

_app.UseRouting();

_app.MapBlazorHub();
_app.MapFallbackToPage("/_Host");

_app.Run();
