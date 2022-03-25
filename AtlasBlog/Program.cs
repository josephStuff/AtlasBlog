using AtlasBlog.Data;
using AtlasBlog.Models;
using AtlasBlog.Services;
using AtlasBlog.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Adding in a COTS policy to avoid getting denied from my portfolio
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", builder =>
         builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});


// Add services to the container.
var connectionString = ConnectionService.GetConnectionString(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<BlogUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Register the Swagger Service
builder.Services.AddSwaggerGen(s =>
{
    OpenApiInfo openApiInfo = new()
    {
        Title = "Atlas Blog API",
        Version = "v1",
        Description = "Candidate API for the Atlas Blog",
        Contact = new()
        {
            Name = "J to the Watson",
            Url = new("https://www.CoderFoundry.com")
        },
        License = new()
        {
            Name = "Api License",
            Url = new("https://www.CoderFoundry.com")
        }
    };
    s.SwaggerDoc(openApiInfo.Version, openApiInfo);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);

});


builder.Services.AddControllersWithViews();

builder.Services.AddTransient<DataService>();
builder.Services.AddScoped<IImageService, BasicImageService>();
builder.Services.AddTransient<IEmailSender, BasicEmailService>();

builder.Services.AddTransient<SlugService>();
builder.Services.AddTransient<SearchService>();

// ---------------------------- LINE OF DEMARCATION ---------------
var app = builder.Build();

// ------------------------ WHEN CALLING A SERVICE FROM THIS MIDDLEWARE WE NEED AN INSTANCE OF IServiceScope
var scope = app.Services.CreateScope();
var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
await dataService.SetupDbAsync();


// --------- await scope.ServiceProvider.GetRequiredService<DataService>().SetupDbAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseCors("DefaultCorsPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// Call on our configured Swagger service ------------ <
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Atlas Blog API");
    s.InjectStylesheet("/css/swaggerUI.css");
    s.InjectJavascript("/js/swaggerUI.js");
    s.DocumentTitle = "CF Atlas Blog API";

    if (!app.Environment.IsDevelopment())
    {
        s.RoutePrefix = "";
    }
});


app.MapControllerRoute(
    name: "custom",
    pattern: "PostsDetails/{slug}",
    defaults: new { controller = "BlogPosts", action = "Details" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
