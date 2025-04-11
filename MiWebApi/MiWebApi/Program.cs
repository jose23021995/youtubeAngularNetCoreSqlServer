using MiWebAPI.Data;
//llama la carpeta donde esta nuestra conecion a la bd
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<EmpleadoData>();
//Está registrando la clase EmpleadoData como un servicio singleton en el contenedor de dependencias de la aplicación.
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
/*
    *Estas líneas configuran el servicio de CORS en tu aplicación .NET. Vamos a desglosarlas:
    builder.Services.AddCors(…):
    Registra el servicio de CORS en el contenedor de dependencias. Esto permite definir políticas para controlar qué orígenes, cabeceras y métodos HTTP pueden acceder a tu API.
    options.AddPolicy("NuevaPolitica", app => { … }):
    Define una política de CORS con el nombre "NuevaPolitica". La función app se utiliza para configurar esta política.
    app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod():
    Configura la política para permitir:
    Cualquier origen: Cualquier dominio puede hacer peticiones a tu API.
    Cualquier cabecera: No se restringen las cabeceras que se pueden enviar.
 */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("NuevaPolitica");
//USAMOS LOS CORS
app.UseAuthorization();

app.MapControllers();

app.Run();
