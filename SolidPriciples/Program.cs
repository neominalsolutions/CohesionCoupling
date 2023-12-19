using SolidPriciples.Cohesion;
using SolidPriciples.Coupling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// IoC Inversion Of Control, Instance alma kontrollerinin Developerdan Application Devredilmesi.

// service registeration iþlemi
// IoC Container yani servislerin bulunduðu toplantý dosya
// builder.Services uygulama geleninde servislerin toplandýðý yerler olmasý sebebi ile IoC Container görevi görüyor. (Castle Windsor,AutoFact, Ninject, SimpleInjector, StructorMap)
//builder.Services.AddScoped<IEmailService, VodafoneEmailService>();
// Uygulama genelinde tüm vodafone servisleri turkcell servisler ile deðiþtirimemiz lazým

//builder.Services.AddScoped<IEmailService, TurkcellEmailService>();
builder.Services.AddScoped<IEmailService, VodafoneEmailService>();
builder.Services.AddScoped<IDiscount, PromationCodeDiscountService>();
builder.Services.AddScoped<IRepository<Order>, EFOrderRepository>();
builder.Services.AddScoped<HighCohesionAndLowCouplingSample>(); // Interface olmadýðý durumlarda sýnýf instance alma yöntemi



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
