using AAAaCQRSApi.Data;
using AAAaCQRSApi.IRepository;
using AAAaCQRSApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Añadir soporte para CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddControllers();


// Configuración del DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

// Registrar Repositorios
builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();

// Registrar MediatR
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(AAAaCQRSApi.Handlers.GetOrderByIdQueryHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(AAAaCQRSApi.Handlers.CreateOrderCommandHandler).Assembly);

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS para la política configurada
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.MapControllers();



app.Run();

