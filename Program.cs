
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine($"Hosting environment: {environment}");

environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (environment != "Local")
{
    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Local", EnvironmentVariableTarget.Machine);
}

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables("STREETCODE_");

builder.Services.AddControllersWithViews();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<string>();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
