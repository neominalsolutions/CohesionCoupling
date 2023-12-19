using SolidPriciples.Cohesion;
using SolidPriciples.Coupling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// IoC Inversion Of Control, Instance alma kontrollerinin Developerdan Application Devredilmesi.

// service registeration i�lemi
// IoC Container yani servislerin bulundu�u toplant� dosya
// builder.Services uygulama geleninde servislerin topland��� yerler olmas� sebebi ile IoC Container g�revi g�r�yor. (Castle Windsor,AutoFact, Ninject, SimpleInjector, StructorMap)
//builder.Services.AddScoped<IEmailService, VodafoneEmailService>();
// Uygulama genelinde t�m vodafone servisleri turkcell servisler ile de�i�tirimemiz laz�m

//builder.Services.AddScoped<IEmailService, TurkcellEmailService>();
builder.Services.AddScoped<IEmailService, VodafoneEmailService>();
builder.Services.AddScoped<IDiscount, PromationCodeDiscountService>();
builder.Services.AddScoped<IRepository<Order>, EFOrderRepository>();
builder.Services.AddScoped<HighCohesionAndLowCouplingSample>(); // Interface olmad��� durumlarda s�n�f instance alma y�ntemi



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
