using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Dal;
using TransnationalLanka.Rms.Mobile.Services.Location;
using TransnationalLanka.Rms.Mobile.Services.MetaData;
using TransnationalLanka.Rms.Mobile.WebApi.Location;
using TransnationalLanka.Rms.Mobile.WebApi.MetaData;

var builder = WebApplication.CreateBuilder(args);

//Initialize the Database Context
builder.Services.AddDbContext<RmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//Add Project Services
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IMetaDataService, MetaDataService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.Contains("Uat"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Application Routes
LocationRoutes.Register(app);
MetaDataRoutes.Register(app);

app.UseHttpsRedirection();

app.Run();