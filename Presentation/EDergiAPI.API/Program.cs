using EDergiAPI.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Configuration'dan connection string alınıyor (appsettings.json’dan)
builder.Services.AddPersistenceServices(builder.Configuration);


// 🔧 Controller ve Swagger servisleri ekleniyor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
	opt.SwaggerDoc("v1", new OpenApiInfo { Title = "EDergiAPI", Version = "v1" });
});

var app = builder.Build();

// 🔧 Ortama göre Swagger arayüzü
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EDergiAPI v1"));
}

// 🔧 HTTPS, Routing, Authorization ve Controller Mapping
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
