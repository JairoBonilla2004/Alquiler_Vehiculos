using AlquilerVehiculos_BLL;
using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;
using Sistema.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<Conexion>();
builder.Services.AddSingleton<EmpleadoDAL>();
builder.Services.AddSingleton<VehiculoDAL>();
builder.Services.AddSingleton<ClienteDAL>();
builder.Services.AddSingleton<ReservaDAL>();
builder.Services.AddSingleton<PagoDAL>();
builder.Services.AddSingleton<SeguroDAL>();


//servicios para los BLL
builder.Services.AddSingleton<EmpleadoBLL>();
builder.Services.AddSingleton<ClienteBLL>();
builder.Services.AddSingleton<VehiculoBLL>();
builder.Services.AddSingleton<ReservaBLL>();
builder.Services.AddSingleton<PagoBLL>();
builder.Services.AddSingleton<SeguroBLL>();
//servicio para obtener el dominio
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=LogIn}/{id?}")
    .WithStaticAssets();


app.Run();
