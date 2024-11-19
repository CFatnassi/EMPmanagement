using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using EMPmanagement.Data;
using EMPmanagement.Persistence;
using EMPmanagement.Repository;
using static EMPmanagement.Helper.LangServices;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;

using EMPmanagement.Helper;
using Microsoft.AspNetCore.Mvc;
using EMPmanagement.Models;
using EMPmanagement.Controllers;
using EMPmanagement.Persistence;
using EMPmanagement.Repository;
using static EMPmanagement.Helper.LangServices;

var builder = WebApplication.CreateBuilder(args);


#region Localization
//Step 1
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options => {
    options.DataAnnotationLocalizerProvider = (type, factory) => {
        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
        return factory.Create("Resource", assemblyName.Name);
    };
});


//builder.Services.AddDevExpressControls();
builder.Services.AddMvc();
//builder.Services.ConfigureReportingServices(configurator => {
//	configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
//		viewerConfigurator.UseCachedReportSourceBuilder();
//	});
//});
// Add localization services
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
            new CultureInfo("en-GB") // Use "en-GB" for dd/MM/yyyy format
        };

    options.DefaultRequestCulture = new RequestCulture("en-GB");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    // Set the DateTime and number format
    foreach (var culture in supportedCultures)
    {
        culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
        culture.DateTimeFormat.LongDatePattern = "dd MMMM yyyy";
        // You can customize other date and time patterns as needed
    }
});


//builder.Services.Configure<RequestLocalizationOptions>(options => {
//	var supportedCultures = new List<CultureInfo> {
//		new CultureInfo("ar"),
//		new CultureInfo("en")
//	};
//	options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
//	options.SupportedCultures = supportedCultures;
//	options.SupportedUICultures = supportedCultures;
//	options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
//});
#endregion







// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


builder.Services.AddRazorPages();




builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddControllers().AddXmlSerializerFormatters();


builder.Services.AddControllers()
          .AddJsonOptions(options =>
             options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

builder.Services.AddControllers()
          .AddJsonOptions(options =>
             options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull);





builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new EMPmanagement.Helper.DateTimeConverter());

    });

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson().AddJsonOptions(Options => Options.JsonSerializerOptions.PropertyNamingPolicy = null);




builder.Services.AddControllers().AddNewtonsoftJson().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = null;
    o.JsonSerializerOptions.DictionaryKeyPolicy = null;
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
    o.JsonSerializerOptions.WriteIndented = false;
});

builder.Services.AddMvc(setupAction =>
{
    setupAction.EnableEndpointRouting = false;
}).AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});


builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.UseMemberCasing();
        });




//builder.Services.AddMvc().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
//});


//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions. = Newtonsoft.Json.DateTimeZoneHandling.Utc;
//    options.SerializerSettings.DateFormatString = "dd'/'MM''yyyy'T'HH':'mm':'ssZ";
//});






var app = builder.Build();

// Enable localization middleware
var supportedCultures = new[] { new CultureInfo("en-GB") };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en-GB")
    .AddSupportedCultures("en-GB")
    .AddSupportedUICultures("en-GB");

app.UseRequestLocalization(localizationOptions);

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "defaultRef",
    pattern: "{controller=Home}/{action=Index}/{Refid?}");

app.MapControllerRoute(
    name: "default2",
    pattern: "{controller=Home}/{action=Index}/{id?}/{id2?}");
app.MapRazorPages();

app.MapControllerRoute(
    name: "default2",
    pattern: "{controller=Home}/{action=Index}/{id?}/{id2?}/{id3?}");
app.MapRazorPages();

System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

app.Run();