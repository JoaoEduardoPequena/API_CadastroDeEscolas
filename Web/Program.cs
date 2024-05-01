using Application.App_Shared.Behaviors;
using Infraestructure.Infra_DbAcess;
using MediatR;
using System.Data;
using System.Text.Json.Serialization;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


builder.Services.AddCors(options =>
{

    options.AddPolicy(name: "CORS",
                         policy =>
                         {
                             policy.AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                         });
});

builder.Services.ConfigureSwagger();

builder.Services.AddScoped<IDbConnection>(_ =>
   new Microsoft.Data.SqlClient.SqlConnection(builder.Configuration.GetConnectionString("DataSource")));

builder.Services.AddScoped<IDataAccess, DataAccess>();
//builder.Services.AddScoped<IMainDataAccess, MainDataAccess>();

builder.Services.AddMvc()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );


var applicationAssembly = typeof(Application.AssemblyReference).Assembly;

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
//builder.Services.AddValidatorsFromAssembly(applicationAssembly);

//var myhandlers = AppDomain.CurrentDomain.Load("Application");
//builder.Services.AddMediatR(cfg =>
//{
//    cfg.RegisterServicesFromAssemblies(myhandlers);
//    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
//});

//builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"));

builder.Services.AddHsts(opt =>
{
    opt.Preload = true;
    opt.IncludeSubDomains = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(swagger =>
{
    swagger.SwaggerEndpoint("/swagger/escolas/swagger.json", "escolas");

    if (app.Environment.IsProduction())
        swagger.RoutePrefix = string.Empty;
});
app.UseDeveloperExceptionPage();

app.UseStaticFiles();
app.UseRouting();
//app.UseAuthorization();
app.UseCors("CORS");

app.MapControllers();
app.MapRazorPages();

app.Run();
