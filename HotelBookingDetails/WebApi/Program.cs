using HotelBookingDetails.Domain.Repositories;
using HotelBookingDetails.Domain;
using System.Reflection;
using HotelBookingDetails.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{ 
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
   
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename)); 
});
builder.Services.AddSingleton<IRepository<Client>, RepositoryClient>();
builder.Services.AddSingleton<IRepository<Hotel>, RepositoryHotel>();
builder.Services.AddSingleton<IRepository<Room>, RepositoryRoom>();
builder.Services.AddSingleton<IRepository<ReservedRooms>, RepositoryReservedRooms>();
builder.Services.AddSingleton<IRepository<Passport>, RepositoryPassport>();
builder.Services.AddSingleton<IRepository<TypeRoom>, RepositoryTypeRoom>();


builder.Services.AddAutoMapper(typeof(Mapping));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();