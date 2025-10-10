using Microsoft.EntityFrameworkCore;
using Lab08_Andreboza.Data;
using Lab08_Andreboza.Services; // <-- Importante: Añadir el using para los servicios

var builder = WebApplication.CreateBuilder(args);

// --- Configuración de la Base de Datos ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LINQExample01DbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// --- Fin de la Configuración ---


// --- REGISTRO DE SERVICIOS ---
// Esto le dice a la aplicación cómo crear tus servicios cuando un controlador los pida.
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
// --- FIN DEL REGISTRO ---


// --- Configuración para API y Swagger ---
builder.Services.AddControllers(); // Se cambia a AddControllers para APIs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Herramienta para documentar y probar tu API
// --- Fin de la Configuración ---

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Podrás probar tu API en la URL /swagger
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Se cambia a MapControllers para que funcione con los atributos [ApiController]
app.MapControllers();

app.Run();