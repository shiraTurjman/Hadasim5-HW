using Api.swagger;
using Bll;
using Dal.Entity;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerFileUploadOperationFilter>();
});

//builder.Services.AddDbContext<ServerDbContext>(
//    o => o.UseSqlServer(builder.Configuration.GetConnectionString("myContextCon"))
//    );
//builder.Services.AddDbContext<ServerDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("myContextCon")));

ExtensionMethod.InitDI(builder.Services, builder.Configuration.GetConnectionString("myContextCon"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
