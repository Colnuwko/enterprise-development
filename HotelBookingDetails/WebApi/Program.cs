using HotelBookingDetails.Domain.Repositories;
using HotelBookingDetails.Domain.Context;
using HotelBookingDetails.Domain.Entity;
using System.Reflection;
using HotelBookingDetails.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HotelBookingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options => 
{ 
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename)); 
});
builder.Services.AddScoped<IRepository<Client>, RepositoryClient>();
builder.Services.AddScoped<IRepository<Hotel>, RepositoryHotel>();
builder.Services.AddScoped<IRepository<Room>, RepositoryRoom>();
builder.Services.AddScoped<IRepository<ReservedRoom>, RepositoryReservedRooms>();
builder.Services.AddScoped<IRepository<Passport>, RepositoryPassport>();
builder.Services.AddScoped<IRepository<TypeRoom>, RepositoryTypeRoom>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(Mapping));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("BlazorClient");

app.UseHttpsRedirection();
app.MapControllers();
app.Run();