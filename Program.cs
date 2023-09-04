using System.Security.Claims;
using Keycloak.AuthServices.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Host.ConfigureKeycloakConfigurationSource("keycloak.json");

builder.Services.AddKeycloakAuthentication(builder.Configuration, options =>
{
    options.RequireHttpsMetadata = false;
});

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("Administrator", policy => policy.RequireClaim("user_roles", "[Administrator]"));
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.MapGet("/", (ClaimsPrincipal user) =>
// {
//     app.Logger.LogInformation(user.Identity.Name);
// }).RequireAuthorization();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 401)
    {
        context.Request.Path = "/Home/Error";
        await next();
    }
});
app.UseStatusCodePages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

