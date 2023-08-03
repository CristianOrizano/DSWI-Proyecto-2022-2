
//(esto es lo que agrego)
using ProyectoMVC2022.Models.ElectrodomDI;
using ProyectoMVC2022.Models.ClienteDI;
using ProyectoMVC2022.Models.TrabajadorDI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//--------------------
//despues de 30 minutos de iniciada la sesion se cierra
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
//--------------------

//(esto es lo que agrego)
//implementar la interface como el objeto que ejecuta los metodos de la clase
builder.Services.AddSingleton<ElectrodomesticoIFace, ElectrodomesticoDAO>();
builder.Services.AddSingleton<ICLiente, ClienteDAO>();
builder.Services.AddSingleton<ITrabajador, TrabajadorDAO>();

var app = builder.Build();

//habilitar el estado Session en el proyecto
app.UseSession();
//--------------------

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trabajador}/{action=Login}/{id?}");

app.Run();
