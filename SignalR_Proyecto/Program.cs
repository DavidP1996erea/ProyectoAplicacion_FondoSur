using SignalR_Proyecto.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages(); 


app.MapHub<eleccionCategoriasHub>("/eleccionCategoriasHub");
app.MapHub<salaEsperaHub>("/salaEsperaHub");
app.MapHub<salasHub>("/salasHub");

app.Run();
