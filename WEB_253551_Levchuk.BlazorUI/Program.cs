using WEB_253551_Levchuk.BlazorUI.Components;
using WEB_253551_Levchuk.BlazorUI.Services;
using WEB_253551_Levchuk.BlazorUI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Настройка HttpClient для работы с API
builder.Services.AddHttpClient<IDataService, DataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7001/");
});

// Добавление SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Маршрут для SignalR Hub
app.MapHub<GameHub>("/gamehub");

app.Run();
