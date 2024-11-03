using HotelBookingDetails.Domain.Repositories;
using System.Reflection;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{ 
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
   
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename)); 
});
builder.Services.AddSingleton<IRepositoryClient, RepositoryClient>();
builder.Services.AddSingleton<IRepositoryHotel, RepositoryHotel>();
builder.Services.AddSingleton<IRepositoryRoom, RepositoryRoom>();
builder.Services.AddSingleton<IRepositoryReservedRooms, RepositoryReservedRooms>();
builder.Services.AddSingleton<IRepositoryPassport, RepositoryPassport>();
builder.Services.AddSingleton<IRepositoryTypeRoom, RepositoryTypeRoom>();
builder.Services.AddAutoMapper(typeof(Mapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
