using LaundrySystem.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Managers
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

// Registers
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

// Authentication
builder.Services.ConfigureAuthentication();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
