using DevIO.UI.Site.Data;
using Microsoft.EntityFrameworkCore;

//ConfigureServices
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MeuDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MeuDbContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Dependency Injection

builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();

#endregion

//Configure
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute("AreaProdutos", "Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
app.MapAreaControllerRoute("AreaVendas", "Vendas", "Vendas/{controller=Pedidos}/{action=Index}/{id?}");

// Rota padrão
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

// Rota de área genérica (não necessário no caso da demo)
//app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Rota de áreas especializadas

app.Run();
